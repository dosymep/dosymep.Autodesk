using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The folder data.
    /// </summary>
    public class FolderData {
        /// <summary>
        /// The name of folder/model.
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Whether the folder has any contents.
        /// </summary>
        public bool HasContents { set; get; }

        /// <summary>
        /// The size in bytes of the folder.
        /// </summary>
        public long Size { set; get; }
        
        /// <summary>
        /// The lock state of the folder/model.
        /// </summary>
        public LockState LockState { set; get; }
        
        /// <summary>
        /// The context of the admin lock on the folder/model,
        /// describing the use of the admin lock such as copying or moving a folder
        /// from one server to another.
        /// </summary>
        public LockContext LockContext { set; get; }

        /// <summary>
        /// The list of descendant models that are locked by the Revit clients.
        /// </summary>
        public List<ModelLockData> ModelLocksInProgress { set; get; }
    }
}