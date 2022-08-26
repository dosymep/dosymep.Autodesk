using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

using dosymep.Autodesk;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents a single Revit Add-In.
    /// </summary>
    public abstract class RevitAddinItem {
        /// <summary>
        /// RevitAPI
        /// </summary>
        public static readonly string AssemblyRevitApi = "RevitAPI";

        /// <summary>
        /// RevitAPIUI
        /// </summary>
        public static readonly string AssemblyRevitApiUi = "RevitAPIUI";

        /// <summary>
        /// Autodesk.Revit.DB.IExternalCommand
        /// </summary>
        public static readonly string CommandInterface = "Autodesk.Revit.UI.IExternalCommand";

        /// <summary>
        /// Autodesk.Revit.DB.IExternalApplication
        /// </summary>
        public static readonly string ApplicationInterface = "Autodesk.Revit.UI.IExternalApplication";

        /// <summary>
        /// Autodesk.Revit.DB.IExternalDBApplication
        /// </summary>
        public static readonly string DBApplicationInterface = "Autodesk.Revit.DB.IExternalDBApplication";

        /// <summary>
        /// AddInTypeTag
        /// </summary>
        public static readonly string AddInTypeTag = "Type";

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
        /// Root addin manifest.
        /// </summary>
        protected RevitAddinManifest _addinManifest;

        /// <summary>
        /// Creates revit addin item.
        /// </summary>
        /// <param name="addinElement">Addin element xml node.</param>
        /// <param name="addinManifest">Root addin manifest.</param>
        /// <typeparam name="T">Revit addin item type.</typeparam>
        /// <returns>Returns revit addin item.</returns>
        protected static T CreateRevitAddinItem<T>(XmlNode addinElement, RevitAddinManifest addinManifest)
            where T : RevitAddinItem, new() {
            T addinItem = new T();

            addinItem._addinManifest = addinManifest;
            addinItem.Name = addinElement.GetXmlNodeValue<string>(NameTag);
            addinItem.AddinId = addinElement.GetXmlNodeValue<Guid>(AddInIdTag);
            addinItem.AddinId = addinItem.AddinId == Guid.Empty
                ? addinElement.GetXmlNodeValue<Guid>(ClientIdTag)
                : addinItem.AddinId;

            addinItem.AssemblyPath = addinElement.GetFilePath(AssemblyTag);
            addinItem.FullClassName = addinElement.GetXmlNodeValue<string>(FullClassNameTag);
            addinItem.SuppressedWarning = addinElement.GetXmlNodeEnumValue<WarningType>(SuppressedWarningTag);

            addinItem.VendorId = addinElement.GetXmlNodeValue<string>(VendorIdTag);
            addinItem.VendorDescription = addinElement.GetXmlNodeValue<string>(VendorDescriptionTag);

            addinItem.ProductImage = addinElement.GetXmlNodeValue<string>(ProductImageTag);
            addinItem.ProductVersion = addinElement.GetXmlNodeValue<string>(ProductVersionTag);
            addinItem.ProductDescription = addinElement.GetXmlNodeValue<string>(ProductDescriptionTag);

            addinItem.AllowLoadingIntoExistingSession =
                addinElement.GetXmlNodeValue<bool>(AllowLoadingIntoExistingSessionTag);

            return addinItem;
        }

        /// <summary>
        /// Fills xml node.
        /// </summary>
        /// <param name="addinItemNode">Root node.</param>
        public void FillXmlNode(XmlNode addinItemNode) {
            addinItemNode.CreateAndAppendAttribute(AddInTypeTag, TypeName);

            addinItemNode.CreateAndAppendElement(NameTag, Name);
            addinItemNode.CreateAndAppendElement(AddInIdTag, AddinId);

            addinItemNode.CreateAndAppendElement(AssemblyTag, AssemblyPath);
            addinItemNode.CreateAndAppendElement(FullClassNameTag, FullClassName);
            addinItemNode.CreateAndAppendElement(SuppressedWarningTag, SuppressedWarning);

            addinItemNode.CreateAndAppendElement(VendorIdTag, VendorId);
            addinItemNode.CreateAndAppendElement(VendorDescriptionTag, VendorDescription);

            addinItemNode.CreateAndAppendElement(ProductImageTag, ProductImage);
            addinItemNode.CreateAndAppendElement(ProductVersionTag, ProductVersion);
            addinItemNode.CreateAndAppendElement(ProductDescriptionTag, ProductDescription);

            addinItemNode.CreateAndAppendElement(AllowLoadingIntoExistingSessionTag, AllowLoadingIntoExistingSession);

            FillXmlNodeImpl(addinItemNode);
        }

        /// <summary>
        /// Checks type of inherits interface.
        /// </summary>
        /// <param name="type">Checked type.</param>
        /// <param name="interfaceFullName">Interface full name.</param>
        /// <returns>Returns true - if type inherit interface, otherwise false.</returns>
        protected static bool IsTypeInheritInterface(Type type, string interfaceFullName) {
            if(type == null) {
                throw new ArgumentNullException(nameof(type));
            }

            if(string.IsNullOrEmpty(interfaceFullName)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(interfaceFullName));
            }

            string interfaceName = interfaceFullName.Split('.').LastOrDefault();
            if(string.IsNullOrEmpty(interfaceName)) {
                throw new InvalidOperationException($"The {interfaceFullName} is not valid.");
            }

            Type interfaceType = type.GetInterface(interfaceName);
            AssemblyName assemblyName = interfaceType?.Assembly.GetName();
            return interfaceType?.FullName?.Equals(interfaceFullName) == true
                   && (assemblyName?.Name.Equals(AssemblyRevitApi) == true || assemblyName?.Name.Equals(AssemblyRevitApiUi) == true);
        }

        /// <summary>
        /// Returns addin items.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <param name="interfaceFullName">Interface full name.</param>
        /// <typeparam name="T">Revit addin type.</typeparam>
        /// <returns>Returns addin items.</returns>
        protected static IEnumerable<T> GetAddinItems<T>(Assembly assembly, string interfaceFullName)
            where T : RevitAddinItem, new() {
            return assembly.GetTypes()
                .Where(item => item.IsPublic)
                .Where(item => item.IsClass)
                .Where(item => !item.IsAbstract)
                .Where(item => IsTypeInheritInterface(item, interfaceFullName))
                .Select(item => CreateAddinItem<T>(assembly, item));
        }

        private static T CreateAddinItem<T>(Assembly assembly, Type type) where T : RevitAddinItem, new() {
            return new T() {
                AddinId = Guid.NewGuid(),
                AllowLoadingIntoExistingSession = true,
                Name = type.Namespace?.Split('.').FirstOrDefault()
                       ?? Path.GetFileNameWithoutExtension(assembly.Location),
                FullClassName = type.FullName,
                AssemblyPath = Path.GetFileName(assembly.Location),
                ProductVersion = assembly.GetName().Version.ToString(),
            };
        }

        /// <summary>
        /// Returns type name revit addin item. 
        /// </summary>
        protected abstract string TypeName { get; }

        /// <summary>
        /// Assembly name.
        /// </summary>
        protected abstract string AssemblyName { get; }

        /// <summary>
        /// Type interface name.
        /// </summary>
        protected abstract string TypeInterfaceName { get; }

        /// <summary>
        /// Fills xml node.
        /// </summary>
        /// <param name="addinItemNode">Current node.</param>
        protected abstract void FillXmlNodeImpl(XmlNode addinItemNode);
        
        /// <summary>
        /// Transform journal element.
        /// </summary>
        /// <param name="transformer">Transformer journal element.</param>
        /// <typeparam name="T">Transform result.</typeparam>
        /// <typeparam name="TVisitable">Visitable element.</typeparam>
        /// <returns>Returns transform result.</returns>
        public abstract T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) where TVisitable : RevitAddinItem;

        /// <summary>
        /// Addin name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id for the Add-In. Each add-in must be assigned a unique identifier.
        /// </summary>
        public Guid AddinId { get; set; }

        /// <summary>
        /// The file path for the assembly which defines the Add-In.
        /// </summary>
        public string AssemblyPath { get; set; }

        /// <summary>
        /// The full file path for the assembly which defines the Add-In.
        /// </summary>
        public string FullAssemblyPath => GetFullAssemblyPath();

        /// <summary>
        /// The full class name for the class providing the entry point into the Add-In.
        /// </summary>
        /// <remarks>This is the class implementing IExternalCommand or IExternalApplication or IExternalDBApplication.</remarks>
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

        /// <summary>
        /// Loading revit addin item assembly in Current Domain.
        /// </summary>
        /// <returns>Returns loaded revit addin item assembly in Current Domain.</returns>
        public Assembly LoadAssembly() {
            if(!AssemblyPath.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase)) {
                throw new InvalidOperationException($"The \"{AssemblyPath}\" assembly path is not a library.");
            }

            if(!File.Exists(FullAssemblyPath)) {
                throw new InvalidOperationException($"The \"{FullAssemblyPath}\" assembly file is not found.");
            }

            return Assembly.LoadFrom(FullAssemblyPath);
        }

        /// <summary>
        /// Returns addin item type.
        /// </summary>
        /// <returns>Returns addin item type.</returns>
        public Type GetAddinItemType() {
            Assembly assembly = LoadAssembly();
            return assembly.GetType(FullClassName, true);
        }

        /// <summary>
        /// Creates addin item object.
        /// </summary>
        /// <typeparam name="T">Addin item type.</typeparam>
        /// <returns>Returns addin item object.</returns>
        public T CreateAddinItemObject<T>() {
            Type addinType = GetAddinItemType();
            return (T) Activator.CreateInstance(addinType);
        }

        private string GetFullAssemblyPath() {
            if(Path.IsPathRooted(AssemblyPath)) {
                return AssemblyPath;
            }

            if(string.IsNullOrEmpty(_addinManifest.FullName)) {
                throw new InvalidOperationException("The addin manifest does not have full name.");
            }

            string directoryName = Path.GetDirectoryName(_addinManifest.FullName);
            if(string.IsNullOrEmpty(directoryName)) {
                throw new InvalidOperationException("Manifest file path is not valid.");
            }

            return Path.Combine(directoryName, AssemblyPath);
        }

        /// <inheritdoc />
        public override string ToString() {
            return FullClassName;
        }
    }
}