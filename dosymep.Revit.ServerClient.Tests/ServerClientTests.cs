using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using dosymep.Revit.ServerClient.DataContracts;

using NUnit.Framework;

namespace dosymep.Revit.ServerClient.Tests {
    public class ServerClientTests {
        private IServerClient _serverClient;

        [SetUp]
        public void Setup() {
            _serverClient = new ServerClientBuilder()
                .SetServerName("10.2.0.144")
                .SetServerVersion("2022")
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
    }
}