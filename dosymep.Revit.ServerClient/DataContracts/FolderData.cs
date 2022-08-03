using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The folder data.
    /// </summary>
    public class FolderData : ObjectData {
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