using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The server properties.
/// </summary>
public class ServerProperties {
    /// <summary>
    ///     The name list of servers (not including accelerators) in the Revit Server Network.
    /// </summary>
    public List<string> Servers { get; set; }

    /// <summary>
    ///     The list of roles current server plays.
    /// </summary>
    public List<ServerRole> ServerRoles { get; set; }

    /// <summary>
    ///     The maximum folder path length that the server supports.
    /// </summary>
    public int MaximumFolderPathLength { get; set; }

    /// <summary>
    ///     The maximum model path length that the server supports.
    /// </summary>
    public int MaximumModelNameLength { get; set; }

    /// <summary>
    ///     The server's machine name.
    /// </summary>
    public string MachineName { get; set; }

    /// <summary>
    ///     The server's access level types.
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, List<string>> AccessLevelTypes { get; set; }
}