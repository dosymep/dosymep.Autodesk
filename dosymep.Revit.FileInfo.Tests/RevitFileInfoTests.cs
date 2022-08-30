using System;

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
        [TestCase(@"D:\OneDrive\OneDrive - A101 DEVELOPMENT\Рабочий стол\020_НАГ_ЭОМ_К10_отсоединено.rvt")]
        public void ReadFileTest(string fullFilePath) {
          var revitFileInfo =  new RevitFileInfo(fullFilePath);
          Assert.NotNull(revitFileInfo);
        }
    }
}