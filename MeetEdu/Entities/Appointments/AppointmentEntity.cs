using AutoMapper;
using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents an appointment document in the MongoDB
    /// </summary>
    public class AppointmentEntity : BaseContactEntity
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
        /// The calendar event
        /// </summary>
        public string? CalendarEvent { get; set; }

        /// <summary>
        /// The link of the meeting
        /// </summary>
        /// <remarks>ex.: Google Meet, Teams Meeting etc.</remarks>
        public string? MeetLink { get; set; }

        /// <summary>
        /// The status
        /// </summary>
        public AppointmentStatus Status { get; set; }

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
        public static AppointmentEntity FromRequestModel(AppointmentRequestModel model)
        {
            var entity = new AppointmentEntity();

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="AppointmentResponseModel"/> from the current <see cref="AppointmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public AppointmentResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<AppointmentResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedAppointmentEntity"/> from the current <see cref="AppointmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedAppointmentEntity ToEmbeddedEntity()
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
        /// The status
        /// </summary>
        public AppointmentStatus Status { get; set; }

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
