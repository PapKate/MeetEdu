namespace AppointMate
{
    /// <summary>
    /// Represents a customer service review document in the MongoDB
    /// </summary>
    public class CustomerServiceReviewEntity : BaseEntity, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The customer service
        /// </summary>
        public IEnumerable<EmbeddedCustomerServiceSessionEntity>? CustomerServiceSessions { get; set; }

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
        public CustomerServiceReviewEntity() : base()
        {

        }

        #endregion
    }
}
