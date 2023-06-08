namespace AppointMate
{
    /// <summary>
    /// The company response model
    /// </summary>
    public class CompanyEntity : StandardEntity, IImageable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Category"/> property
        /// </summary>
        private string? mCategory;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<EmbeddedCategoryEntity>? mCategories;

        #endregion

        #region Public Properties

        /// <summary>
        /// The category
        /// </summary>
        public string Category
        {
            get => mCategory ?? string.Empty;
            set => mCategory = value;
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
        /// The radius for the distance where at home services can be provided in Km
        /// </summary>
        public double AtHomeRadius { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklyScheduleEntity? WorkHours { get; set; }

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

        /// <summary>
        /// The location
        /// </summary>
        public LocationEntity? Location { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public BillingEntity? Billing { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public ShippingEntity? Shipping { get; set; }

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<EmbeddedCategoryEntity> Categories
        {
            get => mCategories ?? Enumerable.Empty<EmbeddedCategoryEntity>();
            set => mCategories = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyEntity() : base()
        {

        }

        #endregion
    }
}
