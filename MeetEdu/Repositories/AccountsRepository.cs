using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<WebServerFailable<LoginResult>> LoginAsync([FromBody] LogInRequestModel model)
        {
            var result = await ValidateUserCredentialsAsync(model);

            // If the validation failed...
            if (!result.IsSuccessful)
                // Return the bad request with the respective error message
                return result.ErrorMessage;

            var user = result.Result!;

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var secretary = await MeetEduDbMapper.Secretaries.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (secretary is not null)
                claims.Add(new(JwtTokenConstants.SecretaryIdClaimType, secretary.Id.ToString()));

            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (professor is not null)
                claims.Add(new(JwtTokenConstants.ProfessorIdClaimType, professor.Id.ToString()));

            //var member = await MeetEduDbMapper.Members.FirstOrDefaultAsync(x => x.UserId == user.Id);

            var token = GenerateJwtToken(claims);

            return new LoginResult(user, token)
            {
                Secretary = secretary,
                Professor = professor,
                //Member = member
            };
        }

        /// <summary>
        /// Generates a JWT bearer token containing the users username.
        /// NOTE: Since we do not encrypt the body of the token it is advisable not to put
        ///       sensitive information at the user claims!
        /// </summary>
        /// <param name="claims">The claims of the user we want to encrypt in the token</param>
        /// <returns></returns>
        public static string GenerateJwtToken(ICollection<Claim> claims)
        {
            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d7sd1rhq8QastquUv9idfdfxds4512fdfg67f")),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            // If no claims are inserter...
            if (claims == null)
                // Create a new list of claims to add the required claims
                claims = new List<Claim>();

            //// The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
            //claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username));
            //// Add user Id so that UserManager.GetUserAsync can find the user based on Id
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //// Add the current security stamp
            //claims.Add(new Claim(JwtTokenConstants.SecurityStampClaimType, user.SecurityStamp ?? string.Empty));
            //// Add the first name
            //claims.Add(new Claim(JwtTokenConstants.FirstNameClaimType, user.FirstName));
            //// Add the last name
            //claims.Add(new Claim(JwtTokenConstants.LastNameClaimType, user.LastName));

            // Generate the JWT Token
            var token = new JwtSecurityToken(
                issuer: "MeetEdu",
                audience: "MeetEdu",
                claims: claims,
                signingCredentials: credentials,
                // Expire if not used for 3 months
                expires: DateTime.Now.Add(TimeSpan.FromDays(90)));

            // Return the generated token
            return new JwtSecurityTokenHandler().WriteToken(token);
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

        /// <summary>
        /// Resets the user password
        /// </summary>
        /// <param name="credentials">The credentials</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserEntity>> ResetUserPasswordAsync(ResetPasswordRequestModel credentials)
        {
            // Get the user manager
            var userManager = DI.UsersRepository;

            // Get the user details
            var user = await MeetEduDbMapper.Users.FirstOrDefaultAsync(x => x.Email == credentials.Email);

            // If we failed to find a user...
            if (user is null)
                // Return error message to user
                return MeetEduWebServerConstants.InvalidLogInCredentialsErrorMessage;

            // If not all inputs were filled...
            if (credentials.TemporaryPassword.IsNullOrEmpty() || credentials.Password.IsNullOrEmpty() || credentials.ConfirmPassword.IsNullOrEmpty())
            {
                // Return error message to user
                return EnglishLocalization.InvalidTotalFormInputsErrorMessage;
            }

            if (credentials.TemporaryPassword != "TempCore123!@")
            {
                // Return error message to user
                return EnglishLocalization.InvalidTemporaryPasswordErrorMessage;
            }

            // If the passwords do not match
            if (credentials.Password != credentials.ConfirmPassword)
            {
                // Return error message to user
                return EnglishLocalization.InvalidConfirmPasswordInputErrorMessage;
            }

            user = await MeetEduDbMapper.Users.UpdateAsync(user.Id, new UserRequestModel() { PasswordHash = credentials.Password.EncryptPassword() });

            // Return the successful result
            return new WebServerFailable<UserEntity>(user!);
        }

        #endregion
    }
}
