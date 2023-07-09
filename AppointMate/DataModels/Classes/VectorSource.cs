namespace AppointMate
{
    /// <summary>
    /// Represents the source of a vector drawable
    /// </summary>
    public sealed class VectorSource : IEquatable<VectorSource>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="PathData"/> property
        /// </summary>
        private string? mPathData;

        #endregion

        #region Public Properties

        /// <summary>
        /// The path data of the icon
        /// </summary>
        public string PathData 
        { 
            get => mPathData ?? string.Empty;
            private set => mPathData = value;
        }

        /// <summary>
        /// The Uri source
        /// </summary>
        public Uri? UriSource { get; private set; }

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
            PathData = pathData
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
        public override string ToString() => PathData ?? UriSource?.ToString() ?? string.Empty;

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
            if (UriSource is null)
                return PathData == other?.PathData;
            else
                return UriSource == other?.UriSource;
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
        /// Create a vector source from the specified <paramref name="pathData"/>
        /// </summary>
        /// <param name="pathData">The path data</param>
        public static implicit operator VectorSource(string pathData) => FromPathData(pathData);

        /// <summary>
        /// Create a vector source from the specified <paramref name="uriSource"/>
        /// </summary>
        /// <param name="uriSource">The URI source</param>
        public static implicit operator VectorSource(Uri uriSource) => FromUriSource(uriSource);

        #endregion
    }
}
