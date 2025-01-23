using dosymep.AutodeskApps;

namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// Save as command journal element.
    /// </summary>
    public class SaveAsFileCommandElement : JournalElement {
        /// <summary>
        /// Constructs save as command journal element.
        /// </summary>
        public SaveAsFileCommandElement()
            : base("Save as document") {
        }
        
        /// <summary>
        /// Maximum backup count.
        /// </summary>
        public int MaxBackupCount { get; set; }

        /// <summary>
        /// View id for generate thumbnail. Default (empty) value -1.
        /// </summary>
        public int ThumbnailViewId { get; set; } = -1;
        
        /// <summary>
        /// Regenerate thumbnail if view/sheet is not up-to date.
        /// </summary>
        public bool RegenerateThumbnail { get; set; } = false;
        
        /// <summary>
        /// Compact file.
        /// </summary>
        public bool CompactFile { get; set; } = false;
        
        /// <summary>
        /// New model path location.
        /// </summary>
        public string ModelPath { get; set; }
        
        /// <summary>
        /// Worksets options.
        /// </summary>
        public WorksetsOption WorksetOption { get; set; } = WorksetsOption.All;

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, SaveAsFileCommandElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }
            
            return default;
        }
    }
}