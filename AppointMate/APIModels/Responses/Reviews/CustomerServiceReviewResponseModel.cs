namespace AppointMate
{
    /// <summary>
    /// Represents a customer service review
    /// </summary>
    public class CustomerServiceReviewResponseModel : BaseResponseModel, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerResponseModel? Customer { get; set; }

        /// <summary>
        /// The customer service
        /// </summary>
        public IEnumerable<EmbeddedCustomerServiceSessionResponseModel>? CustomerServiceSessions { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The number of stars
        /// </summary>
        public uint NumberOfStars { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceReviewResponseModel() : base()
        {

        }

        #endregion
    }
}
