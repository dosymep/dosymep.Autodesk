using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using dosymep.Revit.FileInfo.BasicFileInfos;
using dosymep.Revit.FileInfo.Transmissions;

namespace dosymep.Revit.FileInfo {
    /// <summary>
    /// Revit file info.
    /// </summary>
    public class RevitFileInfo {
        /// <summary>
        /// Enums revit files extensions.
        /// </summary>
        public static readonly IReadOnlyList<string> RevitFilesExtensions = new[] {".rvt", ".rfa", ".rte"};
        
        /// <summary>
        /// Creates revit file info.
        /// </summary>
        /// <param name="modelPath">Revit model file path.</param>
        public RevitFileInfo(string modelPath) {
            if(string.IsNullOrEmpty(modelPath)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(modelPath));
            }

            if(!File.Exists(modelPath)) {
                throw new ArgumentException("Revit document was not found.", nameof(modelPath));
            }

            if(!RevitFilesExtensions.Contains(Path.GetExtension(modelPath), StringComparer.CurrentCultureIgnoreCase)) {
                throw new ArgumentException(
                    $"Revit document have not valid extension, allowed document extensions \"{string.Join(", ", RevitFilesExtensions)}\".",
                    nameof(modelPath));
            }

            ModelPath = modelPath;
            BasicFileInfo = BasicFileInfo.ReadBasicFileInfo(ModelPath);
            TransmissionData = TransmissionData.ReadTransmissionData(ModelPath);
        }
        
        /// <summary>
        /// Revit model file path.
        /// </summary>
        public string ModelPath { get; }
        
        /// <summary>
        /// Basic file info.
        /// </summary>
        public BasicFileInfo BasicFileInfo { get; }
        
        /// <summary>
        /// Basic file info.
        /// </summary>
        public TransmissionData TransmissionData { get; }

        /// <summary>
        /// Updates transmission data.
        /// </summary>
        public void UpdateTransmissionData() {
            TransmissionData.WriteTransmissionData(ModelPath, TransmissionData);
        }
    }
}