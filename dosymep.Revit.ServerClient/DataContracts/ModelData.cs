using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model data.
    /// </summary>
    public class ModelData {
        /// <summary>
        /// The name of folder/model.
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// The size in bytes of the model.
        /// </summary>
        public long ModelSize { set; get; }

        /// <summary>
        /// The size in bytes of the auxiliary data (such as user temporary data) for the model.
        /// </summary>
        public long SupportSize { set; get; }

        /// <summary>
        /// The version number of Revit that the model is lasted modified with.
        /// </summary>
        public ProductVersion ProductVersion { set; get; }
        
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