namespace MeetBase.Web
{
    /// <summary>
    /// Request model used for logging a user in
    /// </summary>
    public class LogInRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The user's username or email
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string? Password { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LogInRequestModel()
        {

        }

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="userNameOrEmail">The user's username or email</param>
        /// <param name="password">The user's password</param>
        public LogInRequestModel(string userNameOrEmail, string password)
        {
            if (userNameOrEmail.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userNameOrEmail));

            Username = userNameOrEmail;

            if (password.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(password));

            Password = password;
        }

        #endregion
    }

    /// <summary>
    /// The reset password request model
    /// </summary>
    public class ResetPasswordRequestModel
    {
        #region Pubic Properties

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's temporary password
        /// </summary>
        public string TemporaryPassword { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user's confirm password
        /// </summary>
        public string ConfirmPassword { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="temporaryPassword">The user's temporary password</param>
        /// <param name="password">The user's password</param>
        /// <param name="confirmPassword">The user's confirm password</param>
        public ResetPasswordRequestModel(string? email, string? temporaryPassword, string? password, string? confirmPassword)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            TemporaryPassword = temporaryPassword ?? throw new ArgumentNullException(nameof(temporaryPassword));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            ConfirmPassword = confirmPassword ?? throw new ArgumentNullException(nameof(confirmPassword));
        }

        #endregion
    }
}
