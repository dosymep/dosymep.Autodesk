using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace dosymep.Revit.FileInfo {
    /// <summary>
    /// Class provides basic file info.
    /// </summary>
    public class BasicFileInfo {
        /// <summary>
        /// Enums revit files extensions.
        /// </summary>
        public static string[] RevitFilesExtensions = new[] {".rvt", ".rfa"};

        /// <summary>
        /// Constructs basic file info.
        /// </summary>
        /// <param name="modelPath">Model file path.</param>
        public BasicFileInfo(string modelPath) {
            if(string.IsNullOrEmpty(modelPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(modelPath));
            }

            if(File.Exists(modelPath)) {
                throw new ArgumentException("Revit document was not found.", nameof(modelPath));
            }
            
            if(RevitFilesExtensions.Contains(Path.GetExtension(modelPath), StringComparer.CurrentCultureIgnoreCase)) {
                throw new ArgumentException($"Revit document have not valid extension, allowed document extensions \"{string.Join(", ", RevitFilesExtensions)}\".", nameof(modelPath));
            }

            ModelPath = modelPath;
        }

        /// <summary>
        /// Model file path.
        /// </summary>
        public string ModelPath { get; }
        
        /// <summary>
        /// Last save path.
        /// </summary>
        public string LastSavePath { get; }
        
        /// <summary>
        /// Central model file path.
        /// </summary>
        public string CentralModelPath { get; }

        /// <summary>
        /// File format version.
        /// </summary>
        public string Format { get; }
        
        /// <summary>
        /// True if model file is modified.
        /// </summary>
        public bool IsModified { get; }
        
        /// <summary>
        /// Is single user cloud model.
        /// </summary>
        public bool IsSingleUserCloudModel { get; }

        /// <summary>
        /// User name who save model file.
        /// </summary>
        public bool Username { get; }
        
        /// <summary>
        /// Author.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Project spark file.
        /// </summary>
        public int ProjectSparkFile { get; }
        
        /// <summary>
        /// Default open workset.
        /// </summary>
        private int DefaultOpenWorkset { get; }

        /// <summary>
        /// Model identity.
        /// </summary>
        public Guid? ModelIdentity { get; }
        
        /// <summary>
        /// Central model identity.
        /// </summary>
        public Guid? CentralModelIdentity { get; }

        /// <summary>
        /// Locale used when saved model file.
        /// </summary>
        public CultureInfo ModelLanguage { get; }
        
        /// <summary>
        /// Application information.
        /// </summary>
        public ApplicationInfo AppInfo { get; }

        /// <summary>
        /// Current model file version.
        /// </summary>
        public ModelVersionInfo CurrentModelVersion { get; }
        
        /// <summary>
        /// Central model file version.
        /// </summary>
        public ModelVersionInfo CentralModelVersion { get; }
    }

    /// <summary>
    /// This is application information.
    /// </summary>
    public class ApplicationInfo {
        /// <summary>
        /// Application build version.
        /// </summary>
        public string Build { get; }

        /// <summary>
        /// True if revit is x64.
        /// </summary>
        public bool Is64Bit { get; } = true;

        /// <summary>
        /// The client application.
        /// </summary>
        public string ClientAppName { get; } = "RevitApplication";
    }

    /// <summary>
    /// This is the central model's version information.
    /// </summary>
    public class ModelVersionInfo {
        /// <summary>
        /// This is the central model's episode GUID corresponding to the last reload latest done for this model.
        /// </summary>
        public Guid? Id { get; }
        
        /// <summary>
        /// This is the central model's version number corresponding to the last reload latest done for this model.
        /// </summary>
        public int VersionNumber { get; }
    }
}