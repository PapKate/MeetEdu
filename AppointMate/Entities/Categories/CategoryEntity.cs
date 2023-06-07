using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public class CategoryEntity : StandardEntity, IDescriptable, IParentable<ObjectId>, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The parent
        /// </summary>
        public ObjectId Parent { get; set; }

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
        public CategoryEntity()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CategoryEntity "/> used when embedding
    /// </summary>
    public class EmbeddedCategoryEntity : EmbeddedStandardEntity
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
        public EmbeddedCategoryEntity() : base()
        {

        }

        #endregion
    }
}