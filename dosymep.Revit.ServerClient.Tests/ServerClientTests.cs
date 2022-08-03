using System;
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
        public async Task GetFolderContentsTest(string folderPath) {
            FolderContents folderContents = await _serverClient.GetFolderContentsAsync(folderPath);

            Assert.AreEqual(folderContents.Path, folderPath);
            Assert.AreEqual(folderContents.Models.Count, 0);
            Assert.AreEqual(folderContents.Folders.Count, 4);
        }

        [Test]
        [TestCase("Вкладки")]
        public async Task GetDirectoryInformationTest(string folderPath) {
            DirectoryData directoryData = await _serverClient.GetDirectoryInformationAsync(folderPath);

            Assert.AreEqual(directoryData.Path, folderPath);
            Assert.AreEqual(directoryData.Exists, true);
            Assert.AreEqual(directoryData.IsFolder, true);
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task GetModelHistoryTest(string modelPath) {
            ModelHistoryData modelHistoryData = await _serverClient.GetModelHistoryAsync(modelPath);

            Assert.AreEqual(modelHistoryData.Path, modelPath);
        }

        [Test]
        [TestCase(@"UnitTests\ModelHistoryTest.rvt")]
        public async Task GetModelInformationTest(string modelPath) {
            ModelInfoData modelInfoData = await _serverClient.GetModelInformationAsync(modelPath);

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
        public async Task GetProjectInfoTest(string modelPath) {
            ProjectInfo projectInfo = await _serverClient.GetProjectInfoAsync(modelPath);

            Assert.AreNotEqual(projectInfo, null);
        }
    }
}