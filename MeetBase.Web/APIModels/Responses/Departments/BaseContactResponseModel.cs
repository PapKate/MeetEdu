namespace MeetBase.Web
{
    /// <summary>
    /// The base model for a contact entity
    /// </summary>
    public abstract class BaseContactResponseModel : DateResponseModel, IMemberIdentifiable<string?>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="FirstName"/> property
        /// </summary>
        private string? mFirstName;

        /// <summary>
        /// The member of the <see cref="LastName"/> property
        /// </summary>
        private string? mLastName;

        /// <summary>
        /// The member of the <see cref="Message"/> property
        /// </summary>
        private string? mMessage;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The member id
        /// </summary>
        public string? MemberId { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName
        {
            get => mFirstName ?? string.Empty;
            set => mFirstName = value;
        }

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName
        {
            get => mLastName ?? string.Empty;
            set => mLastName = value;
        }

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message
        {
            get => mMessage ?? string.Empty;
            set => mMessage = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseContactResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="EmbeddedBaseContactResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public abstract class EmbeddedBaseContactResponseModel : EmbeddedBaseResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The email
        /// </summary>
        public string Email
        {
            get => mEmail ?? string.Empty;
            set => mEmail = value;
        }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedBaseContactResponseModel() : base()
        {

        }

        #endregion
    }
}
