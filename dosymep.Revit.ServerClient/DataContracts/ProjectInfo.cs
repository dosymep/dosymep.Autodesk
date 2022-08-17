using System.Collections.Generic;
using System.Dynamic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The project information.
    /// </summary>
    public class ProjectInfo {
        /// <summary>
        /// The project parameters.
        /// </summary>
        public List<ParamInfoItem> Items { get; set; }
    }
}