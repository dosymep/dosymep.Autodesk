using System;
using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The directory data. 
    /// </summary>
    public class FolderInfoData : RelativePathData {
        /// <summary>
        /// The creation time.
        /// </summary>
        public DateTime? DateCreated { set; get; }

        /// <summary>
        /// The last modified time.
        /// </summary>
        public DateTime? DateModified { set; get; }

        /// <summary>
        /// The user who did the last modification.
        /// </summary>
        public string LastModifiedBy { set; get; }

        /// <summary>
        /// The size of the folder.
        /// </summary>
        public long Size { set; get; }

        /// <summary>
        /// The model size if the folder is a model’s folder.
        /// </summary>
        public long ModelSize { set; get; }

        /// <summary>
        /// The count of sub-folders under the folder.
        /// </summary>
        public int FolderCount { set; get; }

        /// <summary>
        /// The count of models under the folder.
        /// </summary>
        public int ModelCount { set; get; }

        /// <summary>
        /// Whether the folder is a normal folder and not a model’s folder.
        /// </summary>
        public bool IsFolder { set; get; }

        /// <summary>
        /// Whether the folder exists.
        /// </summary>
        public bool Exists { set; get; }

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