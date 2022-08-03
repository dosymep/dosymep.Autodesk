using System;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.Internal {
    /// <summary>
    /// The json serialization class,
    /// this class using NewtonsoftJson
    /// </summary>
    internal class JsonSerialization {
        public string FileExtension => ".json";

        public string Serialize<T>(T @object) {
            if(@object == null) {
                throw new ArgumentNullException(nameof(@object));
            }

            return JsonConvert.SerializeObject(@object);
        }

        public T Deserialize<T>(string text) {
            if(string.IsNullOrEmpty(text)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));
            }

            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}