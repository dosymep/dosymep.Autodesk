using System.Collections.Generic;

using dosymep.Autodesk;

namespace dosymep.Revit.Journaling.JournalElements {
    /// <summary>
    /// Dynamo command element.
    /// </summary>
    public class DynamoCommandElement : JournalElement {
        /// <summary>
        /// Creates
        /// </summary>
        public DynamoCommandElement()
            : base("Execute dynamo command") {
        }

        /// <summary>
        /// Dynamo script path.
        /// </summary>
        public string ScriptPath { get; set; }

        /// <summary>
        /// A json text contains nodes info.
        /// </summary>
        public string ModelNodesInfo { get; set; }

        /// <summary>
        /// External command journal data.
        /// </summary>
        internal IDictionary<string, string> JournalData => new Dictionary<string, string>() {
            {"dynShowUI", "False"},
            {"dynAutomation", "True"},
            {"dynPathExecute", "False"},
            {"dynModelShutDown", "True"},
            {"dynPath", ScriptPath},
            {"dynModelNodesInfo", ModelNodesInfo},
        };

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, DynamoCommandElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }

            return default;
        }
    }
}