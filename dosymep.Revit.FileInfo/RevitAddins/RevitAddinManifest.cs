using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents the contents of a single .addin file.
    /// </summary>
    /// <remarks> The manifest contains a collection of external commands and external applications listed in the .addin file. </remarks>
    public class RevitAddinManifest {
        /// <summary>
        /// Revit addin file extension (.addin).
        /// </summary>
        public static readonly string AddinFileExt = ".addin";
        
        /// <summary>
        /// RevitAddInTag
        /// </summary>
        public static readonly string RevitAddInTag = "RevitAddIn";
        
        /// <summary>
        /// RevitAddInsTag
        /// </summary>
        public static readonly string RevitAddInsTag = "RevitAddIns";
        
        /// <summary>
        /// AddInTag
        /// </summary>
        public static readonly string AddInTag = "AddIn";
        
        /// <summary>
        /// AddInCommandTag
        /// </summary>
        public static readonly string AddInCommandTag = "Command";
        
        /// <summary>
        /// AddInApplicationTag
        /// </summary>
        public static readonly string AddInApplicationTag = "Application";
        
        /// <summary>
        /// AddInDBApplicationTag
        /// </summary>
        public static readonly string AddInDBApplicationTag = "DBApplication";
        
        /// <summary>
        /// Creates revit addin manifest (.addin file).
        /// </summary>
        /// <param name="fullFileName">Full file name to .addin file.</param>
        /// <returns>Returns revit addin manifest (.addin file).</returns>
        /// <exception cref="ArgumentException">When fullFileName is null or empty.</exception>
        public static RevitAddinManifest CreateAddinManifest(string fullFileName) {
            if(string.IsNullOrEmpty(fullFileName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(fullFileName));
            }

            if(!fullFileName.EndsWith(AddinFileExt, StringComparison.CurrentCultureIgnoreCase)) {
                throw new ArgumentException("File path is not valid .addin file.", nameof(fullFileName));
            }

            if(!File.Exists(fullFileName)) {
                throw new ArgumentException("File path is not exists.", nameof(fullFileName));
            }

            var document = new XmlDocument();
            document.Load(fullFileName);
            
            XmlElement documentElement = document.DocumentElement;
            if(documentElement == null || !(documentElement.Name.Equals(RevitAddInTag) || documentElement.Name.Equals(RevitAddInsTag))) {
                throw new ArgumentException($"The \"{documentElement?.Name ?? "null"}\" root tag is not valid. Tag should be {RevitAddInTag} or {RevitAddInsTag}", nameof(fullFileName));
            }

            XmlNode[] addinElements = documentElement.ChildNodes.OfType<XmlNode>()
                .Where(item=> item.NodeType == XmlNodeType.Element)
                .ToArray();
            
            if(addinElements.Length == 0) {
                throw new ArgumentException("Addin file does not have addin elements.", nameof(fullFileName));
            }

            var addinManifest = new RevitAddinManifest() {FullName = fullFileName};
            foreach (XmlNode addinElement in addinElements) {
                if(!addinElement.Name.Equals(AddInTag)) {
                    throw new ArgumentException($"The addin element is not valid. Tag should be {AddInTag}", nameof(fullFileName));
                }
                
                if(addinElement.Attributes == null || addinElement.Attributes.Count == 0 || addinElement.Attributes[RevitAddinItem.AddInTypeTag] == null) {
                    throw new ArgumentException($"The addin element does not have valid type. Tags should be {AddInCommandTag} or {AddInApplicationTag} or {AddInDBApplicationTag}", nameof(fullFileName));
                }

                XmlAttribute attribute = addinElement.Attributes[RevitAddinItem.AddInTypeTag];
                if(attribute.Value.Equals(AddInCommandTag)) {
                    addinManifest.AddinCommands.Add(RevitAddinCommand.CreateAddinCommand(addinElement, addinManifest));
                } else if (attribute.Value.Equals(AddInApplicationTag)) {
                    addinManifest.AddinApplications.Add(RevitAddinApplication.CreateAddinApplication(addinElement, addinManifest));
                } else if(attribute.Value.Equals(AddInDBApplicationTag)) {
                    addinManifest.AddinDBApplications.Add(RevitAddinDBApplication.CreateAddinDBApplication(addinElement, addinManifest));
                } else {
                    throw new NotSupportedException($"The {attribute.Value ?? "null"} addin type is not valid. Addin type should be {AddInCommandTag} or {AddInApplicationTag} or {AddInDBApplicationTag}.");
                }
            }

            return addinManifest;
        }

        /// <summary>
        /// Saved revit addin manifest file.
        /// </summary>
        /// <param name="fullFileName">Full file name path to save.</param>
        public void SaveAs(string fullFileName) {
            if(string.IsNullOrEmpty(fullFileName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(fullFileName));
            }

            if(!fullFileName.EndsWith(AddinFileExt, StringComparison.CurrentCultureIgnoreCase)) {
                throw new ArgumentException("File path is not valid .addin file.", nameof(fullFileName));
            }

            string directoryName = Path.GetDirectoryName(fullFileName);
            if(string.IsNullOrEmpty(directoryName)) {
                throw new ArgumentException("File path is not valid.", nameof(fullFileName));
            }
            
            if(!Directory.Exists(directoryName)) {
                Directory.CreateDirectory(directoryName);
            }

            var document = new XmlDocument();
            XmlNode rootNode = document.CreateAndAppendElement(RevitAddInsTag);
            
            foreach(RevitAddinItem addinItem in AddinItems) {
                addinItem.FillXmlNode(rootNode.CreateAndAppendElement(AddInTag));
            }

            document.Save(fullFileName);
        }

        /// <summary>
        /// Creates addin manifest by root path.
        /// </summary>
        /// <param name="rootPath">Root path.</param>
        /// <returns>Returns enums addin manifests.</returns>
        public static IEnumerable<RevitAddinManifest> CreateAddinManifests(string rootPath) {
            return CreateAddinManifests(rootPath, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Creates addin manifest by root path.
        /// </summary>
        /// <param name="rootPath">Root path.</param>
        /// <param name="searchOption">Search option in root path.</param>
        /// <returns>Returns enums addin manifests.</returns>
        public static IEnumerable<RevitAddinManifest> CreateAddinManifests(string rootPath, SearchOption searchOption) {
            if(string.IsNullOrEmpty(rootPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(rootPath));
            }

            if(!Directory.Exists(rootPath)) {
                throw new ArgumentException("Root path is not exists.", nameof(rootPath));
            }

            return Directory.GetFiles(rootPath, $"*{AddinFileExt}", searchOption)
                .Select(item => CreateAddinManifest(item));
        }

        /// <summary>
        /// The file name which is associated with the manifest.
        /// </summary>
        public string Name => Path.GetFileName(FullName);

        /// <summary>
        /// The full path and file name which is associated with the manifest.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The collection of all external commands in the manifest.
        /// </summary>
        public List<RevitAddinCommand> AddinCommands { get; } = new List<RevitAddinCommand>();

        /// <summary>
        /// The collection of all external applications in the manifest.
        /// </summary>
        public List<RevitAddinApplication> AddinApplications { get; } = new List<RevitAddinApplication>();

        /// <summary>
        /// The collection of all external DB applications in the manifest.
        /// </summary>
        public List<RevitAddinDBApplication> AddinDBApplications { get; } = new List<RevitAddinDBApplication>();

        /// <summary>
        /// Enums all revit addin items in the manifest.
        /// </summary>
        public IEnumerable<RevitAddinItem> AddinItems => AddinCommands
            .OfType<RevitAddinItem>()
            .Union(AddinApplications)
            .Union(AddinDBApplications);


        /// <summary>
        /// Saves revit addin manifest file.
        /// </summary>
        public void Save() {
            SaveAs(FullName);
        }
    }
}