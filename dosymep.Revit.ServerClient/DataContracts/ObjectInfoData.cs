using System;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The object info data.
    /// </summary>
    public class ObjectInfoData : RelativePathData {
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
        /// The size of the model.
        /// </summary>
        public long ModelSize { set; get; }
    }
}