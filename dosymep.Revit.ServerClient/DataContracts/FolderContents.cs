using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The folder contents.
    /// </summary>
    public class FolderContents : RelativePathData {
        /// <summary>
        /// Constructs folder contents.
        /// </summary>
        /// <param name="path">The folder path..</param>
        [JsonConstructor]
        public FolderContents(string path)
            : base(path) {
        }
        
        /// <summary>
        /// The list of sub-folders.
        /// </summary>
        public List<FolderData> Folders { set; get; }

        /// <summary>
        /// The list of sub-models.
        /// </summary>
        public List<ModelData> Models { set; get; }

        /// <summary>
        /// The total space in bytes of the drive where the folder exists.
        /// </summary>
        public long DriveSpace { set; get; }

        /// <summary>
        /// The free space in bytes of the drive where the folder exists.
        /// </summary>
        public long DriveFreeSpace { set; get; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<FileData> Files { set; get; }
        
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