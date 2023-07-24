namespace AppointMate
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public class CustomerResponseModel : DateResponseModel, ICompanyIdentifiable<string>, IUserIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CompanyId"/> property
        /// </summary>
        private string? mCompanyId;

        /// <summary>
        /// The member of the <see cref="UserId"/> property
        /// </summary>
        private string? mUserId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string CompanyId
        { 
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

        /// <summary>
        /// The id of the user
        /// </summary>
        public string UserId 
        { 
            get => mUserId ?? string.Empty; 
            set => mUserId = value; 
        }

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

    /// <summary>
    /// A minimal version of the <see cref="CustomerResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedCustomerResponseModel : EmbeddedBaseResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CompanyId"/> property
        /// </summary>
        private string? mCompanyId;

        /// <summary>
        /// The member of the <see cref="UserId"/> property
        /// </summary>
        private string? mUserId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string CompanyId
        {
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

        /// <summary>
        /// The id of the user
        /// </summary>
        public string UserId
        {
            get => mUserId ?? string.Empty;
            set => mUserId = value;
        }

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
