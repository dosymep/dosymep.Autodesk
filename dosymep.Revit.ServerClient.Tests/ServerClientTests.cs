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

        private static readonly string _serverName = "10.2.0.144";
        private static readonly string _serverVersion = "2022";

        private static readonly object[] _relativePathCases = new object[] {
            new object[] {$@"Folder1\Folder2", new FolderContents(@"Folder1"), new FolderData("Folder2")},
            new object[] {$@"Folder1\Model1", new FolderContents(@"Folder1"), new ModelData("Model1")},
            new object[] {
                $@"Folder1\Folder2\Folder3", new FolderContents(@"Folder1\Folder2"), new FolderData("Folder3")
            },
            new object[] {$@"Folder1\Folder2\Model1", new FolderContents(@"Folder1\Folder2"), new ModelData("Model1")}
        };

        private static readonly object[] _visibleModelPathCases = new object[] {
            new object[] {
                $@"RSN://{_serverName}\Folder1\Model1", new FolderContents(@"Folder1"), new ModelData("Model1")
            }
        };

        [SetUp]
        public void Setup() {
            _serverClient = new ServerClientBuilder()
                .SetServerName(_serverName)
                .SetServerVersion(_serverVersion)
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

            Assert.AreEqual(serverProperties.MachineName, "REVIT-TEST");
            Assert.AreEqual(serverProperties.MaximumModelNameLength, 40);
            Assert.AreEqual(serverProperties.MaximumFolderPathLength, 98);

            Assert.AreEqual(serverProperties.Servers, new[] {"10.2.0.144"});
            Assert.AreEqual(serverProperties.ServerRoles,
                new[] {ServerRole.Host, ServerRole.Accelerator, ServerRole.Admin});

            Assert.AreEqual(serverProperties.AccessLevelTypes, null);
        }

        [Test]
        [TestCase("Вкладки")]
        public async Task FolderContentsTest(string folderPath) {
            FolderContents folderContents = await _serverClient.GetFolderContentsAsync(folderPath);

            Assert.AreEqual(folderContents.Path, folderPath);
            Assert.AreEqual(folderContents.Models.Count, 0);
            Assert.AreEqual(folderContents.Folders.Count, 4);
        }

        [Test]
        [TestCase("Вкладки")]
        public async Task FolderInfoTest(string folderPath) {
            FolderInfoData folderInfoData = await _serverClient.GetFolderInfoAsync(folderPath);

            Assert.AreEqual(folderInfoData.Path, folderPath);
            Assert.AreEqual(folderInfoData.Exists, true);
            Assert.AreEqual(folderInfoData.IsFolder, true);
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task ModelHistoryTest(string modelPath) {
            ModelHistoryData modelHistoryData = await _serverClient.GetModelHistoryAsync(modelPath);

            Assert.AreEqual(modelHistoryData.Path, modelPath);
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task ModelInfoTest(string modelPath) {
            ModelInfoData modelInfoData = await _serverClient.GetModelInfoAsync(modelPath);

            Assert.AreEqual(modelInfoData.Path, modelPath);
            Assert.AreEqual(modelInfoData.ModelGuid, new Guid("4ed0d224-aef6-422c-9525-49a8bbe432d1"));
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt", 96, 96)]
        public async Task GetModelThumbnailTest(string modelPath, int width, int height) {
            using(Stream modelThumbnail = await _serverClient.GetModelThumbnailAsync(modelPath, width, height)) {
                BitmapSource bitmap = BitmapFrame.Create(modelThumbnail);

                Assert.AreEqual((int) bitmap.Width, width);
                Assert.AreEqual((int) bitmap.Height, height);
            }
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task ProjectInfoTest(string modelPath) {
            ProjectInfo projectInfo = await _serverClient.GetProjectInfoAsync(modelPath);

            Assert.AreNotEqual(projectInfo, null);
        }

        [Test]
        public async Task RootFolderContentsTest() {
            FolderContents folderContents = await _serverClient.GetRootFolderContentsAsync();
            Assert.AreNotEqual(folderContents, null);
            Assert.Greater(folderContents.Folders.Count, 0);
        }

        [Test]
        [TestCase()]
        public async Task RecursiveFolderContentsTest() {
            List<FolderContents> folderContents = await _serverClient.GetRecursiveFolderContentsAsync();
            Assert.Greater(folderContents.Count, 0);
        }

        [Test]
        [TestCase("PRKS-06")]
        [TestCase("UnitTests")]
        public async Task RecursiveFolderContentsTest(string folderPath) {
            List<FolderContents> folderContents = await _serverClient.GetRecursiveFolderContentsAsync(folderPath);
            Assert.Greater(folderContents.Count, 0);
        }

        [Test]
        [Order(0)]
        [TestCase(@"UnitTests\NewFolder")]
        public async Task CreateNewFolderTest(string folderPath) {
            await _serverClient.CreateNewFolderAsync(folderPath);
            Assert.IsTrue(await ExistsFolder(folderPath));
        }

        [Test]
        [Order(1)]
        [TestCase(@"UnitTests\NewFolder", @"UnitTests\RenamedFolder", @"RenamedFolder")]
        public async Task RenameObjectTest(string folderPath, string newFolderPath, string renamedFolderName) {
            await _serverClient.RenameObjectAsync(folderPath, renamedFolderName);
            Assert.IsTrue(await ExistsFolder(newFolderPath));
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
            Assert.AreEqual(folderContents.GetRelativeModelPath(objectData), result);
        }

        [Test]
        [TestCaseSource(nameof(_visibleModelPathCases))]
        public void GetVisibleModelPathTest(string result, FolderContents folderContents, ModelData objectData) {
            string visibleModelPath = _serverClient.GetVisibleModelPath(folderContents, objectData);
            Assert.AreEqual(visibleModelPath, result);
        }

        public async Task<bool> ExistsFolder(string folderPath) {
            FolderInfoData folderInfo = await _serverClient.GetFolderInfoAsync(folderPath);
            return folderInfo.Exists;
        }
    }
}