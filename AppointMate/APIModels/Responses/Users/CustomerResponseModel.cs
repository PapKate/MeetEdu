namespace AppointMate
{
    public class CustomerResponseModel : UserResponseModel, ICompanyIdentifiable
    {
        #region Private Members

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        public string UserId { get; set; }

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
        public CustomerResponseModel() : base()
        {

        }

        #endregion
    }

    public class EmbeddedCustomerResponseModel : EmbeddedUserResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The id of the user
        /// </summary>
        public string UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedCustomerResponseModel() : base()
        {

        }

        #endregion
    }

}
