using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using OpenMcdf;

namespace dosymep.Revit.FileInfo.Transmissions {
    /// <summary>
    /// Transmission data.
    /// </summary>
    public class TransmissionData {
        /// <summary>
        /// Transmission data stream name.
        /// </summary>
        public const string TransmissionDataFileName = "TransmissionData";

        /// <summary>
        /// If model was transmitted value is true.
        /// </summary>
        [XmlAttribute("isTransmitted")]
        public bool IsTransmitted { get; set; }

        /// <summary>
        /// User data.
        /// </summary>
        [XmlAttribute("userData")]
        public string UserData { get; set; }

        /// <summary>
        /// Version.
        /// </summary>
        [XmlAttribute("version")]
        public int Version { get; set; }

        /// <summary>
        /// Linked files info.
        /// </summary>
        [XmlElement("ExternalFileReference")]
        public List<ExternalFileReference> ExternalFileReferences { get; set; }

        /// <summary>
        /// Reads transmission data.
        /// </summary>
        /// <param name="revitFileName">Revit model path.</param>
        /// <returns>Returns transmission data.</returns>
        internal static TransmissionData ReadTransmissionData(string revitFileName) {
            using(CompoundFile cf = new CompoundFile(revitFileName)) {
                if(cf.RootStorage.TryGetStream(TransmissionDataFileName, out CFStream rawBasicInfoData)) {
                    byte[] bytes = rawBasicInfoData.GetData();
                    return GetXmlTransmissionData(bytes);
                }
            }

            return null;
        }

        /// <summary>
        /// Writes transmission data.
        /// </summary>
        /// <param name="revitFileName">Revit model path.</param>
        /// <param name="transmissionData">Transmission data.</param>
        internal static void WriteTransmissionData(string revitFileName, TransmissionData transmissionData) {
            using(CompoundFile cf = new CompoundFile(revitFileName, CFSUpdateMode.Update, CFSConfiguration.Default)) {
                if(cf.RootStorage.TryGetStream(TransmissionDataFileName, out CFStream rawBasicInfoData)) {
                    string xmlData = Serialize(transmissionData);

                    var bytes = GetByteArray(xmlData);
                    rawBasicInfoData.SetData(bytes);
                }

                cf.Commit();
            }
        }

        internal static TransmissionData GetXmlTransmissionData(byte[] bytes) {
            using(var stream = new MemoryStream(bytes)) {
                using(var reader = new BinaryReader(stream, Encoding.Unicode)) {
                    int length = reader.ReadInt32();
                    string xmlData = new string(reader.ReadChars(length));

                    using(var textReader = new StringReader(xmlData)) {
                        return Deserialize<TransmissionData>(textReader);
                    }
                }
            }
        }

        internal static byte[] GetByteArray(string textTransmissionData) {
            using(var stream = new MemoryStream()) {
                using(var writer = new BinaryWriter(stream, Encoding.Unicode)) {
                    writer.Write(textTransmissionData.Length);
                    writer.Write(textTransmissionData.ToArray());
                }

                return stream.ToArray();
            }
        }

        internal static string Serialize<T>(T @object) {
            var builder = new StringBuilder();

            using(var xmlWriter = XmlWriter.Create(builder, new XmlWriterSettings() {Indent = false})) {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(xmlWriter, @object, ns);
            }

            return builder.ToString();
        }

        internal static T Deserialize<T>(TextReader textReader) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T) xmlSerializer.Deserialize(textReader);
        }

        /// <inheritdoc />
        public override string ToString() {
            return $"IsTransmitted: {IsTransmitted}; Count: {ExternalFileReferences.Count}";
        }
    }
}