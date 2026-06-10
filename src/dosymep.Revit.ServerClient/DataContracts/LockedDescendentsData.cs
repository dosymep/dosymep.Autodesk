using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The locked descendents data.
    /// </summary>
    public class LockedDescendentsData : RelativePathData {
        /// <summary>
        /// Constructs locked descendents data.
        /// </summary>
        /// <param name="path">The folder path..</param>
        [JsonConstructor]
        public LockedDescendentsData(string path)
            : base(path) {
        }
        
        /// <summary>
        /// The list of paths of locked descendents.
        /// </summary>
        public List<string> Items { set; get; }

        /// <summary>
        /// Whether there is any locked descendents
        /// that has lock context describing the use of the admin lock
        /// such as copying or moving a folder from one server to another.
        /// </summary>
        public bool DescendentHasLockContext { set; get; }
    }
}