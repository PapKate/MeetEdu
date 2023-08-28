using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing secretaries
    /// </summary>
    public class SecretariesRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="SecretariesRepository"/>
        /// </summary>
        public static SecretariesRepository Instance { get; } = new SecretariesRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SecretariesRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a secretary
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<SecretaryEntity>> AddSecretaryAsync(SecretaryRequestModel model, CancellationToken cancellationToken = default)
        {
            return await AppointMateDbMapper.Secretaries.AddAsync(await SecretaryEntity.FromRequestModelAsync(model), cancellationToken);
        }

        /// <summary>
        /// Updates the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="secretary">The secretary</param>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<SecretaryEntity>> UpdateSecretaryAsync(ObjectId id, SecretaryRequestModel secretary, UserRequestModel user, CancellationToken cancellationToken = default)
        {
            var secretaryEntity = await AppointMateDbMapper.Secretaries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the secretary does not exist...
            if (secretaryEntity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Secretaries));

            secretaryEntity = await AppointMateDbMapper.Secretaries.UpdateAsync(id, secretary, cancellationToken);

            var userEntity = await AppointMateDbMapper.Users.FirstAsync(secretaryEntity!.UserId, cancellationToken);

            userEntity = await AppointMateDbMapper.Users.UpdateAsync(userEntity.Id, user, cancellationToken);

            // If the user exists...
            if (user is not null)
                secretaryEntity.User = userEntity!.ToEmbeddedEntity();

            return secretaryEntity;
        }

        /// <summary>
        /// Deletes the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<SecretaryEntity>> DeleteSecretaryAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            // Gets the secretary
            var entity = await AppointMateDbMapper.Secretaries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the secretary does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Secretaries));

            // Removes the secretary from the database
            await AppointMateDbMapper.Secretaries.DeleteAsync(id, cancellationToken);
            // Removes the user from the database
            await AppointMateDbMapper.Users.DeleteAsync(entity.UserId, cancellationToken);

            return entity;
        }

        #endregion
    }
}
