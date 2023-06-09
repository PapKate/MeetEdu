namespace AppointMate
{
    /// <summary>
    /// The service response model
    /// </summary>
    public class ServiceRequestModel : StandardRequestModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Categories"/> property
        /// </summary>
        private IEnumerable<EmbeddedCategoryResponseModel>? mCategories;

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating whether it is at home
        /// </summary>
        public bool? IsAtHome { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The small description
        /// </summary>
        public string? SmallDescription { get; set; }

        /// <summary>
        /// The number of sessions range
        /// </summary>
        public SessionsRange? SessionsRange { get; set; }

        /// <summary>
        /// The indicative days between sessions
        /// </summary>
        public DaysBetweenSessionsRange? DaysBetweenSessionsRange { get; set; }

        /// <summary>
        /// The duration range
        /// </summary>
        public DurationRange? DurationRange { get; set; }

        /// <summary>
        /// A flag indicating whether it is on sale or not
        /// </summary>
        public bool? IsOnSale { get; set; }

        /// <summary>
        /// The price
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The price on sale
        /// </summary>
        public decimal? OnSalePrice { get; set; }

        /// <summary>
        /// The regular price 
        /// </summary>
        public decimal? RegularPrice { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<EmbeddedCategoryRequestModel>? Categories { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceRequestModel() : base()
        {

        }

        #endregion
    }

    public class EmbeddedServiceRequestModel : EmbeddedStandardRequestModel, ICompanyIdentifiable<string>
    {
        #region Private Members
        /// <summary>
        /// The member of the <see cref="CompanyId"/> property
        /// </summary>
        private string? mCompanyId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string CompanyId
        {
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

        /// <summary>
        /// The small description
        /// </summary>
        public string? SmallDescription { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedServiceRequestModel() : base()
        {

        }

        #endregion
    }
}
