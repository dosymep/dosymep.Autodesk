using System.Text;
using System.Xml;
using System.Xml.Serialization;

using OpenMcdf;

namespace dosymep.Revit.FileInfo.TransmissionDataStream;

/// <summary>
///     Transmission data.
/// </summary>
public class TransmissionData {
    /// <summary>
    ///     Transmission data stream name.
    /// </summary>
    public const string TransmissionDataFileName = "TransmissionData";

    /// <summary>
    ///     If model was transmitted value is true.
    /// </summary>
    [XmlAttribute("isTransmitted")]
    public bool IsTransmitted { get; set; }

    /// <summary>
    ///     User data.
    /// </summary>
    [XmlAttribute("userData")]
    public string UserData { get; set; }

    /// <summary>
    ///     Version.
    /// </summary>
    [XmlAttribute("version")]
    public int Version { get; set; }

    /// <summary>
    ///     Linked files info.
    /// </summary>
    [XmlElement("ExternalFileReference")]
    public List<ExternalFileReference> ExternalFileReferences { get; set; }

    /// <summary>
    ///     Reads transmission data.
    /// </summary>
    /// <param name="revitFileName">Revit model path.</param>
    /// <returns>Returns transmission data.</returns>
    internal static TransmissionData ReadTransmissionData(string revitFileName) {
        using(CompoundFile cf = new(revitFileName)) {
            if(cf.RootStorage.TryGetStream(TransmissionDataFileName, out CFStream rawBasicInfoData)) {
                byte[] bytes = rawBasicInfoData.GetData();
                return GetXmlTransmissionData(bytes);
            }
        }

        return null;
    }

    /// <summary>
    ///     Writes transmission data.
    /// </summary>
    /// <param name="revitFileName">Revit model path.</param>
    /// <param name="transmissionData">Transmission data.</param>
    internal static void WriteTransmissionData(string revitFileName, TransmissionData transmissionData) {
        using(CompoundFile cf = new(revitFileName, CFSUpdateMode.Update, CFSConfiguration.Default)) {
            if(cf.RootStorage.TryGetStream(TransmissionDataFileName, out CFStream rawBasicInfoData)) {
                string xmlData = Serialize(transmissionData);

                byte[] bytes = GetByteArray(xmlData);
                rawBasicInfoData.SetData(bytes);
            }

            cf.Commit();
        }
    }

    internal static TransmissionData GetXmlTransmissionData(byte[] bytes) {
        using(MemoryStream stream = new(bytes)) {
            using(BinaryReader reader = new(stream, Encoding.Unicode)) {
                int length = reader.ReadInt32();
                string xmlData = new(reader.ReadChars(length));

                using(StringReader textReader = new(xmlData)) {
                    return Deserialize<TransmissionData>(textReader);
                }
            }
        }
    }

    internal static byte[] GetByteArray(string textTransmissionData) {
        using(MemoryStream stream = new()) {
            using(BinaryWriter writer = new(stream, Encoding.Unicode)) {
                writer.Write(textTransmissionData.Length);
                writer.Write(textTransmissionData.ToArray());
            }

            return stream.ToArray();
        }
    }

    internal static string Serialize<T>(T @object) {
        StringBuilder builder = new();

        using(XmlWriter xmlWriter = XmlWriter.Create(builder, new XmlWriterSettings {Indent = false})) {
            XmlSerializerNamespaces ns = new();
            ns.Add("", "");

            XmlSerializer xmlSerializer = new(typeof(T));
            xmlSerializer.Serialize(xmlWriter, @object, ns);
        }

        return builder.ToString();
    }

    internal static T Deserialize<T>(TextReader textReader) {
        XmlSerializer xmlSerializer = new(typeof(T));
        return (T) xmlSerializer.Deserialize(textReader);
    }

    /// <inheritdoc />
    public override string ToString() {
        return $"IsTransmitted: {IsTransmitted}; Count: {ExternalFileReferences.Count}";
    }
}