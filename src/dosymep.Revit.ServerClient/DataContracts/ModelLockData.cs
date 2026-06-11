namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The model lock data.
/// </summary>
public class ModelLockData {
    /// <summary>
    ///     The age of the model lock.
    /// </summary>
    public TimeSpan Age { get; set; }

    /// <summary>
    ///     The time stamp of the model lock.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    ///     The type of the model lock.
    /// </summary>
    public ModelLockType ModelLockType { get; set; }

    /// <summary>
    ///     The combination of the model lock options
    /// </summary>
    public ModelLockOptions ModelLockOptions { get; set; }

    /// <summary>
    ///     The user who locks the model.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     The model path.
    /// </summary>
    public string ModelPath { get; set; }
}