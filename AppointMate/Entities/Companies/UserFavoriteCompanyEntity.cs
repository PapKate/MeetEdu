using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Only entity
    /// </summary>
    public class UserFavoriteCompanyEntity : DateEntity, ICompanyIdentifiable<ObjectId>, IUserIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId CompanyId { get; set; }

        /// <summary>
        /// The user id
        /// </summary>
        public ObjectId UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserFavoriteCompanyEntity() : base()
        {

        }

        #endregion
    }
}
