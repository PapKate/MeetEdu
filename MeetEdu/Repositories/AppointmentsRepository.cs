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
            // If the professor id is empty...
            if (model.ProfessorId.IsNullOrEmpty())
                // Return error
                return "No professor was attached to this appointment.";

            // If the rule id is empty...
            if (model.RuleId.IsNullOrEmpty())
                // Return error
                return "No rule was attached to this appointment.";

            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == model.ProfessorId.ToObjectId(), cancellationToken);

            // If no professor is found...
            if (professor is null)
                // Return error
                return "No professor was found.";

            // If no professor is found...
            if (professor.WeeklySchedule is null)
                // Return error
                return "Professor has no possible appointment schedule.";

            // Gets the appointment rule with the specified id
            var rule = await MeetEduDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == model.RuleId.ToObjectId(), cancellationToken);

            // If no rule is found...
            if (rule is null)
                // Return error
                return "No rule was found.";

            var professorAppointments = await MeetEduDbMapper.Appointments.SelectAsync(x => x.ProfessorId == professor.Id, cancellationToken);

            var reservedTimeSlots = professorAppointments
                                        .Select(x => (IReadOnlyRangeable<DateTimeOffset>)new Range<DateTimeOffset>(x.DateStart, x.DateStart + rule.Duration)).ToList();

            var isValid = QuartzHelpers.IsAppointmentDateValid(model.DateStart, rule.DateFrom, rule.DateTo, professor.WeeklySchedule.WeeklyHours, rule.Duration, rule.StartMinutes, reservedTimeSlots);

            if (!isValid)
                return "Invalid selected time slot! Please try again.";

            var entity = AppointmentEntity.FromRequestModel(model);

            // If no appointment can be created
            if (entity is null)
                return "Cannot create a new appointment";

            entity.Rule = rule.ToEmbeddedEntity();
            entity.Professor = professor.ToEmbeddedEntity();

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
            var entity = await MeetEduDbMapper.Appointments.UpdateAsync(id, model, cancellationToken);

            // If the appointment does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Appointments));

            return entity;
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

            entity = await MeetEduDbMapper.AppointmentRules.UpdateAsync(id, model, cancellationToken);

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
