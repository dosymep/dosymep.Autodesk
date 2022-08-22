using System.IO;
using System.Threading.Tasks;

using dosymep.Revit.FileInfo.RevitAddins;

using NUnit.Framework;

namespace dosymep.Revit.FileInfo.Tests {
    public class RevitAddinManifestTests {
        [SetUp]
        public void Setup() {

        }

        [TearDown]
        public void Teardown() {

        }

        [Test]
        [TestCase(@"TestFiles\ModPlus.addin")]
        [TestCase(@"TestFiles\pyRevit.addin")]
        [TestCase(@"TestFiles\RevitLookup.addin")]
        [TestCase(@"TestFiles\Autodesk.AddInManager.addin")]
        public void CreateRevitAddinManifestTest(string fullFilePath) {
            var manifest = RevitAddinManifest.CreateAddinManifest(fullFilePath);
            Assert.NotNull(manifest);
        }
    }
}