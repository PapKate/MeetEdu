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
