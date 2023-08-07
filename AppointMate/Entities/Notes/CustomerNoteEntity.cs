using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer note document in the MongoDB
    /// </summary>
    public class CustomerNoteEntity : StandardEntity, ICompanyIdentifiable<ObjectId>, ICustomerIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Message"/> property
        /// </summary>
        private string? mMessage;

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
        /// The message
        /// </summary>
        public string Message
        {
            get => mMessage ?? string.Empty;
            set => mMessage = value;
        }

        /// <summary>
        /// The type
        /// </summary>
        public CustomerNoteType Type { get; set; }

        /// <summary>
        /// A flag indicating whether this note can be seen by the customer or not
        /// </summary>
        public bool IsVisibleToCustomer { get; set; }

        /// <summary>
        /// A flag indicating whether the note automatically hides
        /// </summary>
        public bool IsHidingAutomatically { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerEntity? Customer { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerNoteEntity() : base()
        {

        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CustomerNoteEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="customer">The customer</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CustomerNoteEntity FromRequestModel(CustomerEntity customer, CustomerNoteRequestModel model)
        {
            var entity = new CustomerNoteEntity();

            DI.Mapper.Map(model, entity);
            entity.Customer = customer.ToEmbeddedEntity();

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerNoteResponseModel"/> from the current <see cref="CustomerNoteEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerNoteResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerNoteResponseModel>(this);

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Type: {Type}, Customer:";

        #endregion
    }
}
