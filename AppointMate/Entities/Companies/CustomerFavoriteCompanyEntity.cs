namespace AppointMate
{
    /// <summary>
    /// Only entity
    /// </summary>
    public class CustomerFavoriteCompanyEntity: BaseEntity
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
        public CustomerFavoriteCompanyEntity() : base()
        {

        }

        #endregion
    }
}
