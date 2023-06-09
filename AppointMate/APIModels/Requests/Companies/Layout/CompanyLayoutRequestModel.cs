namespace AppointMate
{
    /// <summary>
    /// Request model used for a company layout
    /// </summary>
    public class CompanyLayoutRequestModel : BaseRequestModel, ICompanyIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CompanyId"/> property
        /// </summary>
        private string? mCompanyId;

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
        /// The description
        /// </summary>
        public string? Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyLayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
