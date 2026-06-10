using System.Runtime.Serialization;

namespace dosymep.Revit.ServerClient.DataContracts
{
    /// <summary>
    /// The parameter value type.
    /// </summary>
    public enum ParamValueType {
        /// <summary>
        /// Length
        /// </summary>
        Length,
        
        /// <summary>
        /// Number
        /// </summary>
        Number,
        
        /// <summary>
        /// Material
        /// </summary>
        Material,
        
        /// <summary>
        /// Text
        /// </summary>
        Text,
        
        /// <summary>
        /// YesNo
        /// </summary>
        [EnumMember(Value = "Yes/No")] 
        YesNo,
        
        /// <summary>
        /// Multiline Text
        /// </summary>
        [EnumMember(Value = "Multiline Text")] 
        MultilineText
    }
}