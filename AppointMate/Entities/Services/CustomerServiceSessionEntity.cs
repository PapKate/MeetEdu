using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer service session document in the MongoDB
    /// </summary>
    public class CustomerServiceSessionEntity : StandardEntity, IDescriptable, ICompanyIdentifiable<ObjectId>, ICustomerIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The customer id
        /// </summary>
        public ObjectId CustomerId { get; set; }

        /// <summary>
        /// The service id
        /// </summary>
        public ObjectId ServiceId { get; set; }

        /// <summary>
        /// The number of the session
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange DateAndTime { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// A flag indicating whether it is confirmed or not
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// The session status
        /// </summary>
        public SessionStatus SessionStatus { get; set; }

        /// <summary>
        /// The staff member
        /// </summary>
        public EmbeddedStaffMemberEntity? StaffMember { get; set; }

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedCustomerServiceEntity? CustomerService { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceSessionEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="ServiceEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CustomerServiceSessionEntity FromRequestModel(CustomerServiceSessionRequestModel model)
        {
            var entity = new CustomerServiceSessionEntity();

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerServiceSessionResponseModel"/> from the current <see cref="CustomerServiceSessionEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerServiceSessionResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerServiceSessionResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedCustomerServiceSessionEntity"/> from the current <see cref="CustomerServiceSessionEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedCustomerServiceSessionEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedCustomerServiceSessionEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CustomerServiceSessionEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedCustomerServiceSessionEntity : EmbeddedStandardEntity
    {
        #region Public Properties

        /// <summary>
        /// The number of the session
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange DateAndTime { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// A flag indicating whether it is confirmed or not
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// The session status
        /// </summary>
        public SessionStatus SessionStatus { get; set; }

        /// <summary>
        /// The service
        /// </summary>
        public EmbeddedCustomerServiceEntity? CustomerService { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCustomerServiceSessionEntity() : base()
        {

        }

        #endregion
    }
}
