using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The directory data.
/// </summary>
public class FolderInfoData : ObjectInfoData {
    /// <summary>
    ///     Constructs folder info data.
    /// </summary>
    /// <param name="path">The folder path..</param>
    [JsonConstructor]
    protected FolderInfoData(string path)
        : base(path) {
    }

    /// <summary>
    ///     The size of the folder.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    ///     The count of sub-folders under the folder.
    /// </summary>
    public int FolderCount { get; set; }

    /// <summary>
    ///     The count of models under the folder.
    /// </summary>
    public int ModelCount { get; set; }

    /// <summary>
    ///     Whether the folder is a normal folder and not a model’s folder.
    /// </summary>
    public bool IsFolder { get; set; }

    /// <summary>
    ///     Whether the folder exists.
    /// </summary>
    public bool Exists { get; set; }

    /// <summary>
    ///     The lock state of the folder/model.
    /// </summary>
    public LockState LockState { get; set; }

    /// <summary>
    ///     The context of the admin lock on the folder/model,
    ///     describing the use of the admin lock such as copying or moving a folder
    ///     from one server to another.
    /// </summary>
    public LockContext LockContext { get; set; }

    /// <summary>
    ///     The list of descendant models that are locked by the Revit clients.
    /// </summary>
    public List<ModelLockData> ModelLocksInProgress { get; set; }
}