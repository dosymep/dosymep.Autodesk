namespace dosymep.Revit.Journaling;

/// <summary>
///     Revit journal templates.
/// </summary>
internal static class RevitJournalTemplates {
    /// <summary>
    ///     Initialization template Revit.
    /// </summary>
    /// {0} Date journal initialization.
    public static readonly string Init
        = @"' Revit {0} Journal by dosymep 
' 0:< 'C {1};

Dim Jrn
Set Jrn = CrsJournalScript";

    /// <summary>
    ///     Initialization debug mode template.
    /// </summary>
    public static readonly string InitDebug
        = @"
' Initialization debug mode
Jrn.Directive ""DebugMode"", ""PermissiveJournal"", True
Jrn.Directive ""DebugMode"", ""PerformAutomaticActionInErrorDialog"", True";

    /// <summary>
    ///     Exit revit application template.
    /// </summary>
    public static readonly string ExitApplication
        = @"
' Exit revit application
Jrn.Command ""SystemMenu"" , ""Quit the application; prompts to save projects , ID_APP_EXIT""";

    /// <summary>
    ///     Template automatically selects the "Do not save the project" option.
    /// </summary>
    public static readonly string PromptDoNotSaveFileWhenExit
        = @"
' Prompt does not save the file when exit
Jrn.Data  ""TaskDialogResult"", _
    ""You have made changes to model that have not been saved. What do you want to do?"",  _
        ""Do not save the project"", ""IDNO""";

    /// <summary>
    ///     Purge unused elements template.
    /// </summary>
    public static readonly string PurgeUnused
        = @"
' Purge unused elements
Jrn.Command ""Ribbon"" , ""Purge(delete) unused families and types, ID_PURGE_UNUSED""
Jrn.PushButton ""Modal , Purge unused , Dialog_Revit_PurgeUnusedTree"", ""OK, IDOK""";

    /// <summary>
    ///     Open a central model template.
    /// </summary>
    public static readonly string CentralOpen
        = @"
' Open central model
Jrn.Command ""Ribbon"" , ""Open an existing project , ID_REVIT_FILE_OPEN""";

    /// <summary>
    /// </summary>
    public static readonly string CentralModelName = @"
' Set Central file name
Jrn.Data ""File Name"" , ""IDOK"", ""{0}""";

    /// <summary>
    /// </summary>
    public static readonly string CentralWorksetConfig = @"
' Set workset config
Jrn.Data ""WorksetConfig"" , ""{0}"", {1}";

    /// <summary>
    /// </summary>
    public static readonly string CentralAcceptCustomWorksets = @"
' Confirm open worksets dialog 
Jrn.PushButton ""Modal , Opening Worksets , Dialog_Revit_Partitions"", ""OK, IDOK""";

    /// <summary>
    ///     Create a local file template.
    /// </summary>
    public static readonly string CentralOpenAsLocalCheckBox
        = @"
' Central open as local file template
Jrn.Data ""FileOpenSubDialog"", ""OpenAsLocalCheckBox"", ""True""";

    /// <summary>
    ///     Central open with detaching.
    /// </summary>
    public static readonly string CentralOpenDetachCheckBox
        = @"
' Central open with detach
Jrn.Data ""FileOpenSubDialog"", ""DetachCheckBox"", ""True""";

    /// <summary>
    ///     Central open with an audit template.
    /// </summary>
    public static readonly string CentralOpenAuditCheckBox
        = @"
' Central open with audit
Jrn.Data ""FileOpenSubDialog"", ""AuditCheckBox"", ""True""";

    /// <summary>
    ///     Save as a file command template.
    /// </summary>
    public static readonly string SaveAsFile
        = @"
' Save as file command
Jrn.Command ""Ribbon"", ""Save the active project with a new name , ID_REVIT_FILE_SAVE_AS""";

    /// <summary>
    ///     Save as file options template.
    /// </summary>
    public static readonly string SaveAsFileOptions
        = @"
' Save as file options
Jrn.Data  ""SaveOptionsData"", {0}, {1}, {2}, {3}, ""{4}""";

    /// <summary>
    ///     Save as a file name option.
    /// </summary>
    public static readonly string SaveAsFileNameOption
        = @"
' Save as file name option
Jrn.Data ""File Name"", ""IDOK"" , ""{0}""";

    /// <summary>
    ///     Save as make this a Central Model after save template
    /// </summary>
    public static readonly string SaveAsMakeThisFileCentalModel
        = @"
' Make this a Central Model after save
Jrn.Data ""BecomeCentralProject"", {0}";

    /// <summary>
    ///     Save as enable worksharing template
    /// </summary>
    public static readonly string SaveAsEnableWorksharing
        = @"
' Enable worksharing
Jrn.Data ""BecomeMultiUser"", {0}";

    /// <summary>
    ///     Replace central file template (replace on revit server)
    /// </summary>
    public static readonly string SaveAsReplaceCentralFile
        = @"
' Apply replace central file
Jrn.Data  ""TaskDialogResult"", _
        ""{0} already exists. What do you want to do?"", ""Replace the original central model"", ""1002""";

    /// <summary>
    ///     Replace workshring file template (replace on file system)
    /// </summary>
    public static readonly string SaveAsReplaceWorksharingFile
        = @"
' Apply replace worksharing
Jrn.Data ""TaskDialogResult"", _
        ""The file {0} already exists.  If you replace it, you will lose all of its backup versions. Do you want to replace the existing file?"", _
        ""Yes"", ""IDYES""";

    /// <summary>
    ///     Synchronization central model template.
    /// </summary>
    public static readonly string FileSync
        = @"
' Synchronization central model
Jrn.Command ""Ribbon"" , ""Save the active project back to the Central Model , ID_FILE_SAVE_TO_CENTRAL""";

    /// <summary>
    ///     Sync comment.
    /// </summary>
    public static readonly string FileSyncComment = @"
' Comments
Jrn.Edit ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Control_Revit_Comment"", ""ReplaceContents"" , ""{0}""";

    /// <summary>
    ///     Accept sync.
    /// </summary>
    public static readonly string FileSyncAccept = @"
' Assign synchronize with central dialog
Jrn.PushButton ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""OK, IDOK""";


    /// <summary>
    ///     Compact central model template.
    /// </summary>
    public static readonly string FileSyncCompactFile
        = @"
' Compact central model 
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Compact Central Model (slow), Control_Revit_ForceCompactCentralModel"", True";

    /// <summary>
    ///     Release borrowed elements template.
    /// </summary>
    public static readonly string FileSyncBorrowedElements
        = @"
' Release borrowed elements
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Borrowed Elements, Control_Revit_ReturnBorrowedElements"", True";

    /// <summary>
    ///     Release borrowed worksets template.
    /// </summary>
    public static readonly string FileSyncUserСreatedWorksets
        = @"
' Release borrowed worksets
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""User-created Worksets, Control_Revit_RelinqUserCreatedPartitions"", True";

    /// <summary>
    ///     Saving local file when sync template.
    /// </summary>
    public static readonly string FileSyncSaveLocalFile
        = @"
' Saving local file when sync
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Save Local File before and after synchronizing with central, Control_Revit_SavePartitionsToLocal"", True";

    /// <summary>
    ///     Execute external command template.
    /// </summary>
    public static readonly string ExecuteExternalCommand =
        @"
' Execute external command 
Jrn.RibbonEvent ""TabActivated:Add-Ins""
Jrn.RibbonEvent ""Execute external command:{0}:{1}""";

    /// <summary>
    ///     Execute external command journal data template.
    /// </summary>
    public static readonly string ExternalCommandJournalData =
        @"
' External command JournalData
Jrn.Data ""APIStringStringMapJournalData"" _";

    /// <summary>
    ///     Dynamo command execute template.
    /// </summary>
    public static readonly string DynamoCommandExecute =
        @"
' Launch dynamo
Jrn.RibbonEvent ""TabActivated:Manage""
Jrn.Command ""Ribbon"" , ""Launch Dynamo, ID_VISUAL_PROGRAMMING_DYNAMO""";

    #region Unresolved references dialog template

    /*
     *
     * ' 0:< TaskDialog "Revit could not find or read 3 references. What do you want to do?"
     * 'Id : TaskDialog_Unresolved_References
     * 'Command Links:
     * '1001 : Open Manage Links to correct the problem
     * '1002 : Ignore and continue opening the project
     * 'DefaultButton : 1001
     *
     */

    /// <summary>
    ///     Unresolved references dialog template.
    /// </summary>
    /// {0} Option text.
    /// {1} Option command ID.
    public static readonly string UnresolvedReferences
        = @"
' Unresolved references
Jrn.Data ""TaskDialogResult"", _
    ""Revit could not find or read some references. What do you want to do?"", _
        ""{0}"", ""{1}""";

    /// <summary>
    ///     Open manage links option text.
    /// </summary>
    public static readonly string UnresolvedReferencesOpenManageLinks = "Open Manage Links to correct the problem";

    /// <summary>
    ///     Ignore and continue option text.
    /// </summary>
    public static readonly string UnresolvedReferencesIgnore = "Ignore and continue opening the project";

    #endregion

    #region Loading transmitted file

    /*
     *
     * ' 0:< TaskDialog "This model has been transmitted from some other location. What do you want to do?"
     * 'Id : TaskDialog_Loading_Transmitted_File
     * 'CommonButtons : Cancel
     * 'Command Links:
     * '1001 : Save this model as a central model in its current location
     * '1002 : Work with this model temporarily
     * 'DefaultButton : 1001
     *
     */


    /// <summary>
    ///     Loading the transmitted file dialog template.
    /// </summary>
    /// {0} Option text.
    /// {1} Option command ID.
    public static readonly string LoadingTransmittedFile
        = @"
' Loading transmitted file
Jrn.Data ""TaskDialogResult"", _
    ""This model has been transmitted from some other location. What do you want to do?"", _
        ""{0}"", ""{1}""";

    /// <summary>
    ///     Save this model as a central model in its current location option text.
    /// </summary>
    public static readonly string LoadingTransmittedFileSaveAsCentral =
        "Save this model as a central model in its current location";

    /// <summary>
    ///     Work with this model temporarily option text.
    /// </summary>
    public static readonly string LoadingTransmittedFileWorkTemporarily = "Work with this model temporarily";

    /// <summary>
    ///     Cancel option ID.
    /// </summary>
    public static readonly string LoadingTransmittedFileCancelId = "IDCANCEL";

    /// <summary>
    ///     Cancel option text.
    /// </summary>
    public static readonly string LoadingTransmittedFileCancelText = "Cancel";

    #endregion

    #region Missing third party updaters dialog template

    /*
     *
     * ' 1:< TaskDialog "The file PROJECT-01 was modified by the third-party updaters PluginName which are not currently installed.
     * '
     * 'If you continue to edit the file, data maintained by PluginName will not be updated properly. This may create problems when PROJECT-01  is later opened when PluginName are present."
     * 'Id : TaskDialog_Missing_Third_Party_Updaters
     * 'Command Links:
     * '1001 : Continue working with the file.
     * '1002 : Do not warn about these updaters again and continue working with the file.
     * '1003 : Close without saving.
     * '1004 : Save the file under a different name and continue working.
     * 'DefaultButton : 1003
     *
     */


    /// <summary>
    ///     Missing third party updaters dialog template.
    /// </summary>
    /// {0} Model name/path.
    /// {1} Option text.
    /// {2} Option command ID.
    public static readonly string MissingThirdPartyUpdaters
        = @"
' Missing third party updaters
Jrn.Data ""TaskDialogResult"", _
    ""The file {0} was modified by the third-party updaters PluginName which are not currently installed."" & vbLf & """" & vbLf & ""If you continue to edit the file, data maintained by {1} will not be updated properly. This may create problems when {0} is later opened when {1} are present."", _
        ""{1}"", ""{2}""";

    /// <summary>
    ///     Continue working with the file option text.
    /// </summary>
    public static readonly string MissingThirdPartyUpdatersContinue = "Continue working with the file.";

    /// <summary>
    ///     Do not warn about these updaters again option text.
    /// </summary>
    public static readonly string MissingThirdPartyUpdatersDoNotWarnAgain =
        "Do not warn about these updaters again and continue working with the file.";

    /// <summary>
    ///     Close without saving option text.
    /// </summary>
    public static readonly string MissingThirdPartyUpdatersCloseWithoutSaving = "Close without saving.";

    /// <summary>
    ///     Save the file under a different name option text.
    /// </summary>
    public static readonly string MissingThirdPartyUpdatersSaveAsDifferentName =
        "Save the file under a different name and continue working.";

    #endregion
}