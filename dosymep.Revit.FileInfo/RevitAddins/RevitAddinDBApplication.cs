using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

using dosymep.AutodeskApps;

using Microsoft.SqlServer.Server;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents a Revit DB external application.
    /// </summary>
    public class RevitAddinDBApplication : RevitAddinItem {
        /// <summary>
        /// LoadInRevitWorkerTag
        /// </summary>
        public static readonly string LoadInRevitWorkerTag = "LoadInRevitWorker";

        /// <summary>
        /// Creates revit addin db application.
        /// </summary>
        /// <param name="addinElement">Addin element xml node.</param>
        /// <param name="addinManifest">Root addin manifest.</param>
        /// <returns>Returns revit addin db application.</returns>
        public static RevitAddinDBApplication CreateAddinDBApplication(XmlNode addinElement, RevitAddinManifest addinManifest) {
            RevitAddinDBApplication addinDBApplication = CreateRevitAddinItem<RevitAddinDBApplication>(addinElement, addinManifest);
            addinDBApplication.LoadInRevitWorker = addinElement.GetXmlNodeValue<bool>(LoadInRevitWorkerTag);
           
            return addinDBApplication;
        }
        
        /// <summary>
        /// Returns addin DB applications.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns> Returns addin DB applications.</returns>
        public static IEnumerable<RevitAddinDBApplication> GetAddinDBApplications(Assembly assembly) {
            return GetAddinItems<RevitAddinDBApplication>(assembly, DBApplicationInterface);
        }

        /// <inheritdoc />
        protected override string TypeName => RevitAddinManifest.AddInDBApplicationTag;
        
        /// <inheritdoc />
        protected override string AssemblyName => AssemblyRevitApi;
        
        /// <inheritdoc />
        protected override string TypeInterfaceName => DBApplicationInterface;

        /// <inheritdoc />
        protected override void FillXmlNodeImpl(XmlNode addinItemNode) {
            addinItemNode.CreateAndAppendElement(LoadInRevitWorkerTag, LoadInRevitWorker);
        }
        
        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, RevitAddinDBApplication> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }
            
            return default;
        }

        /// <summary>
        /// Indicates whether or not a RevitWorker process will load this add-in.
        /// </summary>
        /// <remarks>The default is false.</remarks>
        public bool LoadInRevitWorker { get; set; }
    }
}