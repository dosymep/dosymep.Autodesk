using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The unlock descendents data.
    /// </summary>
    public class UnlockDescendentsData : RelativePathData {
        /// <summary>
        /// Constructs unlock descendents data.
        /// </summary>
        /// <param name="path">The folder path..</param>
        [JsonConstructor]
        protected UnlockDescendentsData(string path)
            : base(path) {
        }
        
        /// <summary>
        /// The list of paths of descendents that are not unlocked.
        /// </summary>
        public List<string> FailedItems { set; get; }
    }
}