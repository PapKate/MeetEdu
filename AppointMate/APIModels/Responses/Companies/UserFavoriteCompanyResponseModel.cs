namespace AppointMate
{
    /// <summary>
    /// Only entity
    /// </summary>
    public class UserFavoriteCompanyResponseModel : BaseResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The company
        /// </summary>
        public string? CompanyId { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public string? UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserFavoriteCompanyResponseModel() : base()
        {

        }

        #endregion
    }
}
