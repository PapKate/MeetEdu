namespace AppointMate
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public class CategoryResponseModel : StandardResponseModel, IDescriptable, IParentable<string>, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Parent"/> property
        /// </summary>
        private string? mParent;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The parent
        /// </summary>
        public string Parent
        {
            get => mParent ?? string.Empty;
            set => mParent = value;
        }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

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
        public CategoryResponseModel()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CategoryResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedCategoryResponseModel : EmbeddedStandardResponseModel
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
        public string Parent
        {
            get => mParent ?? string.Empty;
            set => mParent = value;
        }

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
        public EmbeddedCategoryResponseModel() : base()
        {

        }

        #endregion
    }
}