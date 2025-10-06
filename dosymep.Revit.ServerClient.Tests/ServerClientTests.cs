using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using dosymep.Revit.ServerClient.DataContracts;

using NUnit.Framework;

namespace dosymep.Revit.ServerClient.Tests {
    public class ServerClientTests {
        private IServerClient _serverClient;

        private const string ServerName = "revit-test";
        private const string ServerVersion = "2022";

        private static readonly object[] _relativePathCases = {
            new object[] {@"Folder1\Folder2", new FolderContents(@"Folder1"), new FolderData("Folder2")},
            new object[] {@"Folder1\Model1", new FolderContents(@"Folder1"), new ModelData("Model1")},
            new object[] {
                @"Folder1\Folder2\Folder3", new FolderContents(@"Folder1\Folder2"), new FolderData("Folder3")
            },
            new object[] {@"Folder1\Folder2\Model1", new FolderContents(@"Folder1\Folder2"), new ModelData("Model1")}
        };

        private static readonly object[] _visibleModelPathCases = {
            new object[] {
                $@"RSN://{ServerName}\Folder1\Model1", new FolderContents("Folder1"), new ModelData("Model1")
            }
        };

        [SetUp]
        public void Setup() {
            _serverClient = new ServerClientBuilder()
                .SetServerName(ServerName)
                .SetServerVersion(ServerVersion)
                .Build();
        }

        [TearDown]
        public void Teardown() {
            _serverClient?.Dispose();
            _serverClient = null;
        }

        [Test]
        public async Task ServerPropertiesTest() {
            ServerProperties serverProperties = await _serverClient.GetServerPropertiesAsync();

            Assert.Multiple(() => {
                Assert.That(serverProperties.MachineName, Is.EqualTo(ServerName));
                Assert.That(serverProperties.MaximumModelNameLength, Is.EqualTo(40));
                Assert.That(serverProperties.MaximumFolderPathLength, Is.EqualTo(98));

                Assert.That(new[] { ServerName }, Is.EqualTo(serverProperties.Servers));
                Assert.That(new[] { ServerRole.Host, ServerRole.Accelerator, ServerRole.Admin },
                    Is.EqualTo(serverProperties.ServerRoles));

                Assert.That(serverProperties.AccessLevelTypes, Is.Null);
            });
        }

        [Test]
        [TestCase("Вкладки")]
        public async Task FolderContentsTest(string folderPath) {
            FolderContents folderContents = await _serverClient.GetFolderContentsAsync(folderPath);

            Assert.Multiple(() => {
                Assert.That(folderPath, Is.EqualTo(folderContents.Path));
                Assert.That(folderContents.Models, Is.Empty);
                Assert.That(folderContents.Folders, Has.Count.EqualTo(4));
            });
        }

        [Test]
        [TestCase("Вкладки")]
        public async Task FolderInfoTest(string folderPath) {
            FolderInfoData folderInfoData = await _serverClient.GetFolderInfoAsync(folderPath);

            Assert.Multiple(() => {
                Assert.That(folderPath, Is.EqualTo(folderInfoData.Path));
                Assert.That(folderInfoData.Exists, Is.EqualTo(true));
                Assert.That(folderInfoData.IsFolder, Is.EqualTo(true));
            });
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task ModelHistoryTest(string modelPath) {
            ModelHistoryData modelHistoryData = await _serverClient.GetModelHistoryAsync(modelPath);

            Assert.That(modelPath, Is.EqualTo(modelHistoryData.Path));
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task ModelInfoTest(string modelPath) {
            ModelInfoData modelInfoData = await _serverClient.GetModelInfoAsync(modelPath);

            Assert.That(modelPath, Is.EqualTo(modelInfoData.Path));
            Assert.That(new Guid("4ed0d224-aef6-422c-9525-49a8bbe432d1"), Is.EqualTo(modelInfoData.ModelGuid));
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt", 96, 96)]
        public async Task GetModelThumbnailTest(string modelPath, int width, int height) {
            using(Stream modelThumbnail = await _serverClient.GetModelThumbnailAsync(modelPath, width, height)) {
                BitmapSource bitmap = BitmapFrame.Create(modelThumbnail);

                Assert.Multiple(() => {
                    Assert.That(width, Is.EqualTo((int) bitmap.Width));
                    Assert.That(height, Is.EqualTo((int) bitmap.Height));
                });
            }
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task ProjectInfoTest(string modelPath) {
            ProjectInfo projectInfo = await _serverClient.GetProjectInfoAsync(modelPath);

            Assert.That(projectInfo, Is.Not.Null);
        }

        [Test]
        public async Task RootFolderContentsTest() {
            FolderContents folderContents = await _serverClient.GetRootFolderContentsAsync();
            Assert.That(folderContents, Is.Not.Null);
            Assert.That(folderContents.Folders, Is.Not.Empty);
        }

        [Test]
        [TestCase()]
        public async Task RecursiveFolderContentsTest() {
            List<FolderContents> folderContents = await _serverClient.GetRecursiveFolderContentsAsync();
            Assert.That(folderContents, Is.Not.Empty);
        }

        [Test]
        [TestCase("PRKS-06")]
        [TestCase("UnitTests")]
        public async Task RecursiveFolderContentsTest(string folderPath) {
            List<FolderContents> folderContents = await _serverClient.GetRecursiveFolderContentsAsync(folderPath);
            Assert.That(folderContents, Is.Not.Empty);
        }

        [Test]
        [Order(0)]
        [TestCase(@"UnitTests\NewFolder")]
        public async Task CreateNewFolderTest(string folderPath) {
            await _serverClient.CreateNewFolderAsync(folderPath);
            Assert.That(await ExistsFolder(folderPath), Is.True);
        }

        [Test]
        [Order(1)]
        [TestCase(@"UnitTests\NewFolder", @"UnitTests\RenamedFolder", @"RenamedFolder")]
        public async Task RenameObjectTest(string folderPath, string newFolderPath, string renamedFolderName) {
            await _serverClient.RenameObjectAsync(folderPath, renamedFolderName);
            Assert.That(await ExistsFolder(newFolderPath), Is.True);
        }

        [Test]
        [Order(2)]
        [TestCase(@"UnitTests\RenamedFolder")]
        public async Task RemoveObjectTest(string folderPath) {
            await _serverClient.RemoveObjectAsync(folderPath);
            Assert.ThrowsAsync<HttpRequestException>(async () => await ExistsFolder(folderPath));
        }

        [Test]
        [TestCaseSource(nameof(_relativePathCases))]
        public void GetRelativePathCasesTest(string result, FolderContents folderContents, ObjectData objectData) {
            Assert.That(result, Is.EqualTo(folderContents.GetRelativeModelPath(objectData)));
        }

        [Test]
        [TestCaseSource(nameof(_visibleModelPathCases))]
        public void GetVisibleModelPathTest(string result, FolderContents folderContents, ModelData objectData) {
            string visibleModelPath = _serverClient.GetVisibleModelPath(folderContents, objectData);
            Assert.That(result, Is.EqualTo(visibleModelPath));
        }

        private async Task<bool> ExistsFolder(string folderPath) {
            FolderInfoData folderInfo = await _serverClient.GetFolderInfoAsync(folderPath);
            return folderInfo.Exists;
        }
    }
}