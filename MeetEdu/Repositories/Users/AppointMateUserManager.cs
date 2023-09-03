using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace MeetEdu
{
    /// <summary>
    /// The <see cref="UserManager{TUser}"/> implementation that is used by the Atom framework
    /// </summary>
    public class MeetEduUserManager : UserManager<UserEntity>
    {
        #region Public Properties

        /// <summary>
        /// The token provider used for generating 6 digit verification tokens
        /// </summary>
        public static EmailTokenProvider<UserEntity> EmailTokenProvider { get; } = new EmailTokenProvider<UserEntity>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetEduUserManager(IUserStore<UserEntity> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserEntity> passwordHasher, IEnumerable<IUserValidator<UserEntity>> userValidators, IEnumerable<IPasswordValidator<UserEntity>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserEntity>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates a password reset token for the specified user, using the configured
        /// password reset token provider.
        /// </summary>
        /// <param name="user">The user to generate a password reset token for.</param>
        /// <returns></returns>
        public override Task<string> GeneratePasswordResetTokenAsync(UserEntity user)
            => EmailTokenProvider.GenerateAsync(ResetPasswordTokenPurpose, this, user);

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate an email confirmation token for.</param>
        /// <returns></returns>
        public override Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user)
            => EmailTokenProvider.GenerateAsync(ConfirmEmailTokenPurpose, this, user);

        /// <summary>
        /// Returns a flag indicating whether the specified token is valid for the given
        /// user and purpose.
        /// </summary>
        /// <param name="user">The user to validate the token against.</param>
        /// <param name="tokenProvider">The token provider used to generate the token.</param>
        /// <param name="purpose">The purpose the token should be generated for.</param>
        /// <param name="token">The token to validate</param>
        /// <returns></returns>
        public override Task<bool> VerifyUserTokenAsync(UserEntity user, string tokenProvider, string purpose, string token)
        {
            if (purpose == ResetPasswordTokenPurpose || purpose == ConfirmEmailTokenPurpose)
                return EmailTokenProvider.ValidateAsync(purpose, token, this, user);

            return base.VerifyUserTokenAsync(user, tokenProvider, purpose, token);
        }

        /// <summary>
        /// Changes the password of the specified user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="newPassword">The new password</param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePasswordAsync(UserEntity user, string newPassword)
        {
            foreach (var passwordValidator in PasswordValidators)
            {
                var passwordResult = await passwordValidator.ValidateAsync(this, user, newPassword);
                if (!passwordResult.Succeeded)
                    return passwordResult;
            }

            var newPasswordHash = PasswordHasher.HashPassword(user, newPassword);

            user.PasswordHash = newPassword;

            await MeetEduDbMapper.Users.UpdateOneAsync(
                x => x.Id == user.Id,
                Builders<UserEntity>.Update.Set(x => x.PasswordHash, newPasswordHash));

            return IdentityResult.Success;
        }

        #endregion
    }
}
