namespace AppointMate
{
    /// <summary>
    /// Represents a user document in the MongoDB
    /// </summary>
    public class UserEntity: DateEntity, IImageable, IEmailable, IPhoneable
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
        /// The member of the <see cref="Username"/> property
        /// </summary>
        private string? mUsername;

        /// <summary>
        /// The member of the <see cref="PasswordHash"/> property
        /// </summary>
        private string? mPasswordHash;

        /// <summary>
        /// The member of the <see cref="Email"/> property
        /// </summary>
        private string? mEmail;

        #endregion

        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string Username
        {
            get => mUsername ?? string.Empty;
            set => mUsername = value;
        }

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
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public string PasswordHash
        {
            get => mPasswordHash ?? string.Empty;
            set => mPasswordHash = value;
        }

        /// <summary>
        /// A flag indicating whether the email is confirmed or not
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

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
        /// The birthday
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public Shipping? Shipping { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public Billing? Billing { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="UserEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static UserEntity FromRequestModel(UserRequestModel model)
        {
            var entity = new UserEntity();

            DI.Mapper.Map(model, entity);
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="UserResponseModel"/> from the current <see cref="UserEntity"/>
        /// </summary>
        /// <returns></returns>
        public UserResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<UserResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedUserEntity"/> from the current <see cref="UserEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedUserEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedUserEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="UserEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedUserEntity : EmbeddedStandardEntity
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
        /// The member of the <see cref="Username"/> property
        /// </summary>
        private string? mUsername;

        #endregion

        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string Username
        {
            get => mUsername ?? string.Empty;
            set => mUsername = value;
        }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedUserEntity() : base()
        {

        }

        #endregion
    }
}
