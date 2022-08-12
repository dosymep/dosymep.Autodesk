using System;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model information data.
    /// </summary>
    public class ModelInfoData : ObjectInfoData {
        /// <summary>
        /// Constructs model info data.
        /// </summary>
        /// <param name="path">The folder path..</param>
        [JsonConstructor]
        protected ModelInfoData(string path)
            : base(path) {
        }
        
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