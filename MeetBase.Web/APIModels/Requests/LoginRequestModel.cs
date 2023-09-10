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
}
