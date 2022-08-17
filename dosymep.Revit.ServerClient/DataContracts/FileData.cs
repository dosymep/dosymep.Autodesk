using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The file data.
    /// </summary>
    public class FileData {
        /// <summary>
        /// Constructs file data.
        /// </summary>
        /// <param name="name">Name</param>
        [JsonConstructor]
        public FileData(string name) {
            Name = name;
        }
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }

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