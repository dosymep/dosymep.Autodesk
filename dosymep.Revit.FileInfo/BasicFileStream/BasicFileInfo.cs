using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using dosymep.Autodesk.FileInfo;
using dosymep.Revit.FileInfo.Internal;

using OpenMcdf;

namespace dosymep.Revit.FileInfo.BasicFileStream {
    /// <summary>
    /// Class provides basic file info.
    /// </summary>
    public class BasicFileInfo {
        /// <summary>
        /// Basic file info stream name.
        /// </summary>
        public static readonly string BasicFileInfoName = "BasicFileInfo";

        /// <summary>
        /// Last save path.
        /// </summary>
        public string LastSavePath { get; set; }

        /// <summary>
        /// Central model file path.
        /// </summary>
        public string CentralPath { get; set; }

        /// <summary>
        /// True if model file is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// True if model file is workshared.
        /// </summary>
        public bool IsWorkshared { get; set; }

        /// <summary>
        /// Worksharing type.
        /// </summary>
        public WorksharingType WorksharingType { get; set; }

        /// <summary>
        /// Is single user cloud model.
        /// </summary>
        public bool IsSingleUserCloudModel { get; set; }

        /// <summary>
        /// Is revit lite.
        /// </summary>
        public bool IsRevitLite { get; set; }

        /// <summary>
        /// User name who save model file.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Default open workset.
        /// </summary>
        public int DefaultOpenWorkset { get; set; }

        /// <summary>
        /// File version.
        /// </summary>
        public int FileVersion { get; set; }

        /// <summary>
        /// Locale used when saved model file.
        /// </summary>
        public LanguageCode FileLocale { get; set; } = LanguageCode.Unknown;

        /// <summary>
        /// Model identity.
        /// </summary>
        public ModelIdentity Identity { get; set; } = ModelIdentity.Empty;

        /// <summary>
        /// Central model identity.
        /// </summary>
        public ModelIdentity CentralIdentity { get; set; } = ModelIdentity.Empty;

        /// <summary>
        /// Application information.
        /// </summary>
        public ApplicationInfo AppInfo { get; set; } = new ApplicationInfo();

        /// <summary>
        /// Current model file version.
        /// </summary>
        public ModelVersionInfo CurrentVersion { get; set; } = ModelVersionInfo.Empty;

        /// <summary>
        /// Central model file version.
        /// </summary>
        public ModelVersionInfo CentralVersion { get; set; } = ModelVersionInfo.Empty;

        /// <summary>
        /// Reads basic file information.
        /// </summary>
        /// <param name="modelPath">Model path.</param>
        /// <returns>Returns basic file information if BasicFileInfo exists.</returns>
        /// <exception cref="ArgumentException"><paramref name="modelPath" /> is <see langword="null" />, <see cref="String.Empty"/> or not exists.</exception>
        internal static BasicFileInfo ReadBasicFileInfo(string modelPath) {
            if(string.IsNullOrEmpty(modelPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(modelPath));
            }

            if(!File.Exists(modelPath)) {
                throw new ArgumentException($"The {modelPath} is not exists.", nameof(modelPath));
            }

            using(CompoundFile cf = new CompoundFile(modelPath)) {
                if(cf.RootStorage.TryGetStream(BasicFileInfoName, out CFStream rawBasicInfoData)) {
                    byte[] bytes = rawBasicInfoData.GetData();
                    using(var stream = new MemoryStream(bytes)) {
                        using(var reader = new BinaryReader(stream, Encoding.Unicode)) {
                            return ReadBasicFileInfo(reader, new BasicFileInfo());
                        }
                    }
                }
            }

            return null;
        }

        private static BasicFileInfo ReadBasicFileInfo(BinaryReader reader, BasicFileInfo basicFileInfo) {
            basicFileInfo.FileVersion = reader.ReadInt32();
            basicFileInfo.IsWorkshared = reader.ReadBoolean();
            basicFileInfo.WorksharingType = basicFileInfo.IsWorkshared
                ? reader.ReadWorksharingType()
                : WorksharingType.NotEnabled;

            basicFileInfo.Username = reader.ReadValueString();
            basicFileInfo.CentralPath = reader.ReadValueString();

            if(basicFileInfo.FileVersion >= FormatConstants.Format) {
                basicFileInfo.AppInfo.Format = reader.ReadValueString();
                basicFileInfo.AppInfo.Build = reader.ReadValueString();
            } else {
                basicFileInfo.AppInfo.Build = reader.ReadValueString();
                basicFileInfo.AppInfo.Format = Regex.Match(basicFileInfo.AppInfo.Build, @"20\d\d").Value;
            }

            if(basicFileInfo.FileVersion >= FormatConstants.LastSavePath) {
                basicFileInfo.LastSavePath = reader.ReadValueString();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.DefaultOpenWorkset) {
                basicFileInfo.DefaultOpenWorkset = reader.ReadInt32();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.IsRevitLite) {
                basicFileInfo.IsRevitLite = reader.ReadBoolean();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.CentralIdentity) {
                basicFileInfo.CentralIdentity = reader.ReadIdentity();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.FileLocale) {
                basicFileInfo.FileLocale = reader.ReadLanguageCode();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.IsModified) {
                basicFileInfo.IsModified = reader.ReadBoolean();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.CentralVersion) {
                basicFileInfo.CentralVersion = reader.ReadCentralVersion();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.CurrentVersion) {
                basicFileInfo.CurrentVersion = reader.ReadCurrentVersion();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.Identity) {
                basicFileInfo.Identity = reader.ReadIdentity();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.IsSingleUserCloudModel) {
                basicFileInfo.IsSingleUserCloudModel = reader.ReadBoolean();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.Author) {
                basicFileInfo.Author = reader.ReadValueString();
            }

            if(basicFileInfo.FileVersion >= FormatConstants.ClientAppName) {
                basicFileInfo.AppInfo.ClientAppName = reader.ReadValueString();
            }

            return basicFileInfo;
        }

        /// <inheritdoc />
        public override string ToString() {
            var builder = new StringBuilder();
            builder.AppendLineFormat("Worksharing", WorksharingType);
            builder.AppendLineFormat("Username", Username);
            builder.AppendLineFormat("Central Model Path", CentralPath);

            if(FileVersion >= FormatConstants.Format) {
                builder.AppendLineFormat("Format", AppInfo.Format);
                builder.AppendLineFormat("Build", AppInfo.Build);
            } else {
                builder.AppendLineFormat("Revit Build", AppInfo.Build);
            }

            if(FileVersion >= FormatConstants.LastSavePath) {
                builder.AppendLineFormat("Last Save Path", LastSavePath);
            }

            if(FileVersion >= FormatConstants.DefaultOpenWorkset) {
                builder.AppendLineFormat("Open Workset Default", DefaultOpenWorkset);
            }

            if(FileVersion >= FormatConstants.IsRevitLite) {
                builder.AppendLineFormat("Revit LT File", IsRevitLite);
            }

            if(FileVersion >= FormatConstants.CentralIdentity) {
                builder.AppendLineFormat("Central Model Identity", CentralIdentity);
            }

            if(FileVersion >= FormatConstants.FileLocale) {
                builder.AppendLineFormat("Locale When Saved", FileLocale.Code);
            }

            if(FileVersion >= FormatConstants.IsModified) {
                builder.AppendLineFormat("All Local Changes Saved To Central", IsModified);
            }

            if(FileVersion >= FormatConstants.CentralVersion) {
                builder.AppendLineFormat("Central model's version number corresponding to the last reload latest",
                    CentralVersion.VersionNumber);
                builder.AppendLineFormat("Central model's episode GUID corresponding to the last reload latest",
                    CentralVersion.Id);
            }

            if(FileVersion >= FormatConstants.CurrentVersion) {
                builder.AppendLine();
                builder.Append(
                    $"The unique document version identifier is {CurrentVersion.Id} for {CurrentVersion.VersionNumber} saves");
            }

            if(FileVersion >= FormatConstants.Identity) {
                builder.AppendLineFormat("Model Identity", Identity);
            }

            if(FileVersion >= FormatConstants.IsSingleUserCloudModel) {
                builder.AppendLineFormat("Model is singleUserCloudModel", IsSingleUserCloudModel);
            }

            if(FileVersion >= FormatConstants.Author) {
                builder.AppendLineFormat("Author", Author);
            }

            if(FileVersion >= FormatConstants.ClientAppName) {
                builder.AppendLineFormat("ClientAppName", AppInfo.ClientAppName);
            }

            return builder.ToString();
        }
    }
}