using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Request model used for a review
    /// </summary>
    public class CustomerServiceReviewRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The customer id
        /// </summary>
        public string? CustomerId { get; set; }

        /// <summary>
        /// The service id
        /// </summary>
        public string? ServiceId { get; set; }

        /// <summary>
        /// The company id
        /// </summary>
        public string? CompanyId { get; set; }

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
