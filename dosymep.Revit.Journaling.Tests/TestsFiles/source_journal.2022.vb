' Revit 2022 Journal by dosymep 
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
Jrn.Command "Ribbon" , "Save the active project back to the Central Model , ID_FILE_SAVE_TO_CENTRAL"
' Compact central model 
Jrn.CheckBox "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral", _
        "Compact Central Model (slow), Control_Revit_ForceCompactCentralModel", True
' Comments
Jrn.Edit "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral", _
        "Control_Revit_Comment", "ReplaceContents" , "Synchronization central model."
' Assign synchronize with central dialog
Jrn.PushButton "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral", _
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
Jrn.Command "Ribbon" , "Save the active project back to the Central Model , ID_FILE_SAVE_TO_CENTRAL"
' Compact central model 
Jrn.CheckBox "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral", _
        "Compact Central Model (slow), Control_Revit_ForceCompactCentralModel", True
' Comments
Jrn.Edit "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral", _
        "Control_Revit_Comment", "ReplaceContents" , "Synchronization central model."
' Assign synchronize with central dialog
Jrn.PushButton "Modal , Synchronize with Central , Dialog_Revit_PartitionsSaveToCentral", _
        "OK, IDOK"
Jrn.RibbonEvent "TabActivated:Manage"
Jrn.Command "Ribbon" , "Launch Dynamo, ID_VISUAL_PROGRAMMING_DYNAMO"
' External command JournalData
Jrn.Data "APIStringStringMapJournalData" _
    , 6 _
    , "dynShowUI", "False" _
    , "dynAutomation", "True" _
    , "dynPathExecute", "False" _
    , "dynModelShutDown", "True" _
    , "dynPath", "@C:\script_dynamo.dyn" _
    , "dynModelNodesInfo", "[{"Id":"00000000-0000-0000-0000-000000000000","Name":"Name","Value":"Value"}]" _
' Execute external command 
Jrn.RibbonEvent "TabActivated:Add-Ins"
Jrn.RibbonEvent "Execute external command:9725d9bf-ca8c-4ee8-b8b0-c8257b5eb6f2:dosymep.RevitExternalCommand"
' External command JournalData
Jrn.Data "APIStringStringMapJournalData" _
    , 3 _
    , "key1", "value1" _
    , "key2", "value2" _
    , "key3", "value3" _
' Exit revit application
Jrn.Command "SystemMenu" , "Quit the application; prompts to save projects , ID_APP_EXIT"
