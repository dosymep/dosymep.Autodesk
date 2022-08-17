using System;
using System.Xml;

namespace dosymep.Revit.FileInfo.RevitAddins {
    internal static class XmlDocumentExtensions {
        public static T GetXmlNodeValue<T>(this XmlNode xmlNode, string xmlNodeName) {
            if(xmlNode == null) {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }

            if(typeof(T) == typeof(Guid)) {
                return (T) (Guid.TryParse(xmlNode.SelectSingleNode(xmlNodeName)?.InnerText, out Guid value)
                    ? value
                    : (object) Guid.Empty);
            }

            if(typeof(T) == typeof(bool)) {
                string nodeValue = xmlNode.SelectSingleNode(xmlNodeName)?.InnerText;
                return (T) (object) (nodeValue?.Equals("YES", StringComparison.CurrentCultureIgnoreCase) == true
                                     || nodeValue?.Equals("TRUE", StringComparison.CurrentCultureIgnoreCase) == true);
            }

            return (T) Convert.ChangeType(xmlNode.SelectSingleNode(xmlNodeName)?.InnerText?.Trim(), typeof(T));
        }

        public static T GetXmlNodeEnumValue<T>(this XmlNode xmlNode, string xmlNodeName) where T : struct {
            if(xmlNode == null) {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }

            string enumValue = xmlNode.GetXmlNodeValue<string>(xmlNodeName);
            return Enum.TryParse(enumValue, out T result) ? result : default;
        }

        public static string GetFilePath(this XmlNode xmlNode, string xmlNodeName) {
            if(xmlNode == null) {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }

            return xmlNode.GetXmlNodeValue<string>(xmlNodeName)?.Replace("\"", "");
        }
    }
}