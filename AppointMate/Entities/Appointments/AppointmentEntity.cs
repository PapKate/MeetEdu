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
        public static async Task<WebServerFailable<AppointmentEntity>> FromRequestModelAsync(CustomerServiceRequestModel model)
        {
            // If no customer id was specified...
            if(model.CustomerId is null)
                return AppointMateWebServerConstants.NoCustomerIdSpecifiedInTheRequestErrorMessage;

            // If no service id was specified...
            if (model.ServiceId is null)
                return AppointMateWebServerConstants.NoServiceIdSpecifiedInTheRequestErrorMessage;

            // Gets the customer with the specified id
            var customer = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == model.CustomerId.ToObjectId());

            // If no customer is found...
            if (customer is null)
                // Return
                return WebServerFailable.NotFound(model.CustomerId, nameof(AppointMateDbMapper.Customers));

            // Gets the service with the specified id
            var service = await AppointMateDbMapper.Services.FirstOrDefaultAsync(x => x.Id == model.ServiceId.ToObjectId());

            var entity = new AppointmentEntity();

            DI.Mapper.Map(model, entity);


            entity.DateCreated = DateTime.UtcNow;
            entity.DateModified = DateTime.UtcNow;

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerServiceResponseModel"/> from the current <see cref="AppointmentEntity"/>
        /// </summary>
        /// <returns></returns>
        public new CustomerServiceResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerServiceResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedMemberEntity"/> from the current <see cref="AppointmentEntity"/>
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
