using System.IO;
using System.Threading.Tasks;

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
        public async Task GetFolderContentsTest() {
            string folderPath = "Вкладки";
            FolderContents folderContents = await _serverClient.GetFolderContentsAsync(folderPath);
            
            Assert.AreEqual(folderContents.Path, folderPath);
            Assert.AreEqual(folderContents.Models.Count, 0);
            Assert.AreEqual(folderContents.Folders.Count, 4);
        }
        
        [Test]
        public async Task GetDirectoryInformationTest() {
            string folderPath = "Вкладки";
            DirectoryData directoryData = await _serverClient.GetDirectoryInformationAsync(folderPath);
            
            Assert.AreEqual(directoryData.Path, folderPath);
            Assert.AreEqual(directoryData.Exists, true);
            Assert.AreEqual(directoryData.IsFolder, true);
        }
        
        [Test]
        public async Task GetModelHistoryTest() {
            string folderPath = Path.Combine("UnitTests", "ModelHistoryTest.rvt");
            ModelHistoryData modelHistoryData = await _serverClient.GetModelHistoryAsync(folderPath);
            
            Assert.AreEqual(modelHistoryData.Path, folderPath);
        }
    }
}