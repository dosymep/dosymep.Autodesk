using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The folder data.
    /// </summary>
    public class FolderData : ObjectData {
        /// <summary>
        /// Constructs folder data.
        /// </summary>
        /// <param name="name">The name of folder/model.</param>
        [JsonConstructor]
        public FolderData(string name)
            : base(name) {
        }

        /// <summary>
        /// The size in bytes of the folder.
        /// </summary>
        public long Size { set; get; }
        
        /// <summary>
        /// Whether the folder has any contents.
        /// </summary>
        public bool HasContents { set; get; }
    }
}