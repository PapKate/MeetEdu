
using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Mvc;

namespace MeetEdu
{
    /// <summary>
    /// The manager for the accounts
    /// </summary>
    public class AccountsRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="AccountsRepository"/>
        /// </summary>
        public static AccountsRepository Instance { get; } = new AccountsRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountsRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates the user credentials sent by the user and returns the user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> LoginAsync([FromBody] LogInRequestModel model)
        {
            var result = await ValidateUserCredentialsAsync(model);

            // If the validation failed...
            if (!result.IsSuccessful)
                // Return the bad request with the respective error message
                return result.ErrorMessage;

            return result;
        }

        /// <summary>
        /// Validates the user <paramref name="credentials"/>
        /// </summary>
        /// <param name="credentials">The credentials</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> ValidateUserCredentialsAsync(LogInRequestModel credentials)
        {
            // Make sure we have a user name
            if (credentials is null || credentials.Username.IsNullOrEmpty() || credentials.Password.IsNullOrEmpty())
                // Return error message to user
                return MeetEduWebServerConstants.InvalidLogInCredentialsErrorMessage;

            // Validate if the user credentials are correct...

            // Get the user manager
            var userManager = DI.UsersRepository;

            // Is it an email?
            var isEmail = credentials.Username.IsEmail();

            // Get the user details
            var user = isEmail 
                // Find by email
                ? await MeetEduDbMapper.Users.FirstOrDefaultAsync(x => x.Email == credentials.Username) 
                // Find by username
                : await MeetEduDbMapper.Users.FirstOrDefaultAsync(x => x.Username == credentials.Username);

            // If we failed to find a user...
            if (user is null)
                // Return error message to user
                return MeetEduWebServerConstants.InvalidLogInCredentialsErrorMessage;

            // If we got here we have a user...
            // Let's validate the password

            // Get if password is valid
            var isValidPassword = user.PasswordHash == credentials.Password.EncryptPassword();

            // If the password was wrong
            if (!isValidPassword)
                // Return error message to user
                return MeetEduWebServerConstants.InvalidLogInCredentialsErrorMessage;

            // Return the successful result
            return new WebServerFailable<UserEntity>(user);
        }
        #endregion
    }
}
