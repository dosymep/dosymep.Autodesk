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

            if(!File.Exists(modelPath)) {
                throw new ArgumentException("Revit document was not found.", nameof(modelPath));
            }

            if(RevitFilesExtensions.Contains(Path.GetExtension(modelPath), StringComparer.CurrentCultureIgnoreCase)) {
                throw new ArgumentException(
                    $"Revit document have not valid extension, allowed document extensions \"{string.Join(", ", RevitFilesExtensions)}\".",
                    nameof(modelPath));
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
        public string LastSavePath { get; internal set; }

        /// <summary>
        /// Central model file path.
        /// </summary>
        public string CentralModelPath { get; internal set; }

        /// <summary>
        /// File format version.
        /// </summary>
        public string FormatVersion { get; internal set; }

        /// <summary>
        /// True if model file is modified.
        /// </summary>
        public bool IsModified { get; internal set; }

        /// <summary>
        /// Is single user cloud model.
        /// </summary>
        public bool IsSingleUserCloudModel { get; internal set; }

        /// <summary>
        /// User name who save model file.
        /// </summary>
        public bool Username { get; internal set; }

        /// <summary>
        /// Author.
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// Project spark file.
        /// </summary>
        public int ProjectSparkFile { get; internal set; }

        /// <summary>
        /// Default open workset.
        /// </summary>
        public int DefaultOpenWorkset { get; internal set; }

        /// <summary>
        /// Model identity.
        /// </summary>
        public Guid? ModelIdentity { get; internal set; }

        /// <summary>
        /// Central model identity.
        /// </summary>
        public Guid? CentralModelIdentity { get; internal set; }

        /// <summary>
        /// Locale used when saved model file.
        /// </summary>
        public LanguageCode ModelLanguage { get; internal set; }

        /// <summary>
        /// Application information.
        /// </summary>
        public ApplicationInfo AppInfo { get; internal set; }

        /// <summary>
        /// Current model file version.
        /// </summary>
        public ModelVersionInfo CurrentModelVersion { get; internal set; }

        /// <summary>
        /// Central model file version.
        /// </summary>
        public ModelVersionInfo CentralModelVersion { get; internal set; }
    }

    /// <summary>
    /// This is application information.
    /// </summary>
    public class ApplicationInfo {
        /// <summary>
        /// Application build version.
        /// </summary>
        public string Build { get; internal set; }

        /// <summary>
        /// True if revit is x64.
        /// </summary>
        public bool Is64Bit { get; internal set; } = true;

        /// <summary>
        /// The client application.
        /// </summary>
        public string ClientAppName { get; internal set; } = "RevitApplication";
    }

    /// <summary>
    /// This is the central model's version information.
    /// </summary>
    public class ModelVersionInfo {
        /// <summary>
        /// This is the central model's episode GUID corresponding to the last reload latest done for this model.
        /// </summary>
        public Guid? Id { get; internal set; }

        /// <summary>
        /// This is the central model's version number corresponding to the last reload latest done for this model.
        /// </summary>
        public int VersionNumber { get; internal set; }
    }
}