using System;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Describes the conditions under which a particular external command will be visible in the Revit UI.
    /// </summary>
    /// <remarks> Note that there are a few conditions where the Revit API framework prevents commands from being available always:
    /// <list type="number">
    ///     <item> When the user has another Revit command active, e.g. creating Windows, Doors, or editing sketches. </item>
    ///     <item> When the active view is in perspective mode, or when the view is a Walkthrough, Drawings Lists, View Lists, Note Blocks, View Lists, etc. </item>
    ///     <item> When the user is editing an in-place family. </item>
    /// </list>
    /// </remarks>
    [Flags]
    public enum VisibilityMode
    {
        /// <summary>
        /// The command is available in all possible modes supported by the Revit API.
        /// </summary>
        AlwaysVisible = 0,
        
        /// <summary>
        /// The command is invisible when there is a project document active.
        /// </summary>
        NotVisibleInProject = 1,
        
        /// <summary>
        /// The command is invisible when there is a family document active.
        /// </summary>
        NotVisibleInFamily = 2,
        
        /// <summary>
        /// The command is invisible when there is no active document.
        /// </summary>
        NotVisibleWhenNoActiveDocument = 4,
    }
}