﻿namespace dosymep.Revit.Journaling {
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
        public static readonly string InitDebug
            = @"
' Initialization debug mode
Jrn.Directive ""DebugMode"", ""PermissiveJournal"", True
Jrn.Directive ""DebugMode"", ""PerformAutomaticActionInErrorDialog"", True";

        /// <summary>
        /// Exit revit application template.
        /// </summary>
        public static readonly string ExitApplication
            = @"
' Exit revit application
Jrn.Command ""SystemMenu"" , ""Quit the application; prompts to save projects , ID_APP_EXIT""";

        /// <summary>
        /// Purge unused elements template.
        /// </summary>
        public static readonly string PurgeUnused
            = @"
' Purge unused elements
Jrn.Command ""Ribbon"" , ""Purge(delete) unused families and types, ID_PURGE_UNUSED""
Jrn.PushButton ""Modal , Purge unused , Dialog_Revit_PurgeUnusedTree"", ""OK, IDOK""";

        /// <summary>
        /// Ignore missing links template.
        /// </summary>
        public static readonly string IgnoreMissingLinks
            = @"
' Ignore missing links
Jrn.Data ""TaskDialogResult"",  _
    ""Revit could not find or read 1 references. What do you want to do?"",  _
        ""Ignore and continue opening the project"", ""1002""";

        /// <summary>
        /// Open central model template.
        /// </summary>
        public static readonly string CentralOpen
            = @"
' Open central model
Jrn.Command ""Ribbon"" , ""Open an existing project , ID_REVIT_FILE_OPEN""";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string CentralModelName = @"
' Set Central file name
Jrn.Data ""File Name"" , ""IDOK"", ""{0}""";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string CentralWorksetConfig = @"
' Set workset config
Jrn.Data ""WorksetConfig"" , ""{0}"", {1}";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string CentralAcceptCustomWorksets = @"
' Confirm open worksets dialog 
Jrn.PushButton ""Modal , Opening Worksets , Dialog_Revit_Partitions"", ""OK, IDOK""";

        /// <summary>
        /// Create local file template.
        /// </summary>
        public static readonly string CentralOpenAsLocalCheckBox
            = @"
' Central open as local file template
Jrn.Data ""FileOpenSubDialog"", ""OpenAsLocalCheckBox"", ""True""";

        /// <summary>
        /// Central open with detach.
        /// </summary>
        public static readonly string CentralOpenDetachCheckBox
            = @"
' Central open with detach
Jrn.Data ""FileOpenSubDialog"", ""DetachCheckBox"", ""True""";

        /// <summary>
        /// Central open with audit template.
        /// </summary>
        public static readonly string CentralOpenAuditCheckBox
            = @"
' Central open with audit
Jrn.Data ""FileOpenSubDialog"", ""AuditCheckBox"", ""True""";

        /// <summary>
        /// Save as file command template.
        /// </summary>
        public static readonly string SaveAsFile
            = @"
' Save as file command
Jrn.Command ""Ribbon"", ""Save the active project with a new name , ID_REVIT_FILE_SAVE_AS""";

        /// <summary>
        /// Save as file options template.
        /// </summary>
        public static readonly string SaveAsFileOptions
            = @"
' Save as file options
Jrn.Data  ""SaveOptionsData"", {0}, {1}, {2}, {3}, ""{4}""";

        /// <summary>
        /// Save as file name option.
        /// </summary>
        public static readonly string SaveAsFileNameOption
            = @"
' Save as file name option
Jrn.Data ""File Name"", ""IDOK"" , ""{0}""";

        /// <summary>
        /// Save as make this a Central Model after save template
        /// </summary>
        public static readonly string SaveAsMakeThisFileCentalModel
            = @"
' Make this a Central Model after save
Jrn.Data ""BecomeCentralProject"", {0}";
        
        /// <summary>
        /// Save as enable worksharing template
        /// </summary>
        public static readonly string SaveAsEnableWorksharing
            = @"
' Enable worksharing
Jrn.Data ""BecomeMultiUser"", {0}";

        /// <summary>
        /// Replace central file template (replace on revit server)
        /// </summary>
        public static readonly string SaveAsReplaceCentralFile
            = @"
' Apply replace central file
Jrn.Data  ""TaskDialogResult"", _
        ""{0} already exists. What do you want to do?"", ""Replace the original central model"", ""1002""";
        
        /// <summary>
        /// Replace workshring file template (replace on file system)
        /// </summary>
        public static readonly string SaveAsReplaceWorksharingFile
            = @"
' Apply replace worksharing
Jrn.Data ""TaskDialogResult"", _
        ""The file {0} already exists.  If you replace it, you will lose all of its backup versions. Do you want to replace the existing file?"", _
        ""Yes"", ""IDYES""";
        
        /// <summary>
        /// Synchronization central model template.
        /// </summary>
        public static readonly string FileSync
            = @"
' Synchronization central model
Jrn.Command ""Ribbon"" , ""Save the active project back to the Central Model , ID_FILE_SAVE_TO_CENTRAL""";

        /// <summary>
        /// Sync comment.
        /// </summary>
        public static readonly string FileSyncComment = @"
' Comments
Jrn.Edit ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Control_Revit_Comment"", ""ReplaceContents"" , ""{0}""";

        /// <summary>
        /// Accept sync.
        /// </summary>
        public static readonly string FileSyncAccept = @"
' Assign synchronize with central dialog
Jrn.PushButton ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""OK, IDOK""";


        /// <summary>
        /// Compact central model template.
        /// </summary>
        public static readonly string FileSyncCompactFile
            = @"
' Compact central model 
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Compact Central Model (slow), Control_Revit_ForceCompactCentralModel"", True";

        /// <summary>
        /// Release borrowed elements template.
        /// </summary>
        public static readonly string FileSyncBorrowedElements
            = @"
' Release borrowed elements
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Borrowed Elements, Control_Revit_ReturnBorrowedElements"", True";

        /// <summary>
        /// Release borrowed worksets template.
        /// </summary>
        public static readonly string FileSyncUserСreatedWorksets
            = @"
' Release borrowed worksets
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""User-created Worksets, Control_Revit_RelinqUserCreatedPartitions"", True";

        /// <summary>
        /// Saving local file when sync template.
        /// </summary>
        public static readonly string FileSyncSaveLocalFile
            = @"
' Saving local file when sync
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral"", _
        ""Save Local File before and after synchronizing with central, Control_Revit_SavePartitionsToLocal"", True";

        /// <summary>
        /// Execute external command template.
        /// </summary>
        public static readonly string ExecuteExternalCommand =
            @"
' Execute external command 
Jrn.RibbonEvent ""TabActivated:Add-Ins""
Jrn.RibbonEvent ""Execute external command:{0}:{1}""";

        /// <summary>
        /// Execute external command journal data template.
        /// </summary>
        public static readonly string ExternalCommandJournalData =
            @"
' External command JournalData
Jrn.Data ""APIStringStringMapJournalData"" _";

        /// <summary>
        /// Dynamo command execute template.
        /// </summary>
        public static readonly string DynamoCommandExecute =
            @"
' Launch dynamo
Jrn.RibbonEvent ""TabActivated:Manage""
Jrn.Command ""Ribbon"" , ""Launch Dynamo, ID_VISUAL_PROGRAMMING_DYNAMO""";
    }
}