using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The folder contents.
/// </summary>
public class FolderContents : RelativePathData {
    /// <summary>
    ///     Constructs folder contents.
    /// </summary>
    /// <param name="path">The folder path..</param>
    [JsonConstructor]
    public FolderContents(string path)
        : base(path) {
    }

    /// <summary>
    ///     The list of sub-folders.
    /// </summary>
    public List<FolderData> Folders { get; set; }

    /// <summary>
    ///     The list of sub-models.
    /// </summary>
    public List<ModelData> Models { get; set; }

    /// <summary>
    ///     The total space in bytes of the drive where the folder exists.
    /// </summary>
    public long DriveSpace { get; set; }

    /// <summary>
    ///     The free space in bytes of the drive where the folder exists.
    /// </summary>
    public long DriveFreeSpace { get; set; }

    /// <summary>
    /// </summary>
    public List<FileData> Files { get; set; }

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