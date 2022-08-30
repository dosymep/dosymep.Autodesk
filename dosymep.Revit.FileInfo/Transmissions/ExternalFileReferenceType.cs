using System.Xml.Serialization;

namespace dosymep.Revit.FileInfo.Transmissions {
    /// <summary>
    /// Type linked files.
    /// </summary>
    public enum ExternalFileReferenceType {
        /// <summary>
        /// Decal.
        /// </summary>
        Decal,

        /// <summary>
        /// CAD Link.
        /// </summary>
        [XmlEnum(Name = "CAD Link")]
        CADLink,

        /// <summary>
        /// Revit Link.
        /// </summary>
        [XmlEnum(Name = "Revit Link")]
        RevitLink,

        /// <summary>
        /// Keynote Table.
        /// </summary>
        [XmlEnum(Name = "Keynote Table")]
        KeynoteTable,

        /// <summary>
        /// Assembly Code Table
        /// </summary>
        [XmlEnum(Name = "Assembly Code Table")]
        AssemblyCodeTable,

        /// <summary>
        /// DWF Markup
        /// </summary>
        [XmlEnum(Name = "DWF Markup")]
        DWFMarkup
    }
}
