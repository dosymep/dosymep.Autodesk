using System;

namespace dosymep.Revit.FileInfo.BasicFileStream {
    /// <summary>
    /// This is the central model's version information.
    /// </summary>
    public class ModelVersionInfo {
        /// <summary>
        /// Empty version info.
        /// </summary>
        public static readonly ModelVersionInfo Empty = new ModelVersionInfo(default, default);

        /// <summary>
        /// Creates model version info.
        /// </summary>
        /// <param name="id">Identity.</param>
        /// <param name="versionNumber">Version number.</param>
        internal ModelVersionInfo(Guid id, int versionNumber) {
            Id = id;
            VersionNumber = versionNumber;
        }

        /// <summary>
        /// This is the central model's episode GUID corresponding to the last reload latest done for this model.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// This is the central model's version number corresponding to the last reload latest done for this model.
        /// </summary>
        public int VersionNumber { get; }

        /// <inheritdoc />
        public override string ToString() {
            return $"{VersionNumber} - {Id}";
        }
    }
}