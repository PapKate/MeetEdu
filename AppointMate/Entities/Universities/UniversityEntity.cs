namespace AppointMate
{
    /// <summary>
    /// Represents a university document in the MongoDB
    /// </summary>
    public class UniversityEntity : StandardEntity, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Fields"/> property
        /// </summary>
        private IEnumerable<string>? mFields;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityEntity() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="UniversityEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedUniversityEntity : EmbeddedStandardEntity, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? ImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedUniversityEntity() : base()
        {

        }

        #endregion
    }
}
