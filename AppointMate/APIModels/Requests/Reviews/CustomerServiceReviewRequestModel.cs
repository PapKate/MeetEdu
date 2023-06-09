namespace AppointMate
{
    /// <summary>
    /// Request model used for a review
    /// </summary>
    public class CustomerServiceReviewRequestModel : BaseRequestModel
    {
        #region Public Properties

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
