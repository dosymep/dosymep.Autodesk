using System;

using dosymep.AutodeskApps.FileInfo;
using dosymep.Revit.FileInfo.BasicFileStream;

using NUnit.Framework;

namespace dosymep.Revit.FileInfo.Tests {
    public class RevitFileInfoTests {
        [SetUp]
        public void Setup() {

        }

        [TearDown]
        public void Teardown() {

        }

        [Test]
        [TestCase(@"TestFiles\RVT\test_file.rvt")]
        public void ReadFileTest(string fullFilePath) {
            var revitFileInfo = new RevitFileInfo(fullFilePath);
            Assert.AreEqual(revitFileInfo.ModelPath, fullFilePath);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralPath, @"D:\Projects\Autodesk\dosymep.Autodesk\dosymep.Revit.FileInfo.Tests\TestFiles\RVT\test_file.rvt");
            Assert.AreEqual(revitFileInfo.BasicFileInfo.LastSavePath, @"D:\Projects\Autodesk\dosymep.Autodesk\dosymep.Revit.FileInfo.Tests\TestFiles\RVT\test_file.rvt");
            
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsModified, true);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsRevitLite, false);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsWorkshared, true);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsSingleUserCloudModel, false);
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.Username, null);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.FileVersion, 14);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.DefaultOpenWorkset, 0);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.FileLocale, LanguageCode.ENU);
            
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralVersion.Id, new Guid("e264af0c-75e2-4067-9e1c-d6c5517e21c7"));
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralVersion.VersionNumber, 1);
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CurrentVersion.Id, new Guid("e264af0c-75e2-4067-9e1c-d6c5517e21c7"));
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CurrentVersion.VersionNumber, 1);
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.Identity,  new ModelIdentity(new Guid("face0000-1223-3344-4455-555666666333")));
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralIdentity, new ModelIdentity(new Guid("face0000-1223-3344-4455-555666666333")));
            
            Assert.AreEqual(revitFileInfo.TransmissionData.UserData, "");
            Assert.AreEqual(revitFileInfo.TransmissionData.Version, 5);
            Assert.AreEqual(revitFileInfo.TransmissionData.IsTransmitted, false);
            Assert.AreEqual(revitFileInfo.TransmissionData.ExternalFileReferences.Count, 2);

            Assert.NotNull(revitFileInfo);
        }
        
        [Test]
        [TestCase(@"TestFiles\RVT\test_file2.rvt")]
        public void ReadFileTest2(string fullFilePath) {
            var revitFileInfo = new RevitFileInfo(fullFilePath);
            Assert.AreEqual(revitFileInfo.ModelPath, fullFilePath);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralPath, null);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.LastSavePath, @"C:\Users\Antipin_m\Desktop\Нумерация по линии.Тест.rvt");

            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsModified, false);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsRevitLite, false);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsWorkshared, false);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.IsSingleUserCloudModel, false);
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.Username, null);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.FileVersion, 14);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.DefaultOpenWorkset, 3);
            Assert.AreEqual(revitFileInfo.BasicFileInfo.FileLocale, LanguageCode.RUS);
            
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralVersion.Id, new Guid("e30c18e2-f175-4f05-a814-c10bd9b910dd"));
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralVersion.VersionNumber, 2);
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CurrentVersion.Id, new Guid("e30c18e2-f175-4f05-a814-c10bd9b910dd"));
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CurrentVersion.VersionNumber, 2);
            
            Assert.AreEqual(revitFileInfo.BasicFileInfo.Identity, new ModelIdentity(new Guid("00000000-0000-0000-0000-000000000000")));
            Assert.AreEqual(revitFileInfo.BasicFileInfo.CentralIdentity, new ModelIdentity(new Guid("00000000-0000-0000-0000-000000000000")));
            
            Assert.AreEqual(revitFileInfo.TransmissionData.UserData, "");
            Assert.AreEqual(revitFileInfo.TransmissionData.Version, 5);
            Assert.AreEqual(revitFileInfo.TransmissionData.IsTransmitted, false);
            Assert.AreEqual(revitFileInfo.TransmissionData.ExternalFileReferences.Count, 2);

            Assert.NotNull(revitFileInfo);
        }
    }
}