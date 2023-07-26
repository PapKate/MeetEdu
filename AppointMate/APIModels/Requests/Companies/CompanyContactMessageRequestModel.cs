using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Request model used for a company contact message
    /// </summary>
    public class CompanyContactMessageRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string? CompanyId { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string? Message { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyContactMessageRequestModel() : base()
        {

        }

        #endregion
    }
}
