' Revit Journal by dosymep 
' 0:< 'C 05.08.2022 15:11:45 +03:00;
Dim Jrn
Set Jrn = CrsJournalScript
' Initialization debug mode
Jrn.Directive "DebugMode", "PerformAutomaticActionInErrorDialog", True
Jrn.Directive "DebugMode", "PermissiveJournal", True
' Open central model
Jrn.Command "Ribbon" , "Open an existing project , ID_REVIT_FILE_OPEN"
' Central open with audit
Jrn.Data "FileOpenSubDialog", "AuditCheckBox", "True"
Jrn.Data "TaskDialogResult", _
    "This operation can take a long time. Recommended use includes periodic maintenance of large files and preparation for upgrading to a new release. Do you want to continue?", _
        "Yes", "IDYES"
Jrn.Data "File Name" , "IDOK", "revit_central_model_path.rvt"
Jrn.Data "WorksetConfig" , "Custom", 1
Jrn.PushButton "Modal , Opening Worksets , Dialog_Revit_Partitions", "OK, IDOK"
Jrn.Data "TaskDialogResult", _
    "Detaching this model will create an independent model. You will be unable to synchronize your changes with the original central model." & vbLf & "What do you want to do?", _
        "Detach and preserve worksets", "1001"
Jrn.Directive "DocSymbol" , "[]"
' Synchronization central model
Jrn.Command "Ribbon" , "Save the active project back to the Central Model , ID_FILE_SAVE_TO_MASTER"
' Compact central model 
Jrn.CheckBox "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster", _
        "Compact Central Model (slow), Control_Revit_ForceCompactCentralModel", True
' Comments
Jrn.Edit "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster", _
        "Control_Revit_Comment", "ReplaceContents" , "Synchronization central model."
' Assign synchronize with central dialog
Jrn.PushButton "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster", _
        "OK, IDOK"
' Purge unused elements
Jrn.Command "Ribbon" , "Purge(delete) unused families and types, ID_PURGE_UNUSED"
Jrn.PushButton "Modal , Purge unused , Dialog_Revit_PurgeUnusedTree", "OK, IDOK"
' Purge unused elements
Jrn.Command "Ribbon" , "Purge(delete) unused families and types, ID_PURGE_UNUSED"
Jrn.PushButton "Modal , Purge unused , Dialog_Revit_PurgeUnusedTree", "OK, IDOK"
' Purge unused elements
Jrn.Command "Ribbon" , "Purge(delete) unused families and types, ID_PURGE_UNUSED"
Jrn.PushButton "Modal , Purge unused , Dialog_Revit_PurgeUnusedTree", "OK, IDOK"
' Purge unused elements
Jrn.Command "Ribbon" , "Purge(delete) unused families and types, ID_PURGE_UNUSED"
Jrn.PushButton "Modal , Purge unused , Dialog_Revit_PurgeUnusedTree", "OK, IDOK"
' Purge unused elements
Jrn.Command "Ribbon" , "Purge(delete) unused families and types, ID_PURGE_UNUSED"
Jrn.PushButton "Modal , Purge unused , Dialog_Revit_PurgeUnusedTree", "OK, IDOK"
' Synchronization central model
Jrn.Command "Ribbon" , "Save the active project back to the Central Model , ID_FILE_SAVE_TO_MASTER"
' Compact central model 
Jrn.CheckBox "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster", _
        "Compact Central Model (slow), Control_Revit_ForceCompactCentralModel", True
' Comments
Jrn.Edit "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster", _
        "Control_Revit_Comment", "ReplaceContents" , "Synchronization central model."
' Assign synchronize with central dialog
Jrn.PushButton "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToMaster", _
        "OK, IDOK"
' Exit revit application
Jrn.Command "SystemMenu" , "Quit the application; prompts to save projects , ID_APP_EXIT"
Jrn.Data "TaskDialogResult" , _
    "Do you want to save changes to Untitled?", _
        "No", "IDNO"
