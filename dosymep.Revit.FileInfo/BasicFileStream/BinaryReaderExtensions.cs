using System;
using System.IO;
using System.Text;

using dosymep.AutodeskApps.FileInfo;

namespace dosymep.Revit.FileInfo.BasicFileStream {
    internal static class BinaryReaderExtensions {
        public static ModelVersionInfo ReadCentralVersion(this BinaryReader reader) {
            int versionNumber = reader.ReadInt32();
            Guid id = reader.ReadGuid();
            return new ModelVersionInfo(id, versionNumber);
        }

        public static ModelVersionInfo ReadCurrentVersion(this BinaryReader reader) {
            Guid id = reader.ReadGuid();
            int versionNumber = Convert.ToInt32(reader.ReadValueString());
            return new ModelVersionInfo(id, versionNumber);
        }

        public static LanguageCode ReadLanguageCode(this BinaryReader reader) {
            return LanguageCode.GetLanguageCode(reader.ReadValueString());
        }

        public static WorksharingType ReadWorksharingType(this BinaryReader reader) {
            return (WorksharingType) (reader.ReadByte() + 1);
        }

        public static ModelIdentity ReadIdentity(this BinaryReader reader) {
            return new ModelIdentity(reader.ReadGuid());
        }

        public static Guid ReadGuid(this BinaryReader reader) {
            return new Guid(reader.ReadValueString());
        }

        public static string ReadValueString(this BinaryReader reader) {
            int length = reader.ReadInt32();
            return length <= 0 ? null : new string(reader.ReadChars(length));
        }

        public static StringBuilder AppendLineFormat(this StringBuilder builder, string propertyName, object value) {
            return builder.AppendLine().Append(propertyName).Append(": ").Append(value);
        }
        
        public static StringBuilder AppendLineFormat(this StringBuilder builder, string propertyName, WorksharingType worksharingType) {
            switch(worksharingType) {
                case WorksharingType.NotEnabled:
                    return builder.AppendLineFormat(propertyName, "Not enabled");
                case WorksharingType.Central:
                    return builder.AppendLineFormat(propertyName, "Central");
                case WorksharingType.Local:
                    return builder.AppendLineFormat(propertyName, "Local");
                case WorksharingType.InProgress:
                    return builder.AppendLineFormat(propertyName, "In progress");
                case WorksharingType.CreatedLocal:
                    return builder.AppendLineFormat(propertyName, "Created Local");
                default:
                    throw new ArgumentOutOfRangeException(nameof(worksharingType), worksharingType, null);
            }
        }
    }
}