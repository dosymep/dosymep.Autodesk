using dosymep.Autodesk;

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
        
        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, ExternalCommandElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }
            
            return default;
        }
    }
}