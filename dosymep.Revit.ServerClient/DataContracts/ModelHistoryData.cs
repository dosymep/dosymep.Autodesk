using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The model history data.
    /// </summary>
    public class ModelHistoryData : RelativePathData {
        /// <summary>
        /// The list of a model’s submission history.
        /// </summary>
        public List<ModelHistoryItem> Items { set; get; }
    }
}