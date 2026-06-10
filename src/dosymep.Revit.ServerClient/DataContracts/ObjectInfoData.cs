using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The object info data.
/// </summary>
public abstract class ObjectInfoData : RelativePathData {
    /// <summary>
    ///     Constructs object info data.
    /// </summary>
    /// <param name="path">The folder path..</param>
    [JsonConstructor]
    protected ObjectInfoData(string path)
        : base(path) {
    }

    /// <summary>
    ///     The creation time.
    /// </summary>
    public DateTime? DateCreated { get; set; }

    /// <summary>
    ///     The last modification time.
    /// </summary>
    public DateTime? DateModified { get; set; }

    /// <summary>
    ///     The user who did the last modification.
    /// </summary>
    public string LastModifiedBy { get; set; }

    /// <summary>
    ///     The size of the model.
    /// </summary>
    public long ModelSize { get; set; }
}