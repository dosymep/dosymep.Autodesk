using System.Collections.Generic;
using System.Reflection;
using System.Xml;

using dosymep.Autodesk.FileInfo;

namespace dosymep.Revit.FileInfo.RevitAddins {
    /// <summary>
    /// Represents a Revit external command.
    /// </summary>
    public class RevitAddinCommand : RevitAddinItem {
        /// <summary>
        /// TextTag
        /// </summary>
        public static readonly string TextTag = "Text";
        
        /// <summary>
        /// DescriptionTag
        /// </summary>
        public static readonly string DescriptionTag = "Description";
        
        /// <summary>
        /// LongDescriptionTag
        /// </summary>
        public static readonly string LongDescriptionTag = "LongDescription";
        
        /// <summary>
        /// LargeImageTag
        /// </summary>
        public static readonly string LargeImageTag = "LargeImage";
        
        /// <summary>
        /// TooltipImageTag
        /// </summary>
        public static readonly string TooltipImageTag = "LongDescription";
        
        /// <summary>
        /// DisciplineTag
        /// </summary>
        public static readonly string DisciplineTag = "Discipline";
        
        /// <summary>
        /// VisibilityModeTag
        /// </summary>
        public static readonly string VisibilityModeTag = "VisibilityMode";
        
        /// <summary>
        /// LanguageTypeTag
        /// </summary>
        public static readonly string LanguageTypeTag = "LanguageType";
        
        /// <summary>
        /// AvailabilityClassNameTag
        /// </summary>
        public static readonly string AvailabilityClassNameTag = "AvailabilityClassName";

        /// <summary>
        /// Creates revit addin commands.
        /// </summary>
        /// <param name="addinElement">Addin element xml node.</param>
        /// <param name="addinManifest">Root addin manifest.</param>
        /// <returns>Returns revit addin commands.</returns>
        public static RevitAddinCommand CreateAddinCommand(XmlNode addinElement, RevitAddinManifest addinManifest) {
            RevitAddinCommand addinCommand = CreateRevitAddinItem<RevitAddinCommand>(addinElement, addinManifest);
            
            addinCommand.Text = addinElement.GetXmlNodeValue<string>(TextTag);
            addinCommand.Description = addinElement.GetXmlNodeValue<string>(DescriptionTag);
            addinCommand.LongDescription = addinElement.GetXmlNodeValue<string>(LongDescriptionTag);
            
            addinCommand.LargeImage = addinElement.GetXmlNodeValue<string>(LargeImageTag);
            addinCommand.TooltipImage = addinElement.GetXmlNodeValue<string>(TooltipImageTag);
            
            addinCommand.Discipline = addinElement.GetXmlNodeEnumValue<Discipline>(DisciplineTag);
            addinCommand.VisibilityMode = addinElement.GetXmlNodeEnumValue<VisibilityMode>(VisibilityModeTag);
            addinCommand.LanguageCode = LanguageCode.GetLanguageCode(addinElement.GetXmlNodeValue<string>(LanguageTypeTag));

            addinCommand.AvailabilityClassName = addinElement.GetXmlNodeValue<string>(AvailabilityClassNameTag);

            return addinCommand;
        }
        
        /// <summary>
        /// Returns addin DB applications.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns> Returns addin DB applications.</returns>
        public static IEnumerable<RevitAddinCommand> GetAddinCommands(Assembly assembly) {
            return GetAddinItems<RevitAddinCommand>(assembly, CommandInterface);
        }
        
        /// <inheritdoc />
        protected override string TypeName => RevitAddinManifest.AddInCommandTag;
        
        /// <inheritdoc />
        protected override string AssemblyName => AssemblyRevitApiUi;
        
        /// <inheritdoc />
        protected override string TypeInterfaceName => CommandInterface;

        /// <inheritdoc />
        protected override void FillXmlNodeImpl(XmlNode addinItemNode) {
            addinItemNode.CreateAndAppendElement(TextTag, Text);
            addinItemNode.CreateAndAppendElement(DescriptionTag, Description);
            addinItemNode.CreateAndAppendElement(LongDescriptionTag, LongDescription);
            
            addinItemNode.CreateAndAppendElement(LargeImageTag, LargeImage);
            addinItemNode.CreateAndAppendElement(TooltipImageTag, TooltipImage);
            
            addinItemNode.CreateAndAppendElement(DisciplineTag, Discipline);
            addinItemNode.CreateAndAppendElement(VisibilityModeTag, VisibilityMode);
            addinItemNode.CreateAndAppendElement(LanguageTypeTag, LanguageCode?.FullCode);
            
            addinItemNode.CreateAndAppendElement(AvailabilityClassNameTag, AvailabilityClassName);
        }

        /// <summary>
        /// The text displayed on the external command button.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The short description and short tooltip for the command.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// The text shown in the extended command tooltip.
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// The path to the large image for the command button.
        /// </summary>
        /// <remarks>
        /// This image will be shown on the button in the External Tools pulldown.
        /// The image should be 32 x 32 pixels. If the image is larger it will be adjusted to fit the button.
        /// </remarks>
        public string LargeImage { get; set; }

        /// <summary>
        /// The image shown in the extended command tooltip.
        /// </summary>
        /// <remarks> Recommended image size is 304 * 188 pixels. </remarks>
        public string TooltipImage { get; set; }
        
        /// <summary>
        /// The modes in which the external command will be visible.
        /// </summary>
        /// <remarks>
        /// There is 1 set of mode where visibility can be set:
        /// <item> Visibility affected by the availability of a certain discipline. </item>
        /// </remarks>
        public Discipline Discipline { get; set; }

        /// <summary>
        /// The modes in which the external command will be visible.
        /// </summary>
        /// <remarks>
        /// There are 2 sets of modes where visibility can be set:
        /// <item> Visibility affected by the type of active document. </item>
        /// </remarks>
        public VisibilityMode VisibilityMode { get; set; }
        
        /// <summary>
        /// The language type of Revit Add-In.
        /// </summary>
        public LanguageCode LanguageCode { get; set; }

        /// <summary>
        /// The full class name for the class providing the entry point to decide availability of this command.
        /// </summary>
        /// <remarks> This is the class implementing IExternalCommandAvailability interface. </remarks>
        public string AvailabilityClassName { get; set; }
    }
}