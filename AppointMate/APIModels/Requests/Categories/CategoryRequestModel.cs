namespace AppointMate
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public class CategoryRequestModel : StandardRequestModel, IParentable<string?>, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Parent"/> property
        /// </summary>
        private string? mParent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The parent
        /// </summary>
        public string? Parent { get; set; }
        
        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CategoryRequestModel()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CategoryRequestModel"/> used when embedding
    /// </summary>
    public class EmbeddedCategoryRequestModel : EmbeddedStandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCategoryRequestModel() : base()
        {

        }

        #endregion
    }
}