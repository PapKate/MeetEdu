﻿using MongoDB.Bson;

namespace MeetEdu
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
            return await MeetEduDbMapper.Secretaries.AddAsync(await SecretaryEntity.FromRequestModelAsync(model), cancellationToken);
        }

        /// <summary>
        /// Updates the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The secretary</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<SecretaryEntity>> UpdateSecretaryAsync(ObjectId id, SecretaryRequestModel model, CancellationToken cancellationToken = default)
        {
            var secretaryEntity = await MeetEduDbMapper.Secretaries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the secretary does not exist...
            if (secretaryEntity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Secretaries));

            secretaryEntity = await MeetEduDbMapper.Secretaries.UpdateAsync(id, model, cancellationToken);

            return secretaryEntity!;
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
            var entity = await MeetEduDbMapper.Secretaries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the secretary does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Secretaries));

            // Removes the secretary from the database
            await MeetEduDbMapper.Secretaries.DeleteAsync(id, cancellationToken);
            // Removes the user from the database
            await MeetEduDbMapper.Users.DeleteAsync(entity.UserId, cancellationToken);

            return entity;
        }
        
        #endregion
    }
}
