namespace AppointMate
{
    /// <summary>
    /// Request model used for a category
    /// </summary>
    public class CategoryRequestModel : StandardRequestModel, IImageable
    {
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
}