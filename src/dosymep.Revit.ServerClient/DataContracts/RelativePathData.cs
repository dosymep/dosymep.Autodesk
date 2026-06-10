using Newtonsoft.Json;

namespace dosymep.Revit.ServerClient.DataContracts;

/// <summary>
///     The relative path data.
/// </summary>
public abstract class RelativePathData : IEquatable<RelativePathData> {
    /// <summary>
    ///     Constructs relative path data.
    /// </summary>
    /// <param name="path">The folder path..</param>
    [JsonConstructor]
    protected RelativePathData(string path) {
        Path = path;
    }

    /// <summary>
    ///     The folder path.
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

        if(obj.GetType() != GetType()) {
            return false;
        }

        return Equals((RelativePathData) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() {
        return Path != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Path) : 0;
    }

    /// <summary>
    ///     Determines whether two <see cref="RelativePathData" /> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>
    ///     <c>true</c> if both instances represent the same value; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(RelativePathData left, RelativePathData right) {
        return Equals(left, right);
    }

    /// <summary>
    ///     Determines whether two <see cref="RelativePathData" /> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns>
    ///     <c>true</c> if both instances do not represent the same value; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(RelativePathData left, RelativePathData right) {
        return !Equals(left, right);
    }

    #endregion
}