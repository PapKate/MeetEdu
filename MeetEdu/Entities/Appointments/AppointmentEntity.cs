using AutoMapper;

using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents an appointment document in the MongoDB
    /// </summary>
    public class AppointmentEntity : DepartmentContactMessageEntity, IProfessorIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// The appointment rule id
        /// </summary>
        public ObjectId RuleId { get; set; }

        /// <summary>
        /// The date
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// The time that the appointment begins
        /// </summary>
        public TimeOnly TimeStart { get; set; }

        /// <summary>
        /// The time that the appointment ends
        /// </summary>
        public TimeOnly TimeEnd { get; set; }

        /// <summary>
        /// A flag indicating whether the subscription was canceled or not
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// The date the customer subscription was canceled
        /// </summary>
        public DateTimeOffset? DateCanceled { get; set; }

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// The rule
        /// </summary>
        public EmbeddedAppointmentRuleEntity? Rule { get; set; }

        /// <summary>
        /// The professor
        /// </summary>
        public EmbeddedProfessorEntity? Professor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentEntity() : base()
        {

        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="AppointmentEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<AppointmentEntity?> FromRequestModelAsync(AppointmentRequestModel model)
        {
            // If no rule or professor id is specified...
            if (model.RuleId is null || model.ProfessorId is null)
                return null;

            // Gets the appointment rule with the specified id
            var rule = await MeetEduDbMapper.AppointmentRules.FirstOrDefaultAsync(x => x.Id == model.RuleId.ToObjectId());
            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == model.ProfessorId.ToObjectId());

            // If no rule or professor is found...
            if (rule is null || professor is null)
                // Return
                return null;

            var entity = new AppointmentEntity();

            DI.Mapper.Map(model, entity);
            entity.Rule = rule.ToEmbeddedEntity();
            entity.Professor = professor.ToEmbeddedEntity();
            entity.DateModified = DateTime.UtcNow;

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="AppointmentResponseModel"/> from the current <see cref="AppointmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public new AppointmentResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<AppointmentResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedAppointmentEntity"/> from the current <see cref="AppointmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public new EmbeddedAppointmentEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedAppointmentEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="AppointmentEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedAppointmentEntity : EmbeddedDepartmentContactMessageEntity, IProfessorIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// The appointment rule id
        /// </summary>
        public ObjectId RuleId { get; set; }

        /// <summary>
        /// The date
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// The time that the appointment begins
        /// </summary>
        public TimeOnly TimeStart { get; set; }

        /// <summary>
        /// The time that the appointment ends
        /// </summary>
        public TimeOnly TimeEnd { get; set; }

        /// <summary>
        /// A flag indicating whether the subscription was canceled or not
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// The rule
        /// </summary>
        public EmbeddedAppointmentRuleEntity? Rule { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedAppointmentEntity() : base()
        {

        }

        #endregion
    }
}
