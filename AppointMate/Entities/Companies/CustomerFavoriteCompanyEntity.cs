using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Only entity
    /// </summary>
    public class CustomerFavoriteCompanyEntity : DateEntity, ICompanyIdentifiable<ObjectId>, ICustomerIdentifiable<ObjectId>
    {
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
        /// The company
        /// </summary>
        public EmbeddedCompanyEntity? Comppany { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerFavoriteCompanyEntity() : base()
        {

        }

        #endregion
    }
}
