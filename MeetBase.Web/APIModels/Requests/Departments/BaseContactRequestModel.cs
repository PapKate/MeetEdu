namespace MeetBase.Web
{
    /// <summary>
    /// The base request model for a contact
    /// </summary>
    public abstract class BaseContactRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The member id
        /// </summary>
        public string? MemberId { get; set; }

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
        public BaseContactRequestModel() : base()
        {

        }

        #endregion
    }
}
