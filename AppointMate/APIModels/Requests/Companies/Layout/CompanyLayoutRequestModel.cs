namespace AppointMate
{
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

        /// <summary>
        /// The rooms
        /// </summary>
        public IEnumerable<CompanyRoomRequestModel>? Rooms { get; set; }

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
