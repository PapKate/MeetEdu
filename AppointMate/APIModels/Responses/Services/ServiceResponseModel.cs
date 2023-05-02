namespace AppointMate
{
    /// <summary>
    /// The service response model
    /// </summary>
    public class ServiceResponseModel : StandardResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="SmallDescription"/> property
        /// </summary>
        private string? mSmallDescription;

        /// <summary>
        /// The member of the <see cref="SessionsNote"/> property
        /// </summary>
        private string? mSessionsNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The description
        /// </summary>
        public string Description 
        { 
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The small description
        /// </summary>
        public string SmallDescription
        {
            get => mSmallDescription ?? string.Empty;
            set => mSmallDescription = value;
        }

        /// <summary>
        /// The price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The price on sale
        /// </summary>
        public double PriceOnSale { get; set; }

        /// <summary>
        /// The sessions note
        /// </summary>
        public string SessionsNote 
        { 
            get => mSessionsNote ?? string.Empty;
            set => mSessionsNote = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceResponseModel() : base()
        {

        }

        #endregion
    }
}
