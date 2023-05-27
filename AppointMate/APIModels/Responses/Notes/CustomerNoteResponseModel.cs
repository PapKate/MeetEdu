namespace AppointMate
{
    public class CustomerNoteResponseModel : StandardResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Message"/> property
        /// </summary>
        private string? mMessage;

        /// <summary>
        /// The member of the <see cref="Labels"/> property
        /// </summary>
        private IEnumerable<EmbeddedLabelResponseModel>? mLabels;

        #endregion

        #region Public Properties

        /// <summary>
        /// The message
        /// </summary>
        public string Message 
        {
            get => mMessage ?? string.Empty;
            set => mMessage = value;
        }

        /// <summary>
        /// The type
        /// </summary>
        public CustomerNoteType Type { get; set; }

        /// <summary>
        /// A flag indicating whether this note can be seen by the customer or not
        /// </summary>
        public bool IsVisibleToCustomer { get; set; }

        /// <summary>
        /// A flag indicating whether the note automatically hides
        /// </summary>
        public bool IsHidingAutomatically { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerResponseModel? Customer { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<EmbeddedLabelResponseModel> Labels 
        { 
            get => mLabels ?? Enumerable.Empty<EmbeddedLabelResponseModel>();
            set => mLabels = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerNoteResponseModel() : base()
        {

        }

        #endregion
    }
}
