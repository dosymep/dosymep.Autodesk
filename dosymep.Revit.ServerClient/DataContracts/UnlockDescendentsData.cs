using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The unlock descendents data.
    /// </summary>
    public class UnlockDescendentsData : RelativePathData{
        /// <summary>
        /// The list of paths of descendents that are not unlocked.
        /// </summary>
        public List<string> FailedItems { set; get; }
    }
}