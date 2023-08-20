using AutoMapper;

using MongoDB.Bson;

namespace AppointMate
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
        /// The appointment template id
        /// </summary>
        public ObjectId SubjectId { get; set; }

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
        /// The subject
        /// </summary>
        public EmbeddedAppointmentTemplateEntity? Subject { get; set; }

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
        public static async Task<WebServerFailable<AppointmentEntity>> FromRequestModelAsync(AppointmentRequestModel model)
        {
            // If no subject id was specified...
            if(model.SubjectId is null)
                return AppointMateWebServerConstants.NoAppointmentTemplateIdSpecifiedInTheRequestErrorMessage;

            // Gets the appointment template with the specified id
            var template = await AppointMateDbMapper.AppointmentTemplates.FirstOrDefaultAsync(x => x.Id == model.SubjectId.ToObjectId());

            // If no template is found...
            if (template is null)
                // Return
                return WebServerFailable.NotFound(model.SubjectId, nameof(AppointMateDbMapper.AppointmentTemplates));

            var entity = new AppointmentEntity();

            DI.Mapper.Map(model, entity);
            entity.Subject = template.ToEmbeddedEntity();
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
        /// The appointment template id
        /// </summary>
        public ObjectId SubjectId { get; set; }

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
        /// The subject
        /// </summary>
        public EmbeddedAppointmentTemplateEntity? Subject { get; set; }

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
