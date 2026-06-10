using System.Xml.Serialization;

namespace dosymep.Revit.FileInfo.TransmissionDataStream {
    /// <summary>
    /// Linked file load states.
    /// </summary>
    public enum LoadState {
        /// <summary>
        /// Loaded.
        /// </summary>
        Loaded,

        /// <summary>
        /// Unloaded.
        /// </summary>
        Unloaded,

        /// <summary>
        /// Not found.
        /// </summary>
        [XmlEnum(Name = "Not Found")]
        NotFound
    }
}
