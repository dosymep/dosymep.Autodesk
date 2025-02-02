' Revit 2022 Journal by dosymep 
' 0:< 'C 05.08.2022 15:11:45 +03:00;

Dim Jrn
Set Jrn = CrsJournalScript

' Initialization debug mode
Jrn.Directive "DebugMode", "PermissiveJournal", True
Jrn.Directive "DebugMode", "PerformAutomaticActionInErrorDialog", True

' Open central model
Jrn.Command "Ribbon" , "Open an existing project , ID_REVIT_FILE_OPEN"

' Central open with audit
Jrn.Data "FileOpenSubDialog", "AuditCheckBox", "True"

' Set Central file name
Jrn.Data "File Name" , "IDOK", "revit_central_model_path.rvt"

' Set workset config
Jrn.Data "WorksetConfig" , "Custom", 1

' Confirm open worksets dialog 
Jrn.PushButton "Modal , Opening Worksets , Dialog_Revit_Partitions", "OK, IDOK"

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

' Launch dynamo
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

' Save as file command
Jrn.Command "Ribbon", "Save the active project with a new name , ID_REVIT_FILE_SAVE_AS"

' Save as file options
Jrn.Data  "SaveOptionsData", 20, 42, 0, 1, "Custom"

' Save as file name option
Jrn.Data "File Name", "IDOK" , "revit_central_model_path.rvt"

' Make this a Central Model after save
Jrn.Data "BecomeCentralProject", 1

' Enable worksharing
Jrn.Data "BecomeMultiUser", 1

' Apply replace worksharing
Jrn.Data "TaskDialogResult", _
        "The file revit_central_model_path.rvt already exists.  If you replace it, you will lose all of its backup versions. Do you want to replace the existing file?", _
        "Yes", "IDYES"

' Save as file command
Jrn.Command "Ribbon", "Save the active project with a new name , ID_REVIT_FILE_SAVE_AS"

' Save as file options
Jrn.Data  "SaveOptionsData", 20, 42, 0, 1, "Custom"

' Save as file name option
Jrn.Data "File Name", "IDOK" , "RSN://revit_central_model_path.rvt"

' Make this a Central Model after save
Jrn.Data "BecomeCentralProject", 1

' Enable worksharing
Jrn.Data "BecomeMultiUser", 1

' Apply replace central file
Jrn.Data  "TaskDialogResult", _
        "revit_central_model_path.rvt already exists. What do you want to do?", "Replace the original central model", "1002"

' Exit revit application
Jrn.Command "SystemMenu" , "Quit the application; prompts to save projects , ID_APP_EXIT"
