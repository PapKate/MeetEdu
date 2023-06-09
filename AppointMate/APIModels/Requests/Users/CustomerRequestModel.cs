namespace AppointMate
{
    /// <summary>
    /// Request model used for a customer
    /// </summary>
    public class CustomerRequestModel : UserRequestModel, ICompanyIdentifiable<string>
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
        public CustomerRequestModel() : base()
        {

        }

        #endregion
    }
}
