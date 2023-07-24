namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer
    /// </summary>
    public class CustomerRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string? CompanyId { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        public string? UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerRequestModel() : base()
        {

        }

        #endregion
    }
}
