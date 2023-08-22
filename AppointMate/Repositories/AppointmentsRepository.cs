using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing appointments
    /// </summary>
    public class AppointmentsRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="AppointmentsRepository"/>
        /// </summary>
        public static AppointmentsRepository Instance { get; } = new AppointmentsRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentsRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Appointments

        /// <summary>
        /// Adds an appointment
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentEntity>> AddAppointmentAsync(AppointmentRequestModel model)
        {
            var entity = await AppointmentEntity.FromRequestModelAsync(model);

            // If no appointment can be created
            if (entity is null)
                return "Cannot create a new appointment";

            return await AppointMateDbMapper.Appointments.AddAsync(entity);
        }

        /// <summary>
        /// Updates the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentEntity>> UpdateAppointmentAsync(ObjectId id, AppointmentRequestModel model)
        {
            var entity = await AppointMateDbMapper.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Appointments));

            entity = await AppointmentEntity.FromRequestModelAsync(model);
            
            await AppointMateDbMapper.Appointments.UpdateAsync(entity!);

            return entity!;
        }

        /// <summary>
        /// Deletes the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> DeleteAppointmentAsync(ObjectId id)
        {
            // Gets the appointment
            var entity = await AppointMateDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == id);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.AppointmentRules));

            await AppointMateDbMapper.AppointmentRules.DeleteAsync(id);

            return entity;
        }

        #endregion

        #region Appointment Rules

        /// <summary>
        /// Adds an appointment rule
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> AddAppointmentRuleAsync(AppointmentRuleRequestModel model)
        {
            var entity = AppointmentRuleEntity.FromRequestModel(model);

            // If no appointment can be created
            if (entity is null)
                return "Cannot create a new appointment";

            return await AppointMateDbMapper.AppointmentRules.AddAsync(entity);
        }

        /// <summary>
        /// Updates the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> UpdateAppointmentRuleAsync(ObjectId id, AppointmentRuleRequestModel model)
        {
            var entity = await AppointMateDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == id);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.AppointmentRules));

            entity = AppointmentRuleEntity.FromRequestModel(model);

            await AppointMateDbMapper.AppointmentRules.UpdateAsync(entity!);

            return entity!;
        }

        /// <summary>
        /// Deletes the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> DeleteAppointmentRuleAsync(ObjectId id)
        {
            // Gets the appointment
            var entity = await AppointMateDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == id);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.AppointmentRules));

            await AppointMateDbMapper.AppointmentRules.DeleteAsync(id);

            return entity;
        }

        #endregion

        #endregion
    }
}
