using dosymep.AutodeskApps;

namespace dosymep.Revit.Journaling.JournalElements;

/// <summary>
///     Unresolved references dialog buttons.
/// </summary>
public enum UnresolvedReferences {
    /// <summary>
    ///     Skip journal recording for this dialog.
    /// </summary>
    Skip = 0,

    /// <summary>
    /// Represents the default behavior for unresolved references in a Revit journaling context.
    /// </summary>
    Default = Open,

    /// <summary>
    ///     Open Manage Links to correct the problem.
    /// </summary>
    Open = 1001,

    /// <summary>
    ///     Ignore and continue opening the project.
    /// </summary>
    Ignore = 1002
}

/// <summary>
///     Loading transmitted file dialog buttons.
/// </summary>
public enum LoadingTransmittedFile {
    /// <summary>
    ///     Skip journal recording for this dialog.
    /// </summary>
    Skip = 0,

    /// <summary>
    /// Cancel the operation and close the dialog.
    /// </summary>
    Cancel = 1,

    /// <summary>
    /// Default action for the dialog when loading a transmitted file.
    /// </summary>
    Default = Save,

    /// <summary>
    ///     Save this model as a central model in its current location.
    /// </summary>
    Save = 1001,

    /// <summary>
    ///     Work with this model temporarily.
    /// </summary>
    OpenTemporary = 1002
}

/// <summary>
///     Missing third party updaters dialog buttons.
/// </summary>
public enum MissingThirdPartyUpdaters {
    /// <summary>
    ///     Skip journal recording for this dialog.
    /// </summary>
    Skip = 0,

    /// <summary>
    /// Represents the default action for the missing third party updaters dialog.
    /// </summary>
    Default = CloseWithoutSaving,

    /// <summary>
    ///     Continue working with the file.
    /// </summary>
    ContinueWorking = 1001,

    /// <summary>
    ///     Do not warn about these updaters again and continue working with the file.
    /// </summary>
    DoNotWarn = 1002,

    /// <summary>
    ///     Close without saving.
    /// </summary>
    CloseWithoutSaving = 1003,

    /// <summary>
    ///     Save the file under a different name and continue working.
    /// </summary>
    SaveUnderDifferentName = 1004,
}

/// <summary>
///     Open central model journal element.
/// </summary>
public class OpenCentralModelElement : JournalElement {
    /// <summary>
    ///     Constructs open central model journal element.
    /// </summary>
    public OpenCentralModelElement()
        : base("Open workshared model.") {
    }

    /// <summary>
    ///     Shows that a model with working sets is being opened default true.
    /// </summary>
    public bool IsWorksharedModel { get; set; } = true;

    /// <summary>
    ///     If true detaching central model.
    /// </summary>
    public bool Detach { get; set; } = false;

    /// <summary>
    ///     If true open central model with audit.
    /// </summary>
    public bool WithAudit { get; set; } = true;

    /// <summary>
    ///     If true create local file.
    /// </summary>
    public bool CreateLocal { get; set; } = false;

    /// <summary>
    ///     Worksets options.
    /// </summary>
    public WorksetsOption WorksetOption { get; set; } = WorksetsOption.All;

    /// <summary>
    /// Specifies the behavior for resolving unresolved references encountered during the process of opening a central model.
    /// </summary>
    public UnresolvedReferences UnresolvedReferences { get; set; } = UnresolvedReferences.Skip;

    /// <summary>
    /// Specifies the action to perform when handling a transmitted file during the operation.
    /// </summary>
    public LoadingTransmittedFile LoadingTransmittedFile { get; set; } = LoadingTransmittedFile.Skip;

    /// <summary>
    /// Specifies the action to take when a dialog regarding missing third-party updaters is encountered
    /// while processing a journal element.
    /// </summary>
    public MissingThirdPartyUpdaters MissingThirdPartyUpdaters { get; set; } = MissingThirdPartyUpdaters.Skip;

    /// <summary>
    ///     Model path.
    /// </summary>
    public string ModelPath { get; set; }

    /// <inheritdoc />
    public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
        if(transformer is ITransformer<T, OpenCentralModelElement> openSharedModelTransform) {
            return openSharedModelTransform.Transform(this);
        }

        return default;
    }

    internal static string GetDialogButtonText(UnresolvedReferences unresolvedReferences) {
        return unresolvedReferences switch {
            UnresolvedReferences.Open => RevitJournalTemplates.UnresolvedReferencesOpenManageLinks,
            UnresolvedReferences.Ignore => RevitJournalTemplates.UnresolvedReferencesIgnore,
            _ => throw new ArgumentOutOfRangeException(nameof(unresolvedReferences), unresolvedReferences, null)
        };
    }

    internal static string GetDialogButtonText(LoadingTransmittedFile loadingTransmittedFile) {
        return loadingTransmittedFile switch {
            LoadingTransmittedFile.Cancel => RevitJournalTemplates.LoadingTransmittedFileCancelText,
            LoadingTransmittedFile.Save => RevitJournalTemplates.LoadingTransmittedFileSaveAsCentral,
            LoadingTransmittedFile.OpenTemporary => RevitJournalTemplates.LoadingTransmittedFileWorkTemporarily,
            _ => throw new ArgumentOutOfRangeException(nameof(loadingTransmittedFile), loadingTransmittedFile, null)
        };
    }

    internal static string GetDialogButtonText(MissingThirdPartyUpdaters missingThirdPartyUpdaters) {
        return missingThirdPartyUpdaters switch {
            MissingThirdPartyUpdaters.CloseWithoutSaving => RevitJournalTemplates.MissingThirdPartyUpdatersCloseWithoutSaving,
            MissingThirdPartyUpdaters.ContinueWorking => RevitJournalTemplates.MissingThirdPartyUpdatersContinue,
            MissingThirdPartyUpdaters.DoNotWarn => RevitJournalTemplates.MissingThirdPartyUpdatersDoNotWarnAgain,
            MissingThirdPartyUpdaters.SaveUnderDifferentName => RevitJournalTemplates.MissingThirdPartyUpdatersSaveAsDifferentName,
            _ => throw new ArgumentOutOfRangeException(nameof(missingThirdPartyUpdaters), missingThirdPartyUpdaters, null)
        };
    }
}