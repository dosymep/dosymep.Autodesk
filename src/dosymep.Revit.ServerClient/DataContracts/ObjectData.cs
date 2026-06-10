using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The object data.
    /// </summary>
    public abstract class ObjectData : IEquatable<ObjectData> {
        /// <summary>
        /// Constructs object data.
        /// </summary>
        /// <param name="name">The name of folder/model.</param>
        [JsonConstructor]
        protected ObjectData(string name) {
            Name = name;
        }

        /// <summary>
        /// The name of folder/model.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The lock state of the folder/model.
        /// </summary>
        public LockState LockState { set; get; }

        /// <summary>
        /// The context of the admin lock on the folder/model,
        /// describing the use of the admin lock such as copying or moving a folder
        /// from one server to another.
        /// </summary>
        public LockContext LockContext { set; get; }

        /// <summary>
        /// The list of descendant models that are locked by the Revit clients.
        /// </summary>
        public List<ModelLockData> ModelLocksInProgress { set; get; }

        /// <inheritdoc />
        public override string ToString() {
            return Name;
        }

        #region IEquatable<ObjectData>

        /// <inheritdoc />
        public bool Equals(ObjectData other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }

            if(ReferenceEquals(this, other)) {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.CurrentCultureIgnoreCase);
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

            return Equals((ObjectData) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return (Name != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Name) : 0);
        }

        public static bool operator ==(ObjectData left, ObjectData right) {
            return Equals(left, right);
        }

        public static bool operator !=(ObjectData left, ObjectData right) {
            return !Equals(left, right);
        }

        #endregion
    }
}