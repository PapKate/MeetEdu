using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing universities
    /// </summary>
    public class UniversitiesRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="UniversitiesRepository"/>
        /// </summary>
        public static UniversitiesRepository Instance { get; } = new UniversitiesRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversitiesRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a university
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UniversityEntity>> AddUniversityAsync(UniversityRequestModel model, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.Universities.AddAsync(UniversityEntity.FromRequestModel(model), cancellationToken);

        /// <summary>
        /// Updates the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UniversityEntity>> UpdateUniversityAsync(ObjectId id, UniversityRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.Universities.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Universities));

            return entity;
        }

        /// <summary>
        /// Deletes the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The university id</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UniversityEntity>> DeleteUniversityAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.Universities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Universities));

            await AppointMateDbMapper.Universities.DeleteAsync(id, cancellationToken);

            return entity;
        }

        #region Labels

        /// <summary>
        /// Add a university label
        /// </summary>
        /// <param name="universitiesId">The university id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> AddUniversityLabelAsync(ObjectId universitiesId, LabelRequestModel model, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.UniversityLabels.AddAsync(LabelEntity.FromRequestModel(model, universitiesId), cancellationToken);

        /// <summary>
        /// Updates the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateUniversityLabelAsync(ObjectId id, LabelRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.UniversityLabels.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.UniversityLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the university label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteUniversityLabelAsync(ObjectId id, CancellationToken cancellationToken = default)
                => await AppointMateDbMapper.UniversityLabels.DeleteAsync(id, cancellationToken);

        #endregion

        #endregion
    }
}
