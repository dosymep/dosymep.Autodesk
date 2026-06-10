namespace dosymep.Revit.Journaling {
    /// <summary>
    /// Journal templates for Revit after 2022 version.
    /// </summary>
    internal class RevitJournalTemplatesOld {
        /// <summary>
        /// Synchronization central model template.
        /// </summary>
        public static readonly string FileSync
            = @"
' Synchronization central model
Jrn.Command ""Ribbon"" , ""Save the active project back to the Central Model , ID_FILE_SAVE_TO_MASTER""";

        /// <summary>
        /// Sync comment.
        /// </summary>
        public static readonly string FileSyncComment = @"
' Comments
Jrn.Edit ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster"", _
        ""Control_Revit_Comment"", ""ReplaceContents"" , ""{0}""";

        /// <summary>
        /// Accept sync.
        /// </summary>
        public static readonly string FileSyncAccept = @"
' Assign synchronize with central dialog
Jrn.PushButton ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster"", _
        ""OK, IDOK""";


        /// <summary>
        /// Compact central model template.
        /// </summary>
        public static readonly string FileSyncCompactFile
            = @"
' Compact central model 
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster"", _
        ""Compact Central Model (slow), Control_Revit_ForceCompactCentralModel"", True";

        /// <summary>
        /// Release borrowed elements template.
        /// </summary>
        public static readonly string FileSyncBorrowedElements
            = @"
' Release borrowed elements
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster"", _
        ""Borrowed Elements, Control_Revit_ReturnBorrowedElements"", True";

        /// <summary>
        /// Release borrowed worksets template.
        /// </summary>
        public static readonly string FileSyncUserСreatedWorksets
            = @"
' Release borrowed worksets
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster"", _
        ""User-created Worksets, Control_Revit_RelinqUserCreatedPartitions"", True";

        /// <summary>
        /// Saving local file when sync template.
        /// </summary>
        public static readonly string FileSyncSaveLocalFile
            = @"
' Saving local file when sync
Jrn.CheckBox ""Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster"", _
        ""Save Local File before and after synchronizing with central, Control_Revit_SavePartitionsToLocal"", True";
    }
}