namespace dosymep.Revit.FileInfo.Transmissions {
    /// <summary>
    /// External file reference.
    /// </summary>
    public class ExternalFileReference {
        /// <summary>
        /// Linked file element id.
        /// </summary>
        public int ElementId { get; set; }

        /// <summary>
        /// Type linked file.
        /// </summary>
        public ExternalFileReferenceType ExternalFileReferenceType { get; set; }

        /// <summary>
        /// Linked file last saved path.
        /// </summary>
        public string LastSavedPath { get; set; }

        /// <summary>
        /// Linked file last saved absolute path.
        /// </summary>
        public string LastSavedAbsolutePath { get; set; }

        /// <summary>
        /// Linked file last saved path on server. 
        /// </summary>
        public string LastSavedCentralServerLocation { get; set; }

        /// <summary>
        /// Linked file last saved path type.
        /// </summary>
        public PathType LastSavedPathType { get; set; }

        /// <summary>
        /// Linked file last saved load state.
        /// </summary>
        public LoadState LastSavedLoadState { get; set; }

        /// <summary>
        /// Linked file desired path.
        /// </summary>
        public string DesiredPath { get; set; }

        /// <summary>
        /// Linked file desired path on server. 
        /// </summary>
        public string DesiredCentralServerLocation { get; set; }

        /// <summary>
        /// Linked file desired path type.
        /// </summary>
        public PathType DesiredPathType { get; set; }

        /// <summary>
        /// Linked file desired load state.
        /// </summary>
        public LoadState DesiredLoadState { get; set; }
        
        /// <inheritdoc />
        public override string ToString() {
            return $"ElementId: {ElementId}; RefType: {ExternalFileReferenceType}";
        }
    }
}
