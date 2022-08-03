namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The file data.
    /// </summary>
    public class FileData {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Size
        /// </summary>
        public long Size { set; get; }

        /// <summary>
        /// IsText
        /// </summary>
        public bool IsText { set; get; }
    }
}