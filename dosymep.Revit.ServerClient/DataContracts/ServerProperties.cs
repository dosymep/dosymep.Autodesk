using System.Collections.Generic;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The server properties.
    /// </summary>
    public class ServerProperties {
        /// <summary>
        /// The name list of servers (not including accelerators) in the Revit Server Network.
        /// </summary>
        public List<string> Servers { set; get; }

        /// <summary>
        /// The list of roles current server plays.
        /// </summary>
        public List<ServerRole> ServerRoles { set; get; }

        /// <summary>
        /// The maximum folder path length that the server supports.
        /// </summary>
        public int MaximumFolderPathLength { set; get; }

        /// <summary>
        /// The maximum model path length that the server supports.
        /// </summary>
        public int MaximumModelNameLength { set; get; }

        /// <summary>
        /// The server's machine name. 
        /// </summary>
        public string MachineName { set; get; }
        
        /// <summary>
        /// The server's access level types.
        /// </summary>
        public Dictionary<string, List<string>> AccessLevelTypes { set; get; }
    }
}