namespace dosymep.Revit.FileInfo.BasicFileInfos
{
    /// <summary>
    /// This is application information.
    /// </summary>
    public class ApplicationInfo {
        /// <summary>
        /// Application build version.
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// File format version.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// The client application.
        /// </summary>
        public string ClientAppName { get; set; } = "RevitApplication";
    }
}