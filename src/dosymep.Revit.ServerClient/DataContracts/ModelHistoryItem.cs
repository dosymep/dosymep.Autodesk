namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The model history.
/// </summary>
public class ModelHistoryItem {
    /// <summary>
    ///     The date and time the submission was made to the model.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     The user who made the submission.
    /// </summary>
    public string User { get; set; }

    /// <summary>
    ///     The version of the model created by the submission.
    /// </summary>
    public int VersionNumber { get; set; }

    /// <summary>
    ///     The size of the model at the time the submission was made.
    /// </summary>
    public long ModelSize { get; set; }

    /// <summary>
    ///     The size of the auxiliary data (such as user temporary data)
    ///     for the model at the time the submission was made.
    /// </summary>
    public long SupportSize { get; set; }

    /// <summary>
    ///     The comment message created by the submission.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// </summary>
    public long OverwrittenByHistoryNumber { get; set; }
}