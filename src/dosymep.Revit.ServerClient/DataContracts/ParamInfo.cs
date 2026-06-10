using System;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts
{
    /// <summary>
    /// The parameter information.
    /// </summary>
    public class ParamInfo {
        /// <summary>
        /// Guid of shared parameter.
        /// </summary>
        [JsonProperty("@id")]
        public Guid? Id { get; set; }
        
        /// <summary>
        /// The parameter value.
        /// </summary>
        [JsonProperty("#text")]
        public string Value { get; set; }

        /// <summary>
        /// The parameter’s display name.
        /// </summary>
        [JsonProperty("@displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The parameter’s type
        /// </summary>
        [JsonProperty("@type")]
        public ParamType ParamType { get; set; }

        /// <summary>
        /// The parameter’s value type.
        /// </summary>
        [JsonProperty("@typeOfParameter")]
        public ParamValueType ParamValueType { get; set; }
    }
}