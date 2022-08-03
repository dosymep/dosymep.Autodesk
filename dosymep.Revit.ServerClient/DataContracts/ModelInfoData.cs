using System;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model information data.
    /// </summary>
    public class ModelInfoData : ObjectInfoData {
        /// <summary>
        /// The GUID of the model.
        /// </summary>
        public Guid ModelGuid { set; get; }

        /// <summary>
        /// The size of the auxiliary data (such as user temporary data) for the model.
        /// </summary>
        public long SupportSize { set; get; }
    }
}