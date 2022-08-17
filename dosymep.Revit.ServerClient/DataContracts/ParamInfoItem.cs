using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dosymep.Revit.ServerClient.DataContracts
{
    /// <summary>
    /// The parameter information item.
    /// </summary>
    public class ParamInfoItem {
        /// <summary>
        /// The group name.
        /// </summary>
        [JsonProperty("A:title")]
        public string Title { get; set; }

        [JsonExtensionData] 
        private Dictionary<string, JToken> _additionalData = new Dictionary<string, JToken>();

        /// <summary>
        /// Parameter information.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, ParamInfo> Items { get; set; } = new Dictionary<string, ParamInfo>();

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            foreach(KeyValuePair<string, JToken> kvp in _additionalData) {
                Items.Add(kvp.Key, kvp.Value.ToObject<ParamInfo>());
            }

            _additionalData = null;
        }
    }
}