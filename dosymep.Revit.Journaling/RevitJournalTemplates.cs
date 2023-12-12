namespace dosymep.Revit.Journaling {
    /// <summary>
    /// Revit journal templates.
    /// </summary>
    internal class RevitJournalTemplates {
        /// <summary>
        /// Initialization template Revit.
        /// </summary>
        /// <param name="{0}">Date journal initialization.</param>
        public static readonly string Init
            = @"' Revit {0} Journal by dosymep 
' 0:< 'C {1};
Dim Jrn
Set Jrn = CrsJournalScript";

        /// <summary>
        /// Initialization debug mode template.
        /// </summary>
        /// <param name="{0}">PerformAutomaticActionInErrorDialog.</param>
        /// <param name="{1}">PermissiveJournal.</param>
        public static readonly string InitDebug
            = @"' Initialization debug mode
Jrn.Directive ""DebugMode"", ""PerformAutomaticActionInErrorDialog"", True
Jrn.Directive ""DebugMode"", ""PermissiveJournal"", True";

        /// <summary>
        /// Exit revit application template.
        /// </summary>
        public static readonly string ExitApplication
            = @"' Exit revit application
Jrn.Command ""SystemMenu"" , ""Quit the application; prompts to save projects , ID_APP_EXIT""";

        /// <summary>
        /// Purge unused elements template.
        /// </summary>
        public static readonly string PurgeUnused
            = @"' Purge unused elements
Jrn.Command ""Ribbon"" , ""Purge(delete) unused families and types, ID_PURGE_UNUSED""
Jrn.PushButton ""Modal , Purge unused , Dialog_Revit_PurgeUnusedTree"", ""OK, IDOK""";

        /// <summary>
        /// Ignore missing links template.
        /// </summary>
        public static readonly string IgnoreMissingLinks
            = @"' Ignore missing links
Jrn.Data ""TaskDialogResult"",  _
    ""Revit could not find or read 1 references. What do you want to do?"",  _
        ""Ignore and continue opening the project"", ""1002""";

        /// <summary>
        /// Open central model template.
        /// </summary>
        /// <param name="{0}">Params template.</param>
        /// <param name="{1}">File name.</param>
        /// <param name="{2}">Open workset config.</param>
        public static readonly string CentralOpen
            = @"' Open central model
Jrn.Command ""Ribbon"" , ""Open an existing project , ID_REVIT_FILE_OPEN""
{0}
Jrn.Data ""File Name"" , ""IDOK"", ""{1}""
Jrn.Data ""WorksetConfig"" , ""Custom"", {2}
Jrn.PushButton ""Modal , Opening Worksets , Dialog_Revit_Partitions"", ""OK, IDOK""";

        /// <summary>
        /// Create local file template.
        /// </summary>
        public static readonly string CentralOpenAsLocalCheckBox
            = @"' Central open as local file template
Jrn.Data ""FileOpenSubDialog"", ""OpenAsLocalCheckBox"", ""True""";

        /// <summary>
        /// Central open with detach.
        /// </summary>
        public static readonly string CentralOpenDetachCheckBox
            = @"' Central open with detach
Jrn.Data ""FileOpenSubDialog"", ""DetachCheckBox"", ""True""";

        /// <summary>
        /// Central open with audit template.
        /// </summary>
        public static readonly string CentralOpenAuditCheckBox
            = @"' Central open with audit
Jrn.Data ""FileOpenSubDialog"", ""AuditCheckBox"", ""True""";

        /// <summary>
        /// Synchronization central model template.
        /// </summary>
        /// <param name="{0}">Params template.</param>
        /// <param name="{1}">Comments.</param>
        public static readonly string FileSync
            = @"' Synchronization central model
Jrn.Command ""Ribbon"" , ""Save the active project back to the Central Model , ID_FILE_SAVE_TO_CENTRAL""
{0}
' Comments
Jrn.Edit ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Control_Revit_Comment"", ""ReplaceContents"" , ""{1}""
' Assign synchronize with central dialog
Jrn.PushButton ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""OK, IDOK""";


        /// <summary>
        /// Compact central model template.
        /// </summary>
        public static readonly string FileSyncCompactFile
            = @"' Compact central model 
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Compact Central Model (slow), Control_Revit_ForceCompactCentralModel"", True";

        /// <summary>
        /// Release borrowed elements template.
        /// </summary>
        public static readonly string FileSyncBorrowedElements
            = @"' Release borrowed elements
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Borrowed Elements, Control_Revit_ReturnBorrowedElements"", True";

        /// <summary>
        /// Release borrowed worksets template.
        /// </summary>
        public static readonly string FileSyncUserСreatedWorksets
            = @"' Release borrowed worksets
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""User-created Worksets, Control_Revit_RelinqUserCreatedPartitions"", True";

        /// <summary>
        /// Saving local file when sync template.
        /// </summary>
        public static readonly string FileSyncSaveLocalFile
            = @"' Saving local file when sync
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Save Local File before and after synchronizing with central, Control_Revit_SavePartitionsToLocal"", True";

        /// <summary>
        /// Execute external command template.
        /// </summary>
        public static readonly string ExecuteExternalCommand =
            @"' Execute external command 
Jrn.RibbonEvent ""TabActivated:Add-Ins""
Jrn.RibbonEvent ""Execute external command:{0}:{1}""";

        /// <summary>
        /// Execute external command journal data template.
        /// </summary>
        public static readonly string ExternalCommandJournalData =
            @"' External command JournalData
Jrn.Data ""APIStringStringMapJournalData"" _";

        /// <summary>
        /// Dynamo command execute template.
        /// </summary>
        public static readonly string DynamoCommandExecute =
            @"Jrn.RibbonEvent ""TabActivated:Manage""
Jrn.Command ""Ribbon"" , ""Launch Dynamo, ID_VISUAL_PROGRAMMING_DYNAMO""";
    }
}