namespace MeetBase.Web
{
    /// <summary>
    /// Request model used for a user
    /// </summary>
    public class UserRequestModel : BaseRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// A flag indicating whether the email is confirmed or not
        /// </summary>
        public bool? IsEmailConfirmed { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The birthday
        /// </summary>
        public DateOnly? DateOfBirth { get; set; }

        /// <summary>
        /// The location information
        /// </summary>
        public Location? Location { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserRequestModel() : base()
        {

        }

        #endregion
    }
}
