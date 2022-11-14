using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dosymep.Revit.ServerClient.Internal {
    /// <summary>
    /// The json serialization class,
    /// this class using NewtonsoftJson
    /// </summary>
    internal class JsonSerialization {
        private readonly JsonSerializerSettings _jsonSerializerSettings
            = new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Objects,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                Converters = new List<JsonConverter>() {new IsoDateTimeConverter(), new TimeSpanConverter()}
            };

        public string FileExtension => ".json";

        public string Serialize<T>(T @object) {
            if(typeof(T).IsClass // https://pvs-studio.com/ru/docs/warnings/v3111/
               && object.Equals(@object, default(T))) {
                throw new ArgumentNullException(nameof(@object));
            }

            return JsonConvert.SerializeObject(@object, _jsonSerializerSettings);
        }

        public T Deserialize<T>(string text) {
            if(string.IsNullOrEmpty(text)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));
            }

            return JsonConvert.DeserializeObject<T>(text, _jsonSerializerSettings);
        }
    }

    /// <summary>
    /// https://stackoverflow.com/questions/12633588/parsing-iso-duration-with-json-net
    /// </summary>
    internal class TimeSpanConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var ts = (TimeSpan) value;
            var tsString = XmlConvert.ToString(ts);
            serializer.Serialize(writer, tsString);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Null) {
                return null;
            }

            object defaultValue = objectType == typeof(TimeSpan?) ? (object) null : TimeSpan.Zero;
            var value = serializer.Deserialize<string>(reader);
            return string.IsNullOrEmpty(value) ? defaultValue : XmlConvert.ToTimeSpan(value);
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }
    }
}