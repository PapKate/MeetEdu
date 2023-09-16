using MongoDB.Bson;
using MongoDB.Driver;

using System.Linq;

namespace MeetEdu
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
            => await MeetEduDbMapper.Universities.AddAsync(UniversityEntity.FromRequestModelAsync(model), cancellationToken);

        /// <summary>
        /// Updates the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UniversityEntity>> UpdateUniversityAsync(ObjectId id, UniversityRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.Universities.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Universities));

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
            var entity = await MeetEduDbMapper.Universities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Universities));

            await MeetEduDbMapper.Universities.DeleteAsync(id, cancellationToken);

            await MeetEduDbMapper.UniversityLabels.DeleteManyAsync(Builders<LabelEntity>.Filter.Eq(x => x.DepartmentId, id), cancellationToken);

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
            => await MeetEduDbMapper.UniversityLabels.AddAsync(LabelEntity.FromRequestModel(model, universitiesId), cancellationToken);

        /// <summary>
        /// Updates the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateUniversityLabelAsync(ObjectId id, LabelRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.UniversityLabels.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.UniversityLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the university label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteUniversityLabelAsync(ObjectId id, CancellationToken cancellationToken = default)
                => await MeetEduDbMapper.UniversityLabels.DeleteAsync(id, cancellationToken);

        #endregion

        #endregion
    }
}
