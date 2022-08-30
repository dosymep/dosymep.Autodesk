using System.Xml.Serialization;

namespace dosymep.Revit.FileInfo.TransmissionDataInfos {
    /// <summary>
    /// Path types.
    /// </summary>
    public enum PathType {
        /// <summary>
        /// Absolute path.
        /// </summary>
        Absolute,

        /// <summary>
        /// Relative path.
        /// </summary>
        Relative,

        /// <summary>
        /// Path from RS or BIM360.
        /// </summary>
        [XmlEnum(Name = "Server Location")]
        ServerLocation,

        /// <summary>
        /// Relative to Central Model.
        /// </summary>
        [XmlEnum(Name = "Relative to Central Model")]
        RelativeCentralModel,

        /// <summary>
        /// Relative to Library Locations.
        /// </summary>
        [XmlEnum(Name = "Relative to Library Locations")]
        RelativeLibraryLocations
    }
}
