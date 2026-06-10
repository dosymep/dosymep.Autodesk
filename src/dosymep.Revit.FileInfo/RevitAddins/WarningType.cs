namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Defines types of warnings issued by Revit related to Add-In failures.
    /// </summary>
    public enum WarningType {
        /// <summary>
        /// There is no warning in this Addin.
        /// </summary>
        NoWarning,

        /// <summary>
        /// Revit could not find the indicated class in the assembly file.
        /// </summary>
        ClassNotFound,

        /// <summary>
        /// Revit could not find the assembly file.
        /// </summary>
        WrongAssemblyPath,

        /// <summary>
        /// Unhandled exceptions were caught when the AddIn was executed.
        /// </summary>
        UncaughtException,

        /// <summary>
        /// There is another add-in which registered the same Id (GUID).
        /// </summary>
        GuidConflict,

        /// <summary>
        /// Missing the required value of the FullClassName node.
        /// </summary>
        NoFullClassName,

        /// <summary>
        /// Missing the required value of the ClientId node.
        /// </summary>
        NoClientId,

        /// <summary>
        /// Missing the required value of the Assembly node.
        /// </summary>
        NoAssembly,

        /// <summary>
        /// Missing the required value of the Name node for application add-in.
        /// </summary>
        NoApplicationName,

        /// <summary>
        /// Revit could not interop.
        /// </summary>
        InvalidFormatClientId,

        /// <summary>
        /// Revit could not invoke IExternalCommandAvailability correctly.
        /// </summary>
        WrongAvailabilityClass,

        /// <summary>
        /// Unhandled exceptions were caught when the availability command was executed.
        /// </summary>
        UncaughtExceptionInAvailabilityClass,

        /// <summary>
        /// Not a valid transaction attribute.
        /// </summary>
        InvalidTransactionAttributeError,

        /// <summary>
        /// Not a valid regeneration attribute.
        /// </summary>
        InvalidRegenerationAttributeError,

        /// <summary>
        /// Missing the required value of the VendorId node.
        /// </summary>
        NoVendorId,
    }
}