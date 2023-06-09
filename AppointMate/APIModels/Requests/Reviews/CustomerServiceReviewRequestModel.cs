namespace AppointMate
{
    public class CustomerServiceReviewRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The customer service
        /// </summary>
        public IEnumerable<EmbeddedCustomerServiceSessionRequestModel>? CustomerServiceSessions { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The number of stars
        /// </summary>
        public uint? NumberOfStars { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceReviewRequestModel() : base()
        {

        }

        #endregion
    }
}
