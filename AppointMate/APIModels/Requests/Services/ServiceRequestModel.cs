namespace AppointMate
{
    /// <summary>
    /// Request model used for a service
    /// </summary>
    public class ServiceRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string? CompanyId { get; set; }

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
        /// A flag indicating whether it has multiple sessions
        /// </summary>
        public bool HasMultipleSessions { get; set; }

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
}
