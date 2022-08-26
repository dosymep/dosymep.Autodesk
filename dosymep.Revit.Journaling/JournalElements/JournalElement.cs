using System;

using dosymep.Autodesk;

namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// Abstract class revit journal element.
    /// </summary>
    public abstract class JournalElement {
        /// <summary>
        /// Constructs journal element.
        /// </summary>
        /// <param name="name">Journal element name.</param>
        public JournalElement(string name) {
            Name = name;
        }

        /// <summary>
        /// Identity journal element.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Journal element name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Transform journal element.
        /// </summary>
        /// <param name="transformer">Transformer journal element.</param>
        /// <typeparam name="T">Transform result.</typeparam>
        /// <typeparam name="TVisitable">Visitable element.</typeparam>
        /// <returns>Returns transform result.</returns>
        public abstract T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) where TVisitable : JournalElement;
    }
}