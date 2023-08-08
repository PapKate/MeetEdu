namespace AppointMate
{
    /// <summary>
    /// Represents a company
    /// </summary>
    public class CompanyResponseModel : StandardResponseModel, IImageable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<CompanyType>? mCategories;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelResponseModel>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<CompanyType> Categories
        {
            get => mCategories ?? Enumerable.Empty<CompanyType>();
            set => mCategories = value;
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
        /// A flag indicating whether the company provides at home services
        /// </summary>
        public bool HasAtHomeServices { get; set; }

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
        public WeeklySchedule? WorkHours { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public Billing? Billing { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public Shipping? Shipping { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelResponseModel> Labels
        {
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelResponseModel>();
            set => mLabels = value;
        }

        /// <summary>
        /// The average number of stars from the customer reviews
        /// </summary>
        public double TotalReviewStars { get; set; }

        /// <summary>
        /// The number of customer reviews
        /// </summary>
        public uint TotalReviews { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyResponseModel() : base()
        {

        }

        #endregion
    }


    /// <summary>
    /// The embedded company
    /// </summary>
    public class EmbeddedCompanyResponseModel : EmbeddedStandardResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<CompanyType>? mCategories;

        #endregion

        #region Public Properties

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<CompanyType> Categories
        {
            get => mCategories ?? Enumerable.Empty<CompanyType>();
            set => mCategories = value;
        }

        /// <summary>
        /// The average number of stars from the customer reviews
        /// </summary>
        public double TotalReviewStars { get; set; }

        /// <summary>
        /// The number of customer reviews
        /// </summary>
        public uint TotalReviews { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCompanyResponseModel() : base()
        {

        }

        #endregion
    }
}
