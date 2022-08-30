using System;

namespace dosymep.Revit.FileInfo.BasicFileInfos
{
    /// <summary>
    /// Model identity
    /// </summary>
    public class ModelIdentity {
        private readonly Guid _guid;

        /// <summary>
        /// Empty model identity.
        /// </summary>
        public static readonly ModelIdentity Empty = new ModelIdentity(Guid.Empty);

        /// <summary>
        /// Creates model identity.
        /// </summary>
        /// <param name="guid"></param>
        public ModelIdentity(Guid guid) {
            _guid = guid;
        }

        /// <inheritdoc />
        public override string ToString() {
            return _guid.ToString();
        }
    }
}