using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using dosymep.Revit.FileInfo.RevitAddins;

using NUnit.Framework;

namespace dosymep.Revit.FileInfo.Tests {
    public class RevitAddinManifestTests {
        public static readonly string[] AssemblyPaths = new[] {
            @"C:\Program Files\Autodesk\Revit 2020",
            @"C:\Program Files\Autodesk\Revit 2020\SDA\bin",
            
            $@"C:\Users\{Environment.UserName}\AppData\Roaming\pyRevit\Extensions\BIM4Everyone.lib",
            $@"C:\Users\{Environment.UserName}\AppData\Roaming\pyRevit\Extensions\BIM4Everyone.lib\devexpress_libs\libs",
            
            $@"C:\Users\{Environment.UserName}\AppData\Roaming\pyRevit\Extensions\BIM4Everyone.lib\dosymep_libs\libs",
            $@"C:\Users\{Environment.UserName}\AppData\Roaming\pyRevit\Extensions\BIM4Everyone.lib\dosymep_libs\libs\2020",
            
            $@"C:\Users\{Environment.UserName}\AppData\Roaming\pyRevit-Master\bin",
            $@"C:\Users\{Environment.UserName}\AppData\Roaming\pyRevit-Master\bin\engines\IPY277"
        };
        
        [SetUp]
        public void Setup() {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
        }

        [TearDown]
        public void Teardown() {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
        }
        
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            string assemblyPath = GetAssemblyPath(args);
            return File.Exists(assemblyPath) ? Assembly.LoadFrom(assemblyPath) : null;
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args) {
            string assemblyPath = GetAssemblyPath(args);
            return File.Exists(assemblyPath) ? Assembly.ReflectionOnlyLoadFrom(assemblyPath) : Assembly.ReflectionOnlyLoad(args.Name);
        }

        private static string GetAssemblyPath(ResolveEventArgs args) {
            var assemblyName = new AssemblyName(args.Name);
            return AssemblyPaths
                .Select(item => Path.Combine(item, assemblyName.Name + ".dll"))
                .Where(item => File.Exists(item))
                .FirstOrDefault();
        }

        [Test]
        [TestCase(@"TestFiles\ModPlus.addin")]
        [TestCase(@"TestFiles\pyRevit.addin")]
        [TestCase(@"TestFiles\RevitLookup.addin")]
        [TestCase(@"TestFiles\Autodesk.AddInManager.addin")]
        public void GetRevitAddinManifestTest(string fullFilePath) {
            var manifest = RevitAddinManifest.GetAddinManifest(fullFilePath);
            Assert.NotNull(manifest);
        }

        [Test]
        [TestCase(@"TestFiles\Assemblies\RevitLookup\RevitLookup.dll")]
        [TestCase(@"TestFiles\Assemblies\RevitPlugins\RevitPlugins.dll")]
        [TestCase(@"TestFiles\Assemblies\RevitPlugins\RevitPlugins_2021.dll")]
        [TestCase(@"TestFiles\Assemblies\RevitPlugins\RevitPlugins_2022.dll")]
        [TestCase(@"TestFiles\Assemblies\RevitPlugins\RevitPlugins_2023.dll")]
        [TestCase(@"TestFiles\Assemblies\AddInManager\Autodesk.AddInManager.Command.dll")]
        [TestCase(@"TestFiles\Assemblies\pyRevit-Master\bin\engines\IPY277\pyRevitLoader.dll")]
        public void CreateRevitAddinManifestTests(string fullFilePath) {
            var manifest = RevitAddinManifest.CreateAddinManifest(fullFilePath);
            manifest.Save();
            File.Delete(manifest.FullName);
        }

        [Test]
        [TestCase(@"TestFiles\Assemblies\RevitLookup")]
        [TestCase(@"TestFiles\Assemblies\AddInManager")]
        [TestCase(@"TestFiles\Assemblies\RevitPlugins")]
        [TestCase(@"TestFiles\Assemblies\pyRevit-Master")]
        public void CreateRevitAddinManifestsTests(string rootDirectory) {
            var manifests = RevitAddinManifest.CreateAddinManifests(rootDirectory);
            foreach(RevitAddinManifest manifest in manifests) {
                manifest.Save();
                File.Delete(manifest.FullName);
            }
        }
    }
}