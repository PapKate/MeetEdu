using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer document in the MongoDB
    /// </summary>
    public class CustomerEntity: UserEntity, ICompanyIdentifiable<ObjectId>, IUserIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        /// <summary>
        /// The total number of appointments
        /// </summary>
        public uint TotalAppointments { get; set; }

        /// <summary>
        /// The total number of favorite companies
        /// </summary>
        public uint TotalFavoriteCompanies { get; set; }

        /// <summary>
        /// The total number of reviews
        /// </summary>
        public uint TotalReviews { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CustomerEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CustomerEntity FromRequestModel(CustomerRequestModel model)
        {
            var entity = new CustomerEntity();

            DI.Mapper.Map(model, entity);
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerResponseModel"/> from the current <see cref="CustomerEntity"/>
        /// </summary>
        /// <returns></returns>
        public new CustomerResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerResponseModel>(this);

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedCustomerEntity"/> from the current <see cref="CustomerEntity"/>
        /// </summary>
        /// <returns></returns>
        public new EmbeddedCustomerEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedCustomerEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CustomerEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedCustomerEntity : EmbeddedUserEntity, ICompanyIdentifiable<ObjectId>, IUserIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        public ObjectId UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCustomerEntity() : base()
        {

        }

        #endregion
    }
}
