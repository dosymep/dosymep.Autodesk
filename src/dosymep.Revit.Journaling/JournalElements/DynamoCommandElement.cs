using System;
using System.Collections.Generic;

using dosymep.AutodeskApps;

using Newtonsoft.Json;

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
        /// Dynamo model info.
        /// </summary>
        public List<DynamoNodeInfo> NodesInfo { get; set; }

        /// <summary>
        /// External command journal data.
        /// </summary>
        internal IDictionary<string, string> JournalData => new Dictionary<string, string>() {
            {"dynShowUI", "False"},
            {"dynAutomation", "True"},
            {"dynPathExecute", "False"},
            {"dynModelShutDown", "True"},
            {"dynPath", ScriptPath},
            {"dynModelNodesInfo", JsonConvert.SerializeObject(NodesInfo)},
        };

        /// <inheritdoc />
        public override T Reduce<T, TVisitable>(ITransformer<T, TVisitable> transformer) {
            if(transformer is ITransformer<T, DynamoCommandElement> openSharedModelTransform) {
                return openSharedModelTransform.Transform(this);
            }

            return default;
        }
    }

    /// <summary>
    /// Dynamo model node info.
    /// </summary>
    public class DynamoNodeInfo {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get;set;}
        
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Value.
        /// </summary>
        public string Value { get; set; }
    }
}