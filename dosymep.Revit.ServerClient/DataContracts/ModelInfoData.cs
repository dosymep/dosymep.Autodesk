using System;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model information data.
    /// </summary>
    public class ModelInfoData : RelativePathData {
        /// <summary>
        /// The creation time.
        /// </summary>
        public DateTime? DateCreated { set; get; }

        /// <summary>
        /// The last modification time.
        /// </summary>
        public DateTime? DateModified { set; get; }

        /// <summary>
        /// The user who did the last modification.
        /// </summary>
        public string LastModifiedBy { set; get; }
        
        /// <summary>
        /// The GUID of the model.
        /// </summary>
        public Guid ModelGuid { set; get; }

        /// <summary>
        /// The size of the model.
        /// </summary>
        public long ModelSize { set; get; }

        /// <summary>
        /// The size of the auxiliary data (such as user temporary data) for the model.
        /// </summary>
        public long SupportSize { set; get; }
    }
}