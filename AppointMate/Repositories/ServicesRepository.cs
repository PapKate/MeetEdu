using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing services
    /// </summary>
    public class ServicesRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="ServicesRepository"/>
        /// </summary>
        public static ServicesRepository Instance { get; } = new ServicesRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServicesRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a service
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<ServiceEntity> AddServiceAsync(ServiceRequestModel model)
            => await AppointMateDbMapper.Services.AddAsync(ServiceEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of services 
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<ServiceEntity>>> AddServicesAsync(IEnumerable<ServiceRequestModel> models)
            => new WebServerFailable<IEnumerable<ServiceEntity>>(await AppointMateDbMapper.Services.AddRangeAsync(models.Select(ServiceEntity.FromRequestModel).ToList()));

        /// <summary>
        /// Updates the service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ServiceEntity>> UpdateServiceAsync(ObjectId id, ServiceRequestModel model)
        {
            var entity = await AppointMateDbMapper.Services.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Services));

            return entity;
        }

        /// <summary>
        /// Deletes the service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<ServiceEntity>> DeleteServiceAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Services));

            await AppointMateDbMapper.Services.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #region Labels

        /// <summary>
        /// Add a service label
        /// </summary>
        /// <param name="serviceId">The service id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> AddServiceLabelAsync(ObjectId serviceId, LabelRequestModel model)
            => await AppointMateDbMapper.ServiceLabels.AddAsync(LabelEntity.FromRequestModel(model, serviceId));

        /// <summary>
        /// Adds a list of service labels
        /// </summary>
        /// <param name="serviceId">The service id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<LabelEntity>>> AddServiceLabelsAsync(ObjectId serviceId, IEnumerable<LabelRequestModel> models)
            => new WebServerFailable<IEnumerable<LabelEntity>>(await AppointMateDbMapper.ServiceLabels.AddRangeAsync(models.Select(x => LabelEntity.FromRequestModel(x, serviceId)).ToList()));

        /// <summary>
        /// Updates the service with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateServiceLabelAsync(ObjectId id, LabelRequestModel model)
        {
            var entity = await AppointMateDbMapper.ServiceLabels.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.ServiceLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the service label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteServiceLabelAsync(ObjectId id)
                => await AppointMateDbMapper.ServiceLabels.DeleteAsync(id);

        #endregion

        #endregion
    }
}
