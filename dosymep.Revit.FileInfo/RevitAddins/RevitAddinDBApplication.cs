using System.Xml;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents a Revit DB external application.
    /// </summary>
    public class RevitAddinDBApplication : RevitAddinItem {
        /// <summary>
        /// LoadInRevitWorkerTag
        /// </summary>
        public static readonly string LoadInRevitWorkerTag = "LoadInRevitWorker";

        /// <summary>
        /// Creates revit addin db application.
        /// </summary>
        /// <param name="addinElement">Addin element xml node.</param>
        /// <param name="addinManifest">Root addin manifest.</param>
        /// <returns>Returns revit addin db application.</returns>
        public static RevitAddinDBApplication CreateAddinDBApplication(XmlNode addinElement, RevitAddinManifest addinManifest) {
            RevitAddinDBApplication addinDBApplication = CreateRevitAddinItem<RevitAddinDBApplication>(addinElement, addinManifest);
            addinDBApplication.LoadInRevitWorker = addinElement.GetXmlNodeValue<bool>(LoadInRevitWorkerTag);
           
            return addinDBApplication;
        }

        /// <summary>
        /// Indicates whether or not a RevitWorker process will load this add-in.
        /// </summary>
        /// <remarks>The default is false.</remarks>
        public bool LoadInRevitWorker { get; set; }
    }
}