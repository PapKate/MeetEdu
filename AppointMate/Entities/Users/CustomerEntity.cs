using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer document in the MongoDB
    /// </summary>
    public class CustomerEntity: DateEntity, ICompanyIdentifiable<ObjectId>, IUserIdentifiable<ObjectId>
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
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CustomerEntity FromRequestModel(ObjectId companyId, ObjectId userId, CustomerRequestModel model)
        {
            var entity = new CustomerEntity();

            DI.Mapper.Map(model, entity);
            entity.CompanyId = companyId;
            entity.UserId = userId; 

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerResponseModel"/> from the current <see cref="CustomerEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerResponseModel>(this);

        /// <summary>
        /// Updates the values of the specified <paramref name="entity"/> with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static async void UpdateNonAutoMapperValues(CustomerRequestModel model, CustomerEntity entity)
        {
            var customerSessions = await AppointMateDbMapper.CustomerServiceSessions.SelectAsync(x => x.CustomerId == entity.Id);
            entity.TotalAppointments = (uint)customerSessions.Count();

            var customerFavoriteCompanies = await AppointMateDbMapper.CustomerFavoriteCompanies.SelectAsync(x => x.CustomerId == entity.Id);
            entity.TotalFavoriteCompanies = (uint)customerSessions.Count();
            
            var customerReviews = await AppointMateDbMapper.CustomerServiceReviews.SelectAsync(x => x.CustomerId == entity.Id);
            entity.TotalReviews = (uint)customerReviews.Count();
        }

        /// <summary>
        /// Creates and returns a <see cref="EmbeddedCustomerEntity"/> from the current <see cref="CustomerEntity"/>
        /// </summary>
        /// <returns></returns>
        public EmbeddedCustomerEntity ToEmbeddedEntity()
            => EntityHelpers.ToEmbeddedEntity<EmbeddedCustomerEntity>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="CustomerEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedCustomerEntity : EmbeddedBaseEntity, ICompanyIdentifiable<ObjectId>, IUserIdentifiable<ObjectId>
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
