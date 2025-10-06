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
            RevitFileInfo revitFileInfo = new RevitFileInfo(fullFilePath);
            Assert.Multiple(() => {
                Assert.That(fullFilePath, Is.EqualTo(revitFileInfo.ModelPath));
                Assert.That(revitFileInfo.BasicFileInfo.CentralPath,
                    Is.EqualTo(
                        @"D:\Projects\Autodesk\dosymep.Autodesk\dosymep.Revit.FileInfo.Tests\TestFiles\RVT\test_file.rvt"));
                Assert.That(revitFileInfo.BasicFileInfo.LastSavePath,
                    Is.EqualTo(
                        @"D:\Projects\Autodesk\dosymep.Autodesk\dosymep.Revit.FileInfo.Tests\TestFiles\RVT\test_file.rvt"));


                Assert.That(revitFileInfo.BasicFileInfo.IsModified, Is.True);
                Assert.That(revitFileInfo.BasicFileInfo.IsRevitLite, Is.False);
                Assert.That(revitFileInfo.BasicFileInfo.IsWorkshared, Is.True);
                Assert.That(revitFileInfo.BasicFileInfo.IsSingleUserCloudModel, Is.False);

                Assert.That(revitFileInfo.BasicFileInfo.Username, Is.Null);
                Assert.That(revitFileInfo.BasicFileInfo.FileVersion, Is.EqualTo(14));
                Assert.That(revitFileInfo.BasicFileInfo.DefaultOpenWorkset, Is.Zero);
                Assert.That(LanguageCode.ENU, Is.EqualTo(revitFileInfo.BasicFileInfo.FileLocale));


                Assert.That(new Guid("e264af0c-75e2-4067-9e1c-d6c5517e21c7"),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.CentralVersion.Id));
                Assert.That(revitFileInfo.BasicFileInfo.CentralVersion.VersionNumber, Is.EqualTo(1));
            });

            Assert.Multiple(() => {
                Assert.That(new Guid("e264af0c-75e2-4067-9e1c-d6c5517e21c7"),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.CurrentVersion.Id));
                Assert.That(revitFileInfo.BasicFileInfo.CurrentVersion.VersionNumber, Is.EqualTo(1));

                Assert.That(new ModelIdentity(new Guid("face0000-1223-3344-4455-555666666333")),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.Identity));
            });
            
            Assert.Multiple(() => {
                Assert.That(new ModelIdentity(new Guid("face0000-1223-3344-4455-555666666333")),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.CentralIdentity));

                Assert.That(revitFileInfo.TransmissionData.UserData, Is.EqualTo(""));
                Assert.That(revitFileInfo.TransmissionData.Version, Is.EqualTo(5));
                Assert.That(revitFileInfo.TransmissionData.IsTransmitted, Is.False);
                Assert.That(revitFileInfo.TransmissionData.ExternalFileReferences, Has.Count.EqualTo(2));
            });

            Assert.That(revitFileInfo, Is.Not.Null);
        }

        [Test]
        [TestCase(@"TestFiles\RVT\test_file2.rvt")]
        public void ReadFileTest2(string fullFilePath) {
            RevitFileInfo revitFileInfo = new RevitFileInfo(fullFilePath);
            Assert.Multiple(() => {
                Assert.That(fullFilePath, Is.EqualTo(revitFileInfo.ModelPath));
                Assert.That(revitFileInfo.BasicFileInfo.CentralPath, Is.Null);
                Assert.That(revitFileInfo.BasicFileInfo.LastSavePath,
                    Is.EqualTo(@"C:\Users\Antipin_m\Desktop\Нумерация по линии.Тест.rvt"));

                Assert.That(revitFileInfo.BasicFileInfo.IsModified, Is.False);
                Assert.That(revitFileInfo.BasicFileInfo.IsRevitLite, Is.False);
                Assert.That(revitFileInfo.BasicFileInfo.IsWorkshared, Is.False);
                Assert.That(revitFileInfo.BasicFileInfo.IsSingleUserCloudModel, Is.False);

                Assert.That(revitFileInfo.BasicFileInfo.Username, Is.Null);
                Assert.That(revitFileInfo.BasicFileInfo.FileVersion, Is.EqualTo(14));
                Assert.That(revitFileInfo.BasicFileInfo.DefaultOpenWorkset, Is.EqualTo(3));
                Assert.That(LanguageCode.RUS, Is.EqualTo(revitFileInfo.BasicFileInfo.FileLocale));


                Assert.That(new Guid("e30c18e2-f175-4f05-a814-c10bd9b910dd"),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.CentralVersion.Id));
                Assert.That(revitFileInfo.BasicFileInfo.CentralVersion.VersionNumber, Is.EqualTo(2));
            });

            Assert.Multiple(() => {
                Assert.That(new Guid("e30c18e2-f175-4f05-a814-c10bd9b910dd"),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.CurrentVersion.Id));
                Assert.That(revitFileInfo.BasicFileInfo.CurrentVersion.VersionNumber, Is.EqualTo(2));

                Assert.That(new ModelIdentity(new Guid("00000000-0000-0000-0000-000000000000")),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.Identity));
            });
           
            Assert.Multiple(() => {
                Assert.That(new ModelIdentity(new Guid("00000000-0000-0000-0000-000000000000")),
                    Is.EqualTo(revitFileInfo.BasicFileInfo.CentralIdentity));

                Assert.That(revitFileInfo.TransmissionData.UserData, Is.EqualTo(""));
                Assert.That(revitFileInfo.TransmissionData.Version, Is.EqualTo(5));
                Assert.That(revitFileInfo.TransmissionData.IsTransmitted, Is.False);
                Assert.That(revitFileInfo.TransmissionData.ExternalFileReferences, Has.Count.EqualTo(2));
            });

            Assert.That(revitFileInfo, Is.Not.Null);
        }
    }
}