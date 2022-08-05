namespace dosymep.Revit.Journaling {
    /// <summary>
    /// Marker transform interface.
    /// </summary>
    public interface ITransformer {
    }

    /// <summary>
    /// Transform interface.
    /// </summary>
    /// <typeparam name="T">Transform type result.</typeparam>
    /// <typeparam name="TVisitable">Visitable type element.</typeparam>
    public interface ITransformer<T, in TVisitable> {
        /// <summary>
        /// Transform method.
        /// </summary>
        /// <param name="visitable">Visitable element.</param>
        /// <returns>Returns transform result.</returns>
        T Transform(TVisitable visitable);
    }
}