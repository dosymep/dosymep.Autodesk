namespace dosymep.Revit.FileInfo
{
    /// <summary>
    /// Worksharing types.
    /// </summary>
    public enum WorksharingType {
        /// <summary>
        /// Not enable worksharing.
        /// </summary>
        NotEnabled,

        /// <summary>
        /// Enable worksharing.
        /// </summary>
        Central,

        /// <summary>
        /// Created local file.
        /// </summary>
        Local,

        /// <summary>
        /// In progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// Created local from RS (RevitServerToolCommand).
        /// </summary>
        CreatedLocal
    }
}