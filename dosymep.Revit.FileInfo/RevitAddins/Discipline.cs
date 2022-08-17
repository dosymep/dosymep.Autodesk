namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Describes the conditions under which a particular external command will be visible in the Revit UI.
    /// </summary>
    /// <remarks> Note that there are a few conditions where the Revit API framework prevents commands from being available always:
    /// <list type="number">
    ///     <item> When the user has another Revit command active, e.g. creating Windows, Doors, or editing sketches. </item>
    ///     <item> When the active view is in perspective mode, or when the view is a Walkthrough, Drawings Lists, View Lists, Note Blocks, View Lists, etc. </item>
    /// </list>
    /// </remarks>
    public enum Discipline {
        /// <summary>
        /// The command is available in all possible disciplines.
        /// </summary>
        Any = 0,

        /// <summary>
        /// The command is available in the Architecture discipline.
        /// </summary>
        Architecture = 1,

        /// <summary>
        /// The command is available in the Structure discipline.
        /// </summary>
        Structure = 2,

        /// <summary>
        /// The command is available in the StructuralAnalysis discipline.
        /// </summary>
        StructuralAnalysis = 4,

        /// <summary>
        /// The command is available in the Massing and Site discipline.
        /// </summary>
        MassingAndSite = 8,

        /// <summary>
        /// The command is available in the EnergyAnalysis discipline.
        /// </summary>
        EnergyAnalysis = 16, // 0x00000010

        /// <summary>
        /// The command is available in the Mechanical discipline.
        /// </summary>
        Mechanical = 32, // 0x00000020

        /// <summary>
        /// The command is available in the Electrical discipline.
        /// </summary>
        Electrical = 64, // 0x00000040

        /// <summary>
        /// The command is available in the Piping discipline.
        /// </summary>
        Piping = 128, // 0x00000080

        /// <summary>
        /// The command is available in the MechanicalAnalysis discipline.
        /// </summary>
        MechanicalAnalysis = 256, // 0x00000100

        /// <summary>
        /// The command is available in the PipingAnalysis discipline.
        /// </summary>
        PipingAnalysis = 512, // 0x00000200

        /// <summary>
        /// The command is available in the ElectricalAnalysis discipline.
        /// </summary>
        ElectricalAnalysis = 1024, // 0x00000400
    }
}