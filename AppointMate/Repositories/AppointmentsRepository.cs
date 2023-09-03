using MongoDB.Bson;

namespace MeetEdu
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
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentEntity>> AddAppointmentAsync(AppointmentRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await AppointmentEntity.FromRequestModelAsync(model);

            // If no appointment can be created
            if (entity is null)
                return "Cannot create a new appointment";

            return await MeetEduDbMapper.Appointments.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Updates the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentEntity>> UpdateAppointmentAsync(ObjectId id, AppointmentRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.Appointments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Appointments));

            entity = await AppointmentEntity.FromRequestModelAsync(model);
            
            await MeetEduDbMapper.Appointments.UpdateAsync(entity!, cancellationToken);

            return entity!;
        }

        /// <summary>
        /// Deletes the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentEntity>> DeleteAppointmentAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            // Gets the appointment
            var entity = await MeetEduDbMapper.Appointments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Appointments));

            await MeetEduDbMapper.Appointments.DeleteAsync(id, cancellationToken);

            return entity;
        }

        #endregion

        #region Appointment Rules

        /// <summary>
        /// Adds an appointment rule
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> AddAppointmentRuleAsync(AppointmentRuleRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = AppointmentRuleEntity.FromRequestModel(model);

            // If no appointment can be created
            if (entity is null)
                return "Cannot create a new appointment";

            return await MeetEduDbMapper.AppointmentRules.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Updates the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> UpdateAppointmentRuleAsync(ObjectId id, AppointmentRuleRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.AppointmentRules));

            entity = AppointmentRuleEntity.FromRequestModel(model);

            await MeetEduDbMapper.AppointmentRules.UpdateAsync(entity!, cancellationToken);

            return entity!;
        }

        /// <summary>
        /// Deletes the appointment rule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<AppointmentRuleEntity>> DeleteAppointmentRuleAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            // Gets the appointment
            var entity = await MeetEduDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.AppointmentRules));

            await MeetEduDbMapper.AppointmentRules.DeleteAsync(id, cancellationToken);

            return entity;
        }

        #endregion

        #endregion
    }
}
