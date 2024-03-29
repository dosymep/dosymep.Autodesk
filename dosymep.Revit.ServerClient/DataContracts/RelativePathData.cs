﻿using System;

using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts {
    /// <summary>
    /// The relative path data.
    /// </summary>
    public abstract class RelativePathData : IEquatable<RelativePathData> {
        /// <summary>
        /// Constructs relative path data.
        /// </summary>
        /// <param name="path">The folder path..</param>
        [JsonConstructor]
        protected RelativePathData(string path) {
            Path = path;
        }

        /// <summary>
        /// The folder path.
        /// </summary>
        public string Path { get; }

        /// <inheritdoc />
        public override string ToString() {
            return Path;
        }

        #region IEquatable<RelativePathData>

        /// <inheritdoc />
        public bool Equals(RelativePathData other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }

            if(ReferenceEquals(this, other)) {
                return true;
            }

            return string.Equals(Path, other.Path, StringComparison.CurrentCultureIgnoreCase);
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

            return Equals((RelativePathData) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return (Path != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Path) : 0);
        }

        public static bool operator ==(RelativePathData left, RelativePathData right) {
            return Equals(left, right);
        }

        public static bool operator !=(RelativePathData left, RelativePathData right) {
            return !Equals(left, right);
        }

        #endregion
    }
}