using System;
using System.Collections.Generic;
using System.Xml;

namespace dosymep.Revit.FileInfo.RevitAddins {
    internal static class XmlDocumentExtensions {
        public static XmlNode CreateAndAppendElement(this XmlDocument document, string xmlNodeName) {
            if(document == null) {
                throw new ArgumentNullException(nameof(document));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }
            
            return document.AppendChild(document.CreateElement(xmlNodeName));
        }
        
        public static XmlNode CreateAndAppendElement(this XmlNode xmlNode, string xmlNodeName) {
            if(xmlNode == null) {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }
            
            XmlDocument document = xmlNode.OwnerDocument;
            if(document == null) {
                throw new ArgumentException("Owner document is not set.", nameof(xmlNode));
            }
            
            return xmlNode.AppendChild(document.CreateElement(xmlNodeName));
        }
        
        public static XmlNode CreateAndAppendElement<T>(this XmlNode xmlNode, string xmlNodeName, T xmlValue) {
            if(xmlNode == null) {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }
            
            XmlDocument document = xmlNode.OwnerDocument;
            if(document == null) {
                throw new ArgumentException("Owner document is not set.", nameof(xmlNode));
            }

            if(EqualityComparer<T>.Default.Equals(xmlValue, default(T))) {
                return null;
            }

            XmlNode element = xmlNode.CreateAndAppendElement(xmlNodeName);
            element.InnerText = xmlValue.ToString();
            
            return element;
        }

        public static XmlNode CreateAndAppendAttribute<T>(this XmlNode xmlNode, string xmlNodeName, T xmlValue) {
            if(xmlNode == null) {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            if(string.IsNullOrEmpty(xmlNodeName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(xmlNodeName));
            }

            XmlDocument document = xmlNode.OwnerDocument;
            if(document == null) {
                throw new ArgumentException("Owner document is not set.", nameof(xmlNode));
            }

            XmlAttribute attribute = document.CreateAttribute(xmlNodeName);
            attribute.Value = xmlValue?.ToString() ?? string.Empty;
            
            return xmlNode.Attributes?.Append(attribute);
        }

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