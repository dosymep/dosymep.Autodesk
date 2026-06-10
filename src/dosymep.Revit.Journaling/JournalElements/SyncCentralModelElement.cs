using dosymep.AutodeskApps;

namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// Synchronization central model journal element.
    /// </summary>
    public class SyncCentralModelElement : JournalElement {
        /// <summary>
        /// Creates synchronization central model journal element.
        /// </summary>
        public SyncCentralModelElement() 
            : base("Synchronization shared model.") {
        }
        
        /// <summary>
        /// If true compact central model when sync.
        /// </summary>
        public bool Compact { get; set; } = true;
        
        /// <summary>
        /// If true saving local file when sync.
        /// </summary>
        public bool SaveLocalFile { get; set; } = false;
        
        /// <summary>
        /// If true release borrowed elements when sync.
        /// </summary>
        public bool BorrowedElements { get; set; } = false;
        
        /// <summary>
        /// If true release borrowed worksets when sync.
        /// </summary>
        public bool UserCreatedWorksets { get; set; } = false;

        /// <summary>
        /// Comment text when sync.
        /// </summary>
        public string Comment { get; set; } = "Synchronization central model.";

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, SyncCentralModelElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }
            
            return default;
        }
    }
}