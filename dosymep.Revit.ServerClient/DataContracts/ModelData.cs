using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model data.
    /// </summary>
    public class ModelData : ObjectData {
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
    }
}