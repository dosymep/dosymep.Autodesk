using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model history data.
    /// </summary>
    public class ModelHistoryData : RelativePathData {
        /// <summary>
        /// Constructs model history data.
        /// </summary>
        /// <param name="path">The folder path..</param>
        [JsonConstructor]
        public ModelHistoryData(string path)
            : base(path) {
        }
        
        /// <summary>
        /// The list of a model’s submission history.
        /// </summary>
        public List<ModelHistoryItem> Items { set; get; }
    }
}