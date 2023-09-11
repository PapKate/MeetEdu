using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBase.Blazor
{
    /// <summary>
    /// Represents the source of a vector drawable
    /// </summary>
    public sealed class VectorSource : IEquatable<VectorSource>
    {
        #region Public Properties

        /// <summary>
        /// An empty <see cref="VectorSource"/>
        /// </summary>
        public static VectorSource Empty { get; } = VectorSource.FromPathData(string.Empty);

        /// <summary>
        /// The path data of the icon
        /// </summary>
        public string? PathData { get; private set; }

        /// <summary>
        /// A flag indicating whether the vector source
        /// uses a path data
        /// </summary>
        [MemberNotNullWhen(true, nameof(PathData))]
        [MemberNotNullWhen(false, nameof(UriSource))]
        public bool HasPathData => PathData != null;

        /// <summary>
        /// The Uri source
        /// </summary>
        public Uri? UriSource { get; private set; }

        /// <summary>
        /// A flag indicating whether the vector source
        /// uses an external URI
        /// </summary>
        [MemberNotNullWhen(false, nameof(PathData))]
        [MemberNotNullWhen(true, nameof(UriSource))]
        public bool HasUriSource => UriSource != null;

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor
        /// </summary>
        private VectorSource() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="VectorSource"/> from the specified <paramref name="pathData"/>
        /// </summary>
        /// <param name="pathData">The path data</param>
        /// <returns></returns>
        public static VectorSource FromPathData(string pathData) => new VectorSource
        {
            PathData = pathData ?? string.Empty
        };

        /// <summary>
        /// Creates and returns a <see cref="VectorSource"/> from the specified <paramref name="uriSource"/>
        /// </summary>
        /// <param name="uriSource">The Uri source</param>
        /// <returns></returns>
        public static VectorSource FromUriSource(Uri uriSource)
        {
            _ = uriSource ?? throw new ArgumentNullException(nameof(uriSource));

            return new VectorSource
            {
                UriSource = uriSource
            };
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => PathData ?? UriSource!.ToString();

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is VectorSource vectorSource)
                return Equals(vectorSource);

            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns></returns>
        public bool Equals(VectorSource? other)
        {
            if (other is null)
                return false;

            if (UriSource is null)
                return PathData == other.PathData;
            else
                return UriSource == other.UriSource;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(PathData, UriSource);

        #endregion

        #region Operators

        /// <summary>
        /// Creates and returns a <see cref="VectorSource"/> from the specified <paramref name="pathData"/>
        /// </summary>
        /// <param name="pathData">The path data</param>
        public static implicit operator VectorSource(string pathData) => FromPathData(pathData);

        /// <summary>
        /// Creates and returns a <see cref="VectorSource"/> from the specified <paramref name="uriSource"/>
        /// </summary>
        /// <param name="uriSource">The URI source</param>
        public static implicit operator VectorSource(Uri uriSource) => FromUriSource(uriSource);

        #endregion
    }
}
