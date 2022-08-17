using System;
using System.Xml;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents a single Revit Add-In.
    /// </summary>
    public abstract class RevitAddinItem {
        /// <summary>
        /// NameTag
        /// </summary>
        public static readonly string NameTag = "Name";
        
        /// <summary>
        /// AddInIdTag
        /// </summary>
        public static readonly string AddInIdTag = "AddInId";
        
        /// <summary>
        /// AssemblyTag
        /// </summary>
        public static readonly string AssemblyTag = "Assembly";
        
        /// <summary>
        /// ClientIdTag
        /// </summary>
        public static readonly string ClientIdTag = "ClientId";
        
        /// <summary>
        /// FullClassNameTag
        /// </summary>
        public static readonly string FullClassNameTag = "FullClassName";
        
        /// <summary>
        /// SuppressedWarningTag
        /// </summary>
        public static readonly string SuppressedWarningTag = "SuppressedWarning";
        
        /// <summary>
        /// VendorIdTag
        /// </summary>
        public static readonly string VendorIdTag = "VendorId";
        
        /// <summary>
        /// VendorDescriptionTag
        /// </summary>
        public static readonly string VendorDescriptionTag = "VendorDescription";
        
        /// <summary>
        /// ProductImageTag
        /// </summary>
        public static readonly string ProductImageTag = "ProductImage";
        
        /// <summary>
        /// ProductVersionTag
        /// </summary>
        public static readonly string ProductVersionTag = "ProductVersion";
        
        /// <summary>
        /// ProductDescriptionTag
        /// </summary>
        public static readonly string ProductDescriptionTag = "ProductDescription";
        
        /// <summary>
        /// AllowLoadingIntoExistingSessionTag
        /// </summary>
        public static readonly string AllowLoadingIntoExistingSessionTag = "AllowLoadingIntoExistingSession";
        
        /// <summary>
        /// Creates revit addin item.
        /// </summary>
        /// <param name="addinElement">Addin element xml node.</param>
        /// <typeparam name="T">Revit addin item type.</typeparam>
        /// <returns>Returns revit addin item.</returns>
        protected static T CreateRevitAddinItem<T>(XmlNode addinElement)
            where T : RevitAddinItem, new() {
            T addinItem = new T();

            addinItem.Name = addinElement.GetXmlNodeValue<string>(NameTag);
            addinItem.AddinId = addinElement.GetXmlNodeValue<Guid>(FullClassNameTag);
            
            addinItem.Assembly = addinElement.GetFilePath(AssemblyTag);
            addinItem.FullClassName = addinElement.GetXmlNodeValue<string>(FullClassNameTag);
            addinItem.SuppressedWarning = addinElement.GetXmlNodeValue<WarningType>(SuppressedWarningTag);
            
            addinItem.VendorId = addinElement.GetXmlNodeValue<string>(VendorIdTag);
            addinItem.VendorDescription = addinElement.GetXmlNodeValue<string>(VendorDescriptionTag);
            
            addinItem.ProductImage = addinElement.GetXmlNodeValue<string>(ProductImageTag);
            addinItem.ProductVersion = addinElement.GetXmlNodeValue<string>(ProductVersionTag);
            addinItem.ProductDescription = addinElement.GetXmlNodeValue<string>(ProductDescriptionTag);
            
            addinItem.AllowLoadingIntoExistingSession = addinElement.GetXmlNodeValue<bool>(AllowLoadingIntoExistingSessionTag);
            
            return addinItem;
        }
        
        /// <summary>
        /// Application name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id for the Add-In. Each add-in must be assigned a unique identifier.
        /// </summary>
        public Guid AddinId { get; set; }

        /// <summary>
        /// The file path for the assembly which defines the Add-In.
        /// </summary>
        public string Assembly { get; set; }
        
        /// <summary>
        /// The full class name for the class providing the entry point into the Add-In.
        /// </summary>
        /// <remarks> This is the class implementing IExternalCommand or IExternalApplication. </remarks>
        public string FullClassName { get; set; }
        
        /// <summary>
        /// A startup failure which, if it occurs, will be suppressed from the user.
        /// </summary>
        /// <remarks>
        /// When Revit attempts to start an Add-In and fails to do so, it will show a message to the user about the error.
        /// Users have the option to ignore that error in the future. This property is used to represent the user's choice to ignore an error.
        /// Note that if a different error occurs for the Add-In, it will still be shown to the user.
        /// </remarks>
        public WarningType SuppressedWarning { get; set; }

        /// <summary>
        /// The vendor id.
        /// </summary>
        /// <remarks> This is required by add-in application. </remarks>
        public string VendorId { get; set; }

        /// <summary>
        /// The vendor description.
        /// </summary>
        /// <remarks>This should be a string that the user can use to identify the vendor. </remarks>
        public string VendorDescription { get; set; }

        /// <summary>
        /// The product image.
        /// </summary>
        public string ProductImage { get; set; }
        
        /// <summary>
        /// The product version.
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// The product description.
        /// </summary>
        public string ProductDescription { get; set; }
        
        /// <summary>
        /// The flag of loading permission.
        /// </summary>
        public bool AllowLoadingIntoExistingSession { get; set; }
    }
}