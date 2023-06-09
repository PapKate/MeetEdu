namespace AppointMate
{
    /// <summary>
    /// The company response model
    /// </summary>
    public class CompanyRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The category
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The radius for the distance where at home services can be provided in Km
        /// </summary>
        public double? AtHomeRadius { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklyScheduleResponseModel? WorkHours { get; set; }

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
        public LocationRequestModel? Location { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public BillingRequestModel? Billing { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public ShippingRequestModel? Shipping { get; set; }

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<EmbeddedCategoryRequestModel>? Categories { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyRequestModel() : base()
        {

        }

        #endregion
    }
}
