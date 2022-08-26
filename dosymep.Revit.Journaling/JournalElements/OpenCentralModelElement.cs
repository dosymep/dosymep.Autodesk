using dosymep.Autodesk;

namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// Open central model journal element.
    /// </summary>
    public class OpenCentralModelElement : JournalElement {
        /// <summary>
        /// Constructs open central model journal element.
        /// </summary>
        public OpenCentralModelElement()
            : base("Open workshared model.") {
        }

        /// <summary>
        /// If true detaching central model.
        /// </summary>
        public bool Detach { get; set; } = false;
        
        /// <summary>
        /// If true open central model with audit.
        /// </summary>
        public bool WithAudit { get; set; } = true;
        
        /// <summary>
        /// If true keep workset.
        /// </summary>
        public bool KeepWorkset { get; set; } = true;
        
        /// <summary>
        /// If true create local file.
        /// </summary>
        public bool CreateLocal { get; set; } = false;
        
        /// <summary>
        /// Model path.
        /// </summary>
        public string ModelPath { get; set; }

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, OpenCentralModelElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }
            
            return default;
        }
    }
}