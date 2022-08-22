using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents a Revit external application.
    /// </summary>
    public class RevitAddinApplication : RevitAddinItem {
        /// <summary>
        /// Creates revit addin application.
        /// </summary>
        /// <param name="addinElement">Addin element xml node.</param>
        /// <param name="addinManifest">Root addin manifest.</param>
        /// <returns>Returns revit addin application.</returns>
        public static RevitAddinApplication CreateAddinApplication(XmlNode addinElement,
            RevitAddinManifest addinManifest) {
            return CreateRevitAddinItem<RevitAddinApplication>(addinElement, addinManifest);
        }
        
        /// <summary>
        /// Returns addin DB applications.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns> Returns addin DB applications.</returns>
        public static IEnumerable<RevitAddinApplication> GetAddinApplications(Assembly assembly) {
            return GetAddinItems<RevitAddinApplication>(assembly, ApplicationInterface);
        }
        
        /// <inheritdoc />
        protected override string TypeName => RevitAddinManifest.AddInApplicationTag;
        
        /// <inheritdoc />
        protected override string AssemblyName => AssemblyRevitApiUi;
        
        /// <inheritdoc />
        protected override string TypeInterfaceName => ApplicationInterface;

        /// <inheritdoc />
        protected override void FillXmlNodeImpl(XmlNode addinItemNode) { }
    }
}