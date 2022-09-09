using System.Collections;
using System.Collections.Generic;

using dosymep.Autodesk;
using dosymep.Revit.FileInfo.RevitAddins;

namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// External command journal element.
    /// </summary>
    public class ExternalCommandElement : JournalElement {
        /// <summary>
        /// Constructs external command journal element.
        /// </summary>
        public ExternalCommandElement()
            : base("Execute external command.") {
        }
        
        /// <summary>
        /// External command.
        /// </summary>
        public RevitAddinItem RevitAddinItem { get; set; }
        
        /// <summary>
        /// External command journal data.
        /// </summary>
        public IDictionary<string, string> JournalData { get; set; }

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, ExternalCommandElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }
            
            return default;
        }
    }
}