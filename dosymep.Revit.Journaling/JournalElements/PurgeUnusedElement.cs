namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// Purge unused journal element.
    /// </summary>
    public class PurgeUnusedElement : JournalElement {
        /// <summary>
        /// Constructs purge unused journal element.
        /// </summary>
        public PurgeUnusedElement() 
            : base("Remove unused elements.") {
        }
        
        /// <summary>
        /// Number command executions.
        /// </summary>
        public int TryCount { get; set; } = 5;

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, PurgeUnusedElement> typed) {
                return typed.Transform(this);
            }

            return default;
        }
    }
}