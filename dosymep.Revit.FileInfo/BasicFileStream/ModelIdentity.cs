using System;

namespace dosymep.Revit.FileInfo.BasicFileStream {
    /// <summary>
    /// Model identity
    /// </summary>
    public class ModelIdentity : IEquatable<ModelIdentity> {
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

        #region IEquatable<ModelIdentity>

        /// <inheritdoc />
        public bool Equals(ModelIdentity other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }

            if(ReferenceEquals(this, other)) {
                return true;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }

            if(ReferenceEquals(this, obj)) {
                return true;
            }

            if(obj.GetType() != this.GetType()) {
                return false;
            }

            return Equals((ModelIdentity) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return _guid.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ModelIdentity left, ModelIdentity right) {
            return Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ModelIdentity left, ModelIdentity right) {
            return !Equals(left, right);
        }

        #endregion
    }
}