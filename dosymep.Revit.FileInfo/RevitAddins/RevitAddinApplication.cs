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
        
        /// <inheritdoc />
        protected override string TypeName => RevitAddinManifest.AddInApplicationTag;
        
        /// <inheritdoc />
        protected override void FillXmlNodeImpl(XmlNode addinItemNode) { }
    }
}