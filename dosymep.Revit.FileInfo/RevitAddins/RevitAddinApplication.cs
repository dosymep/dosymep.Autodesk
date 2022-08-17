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
        /// <returns>Returns revit addin application.</returns>
        public static RevitAddinApplication CreateAddinApplication(XmlNode addinElement) {
            return CreateRevitAddinItem<RevitAddinApplication>(addinElement);
        }
    }
}