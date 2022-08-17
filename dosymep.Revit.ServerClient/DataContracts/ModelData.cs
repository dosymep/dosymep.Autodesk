using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model data.
    /// </summary>
    public class ModelData : ObjectData {
        /// <summary>
        /// Constructs model data.
        /// </summary>
        /// <param name="name">The name of folder/model.</param>
        [JsonConstructor]
        public ModelData(string name)
            : base(name) {
        }
        
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