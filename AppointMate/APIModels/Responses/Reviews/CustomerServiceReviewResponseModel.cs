using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer service review
    /// </summary>
    public class CustomerServiceReviewResponseModel : DateResponseModel, INoteable, IDepartmentIdentifiable<string>, ICustomerIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CustomerId"/> property
        /// </summary>
        private string? mCustomerId;

        /// <summary>
        /// The member of the <see cref="ServiceId"/> property
        /// </summary>
        private string? mServiceId;

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mCompanyId;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The customer id
        /// </summary>
        public string CustomerId
        {
            get => mCustomerId ?? string.Empty;
            set => mCustomerId = value;
        }

        /// <summary>
        /// The service id
        /// </summary>
        public string ServiceId
        {
            get => mServiceId ?? string.Empty;
            set => mServiceId = value;
        }

        /// <summary>
        /// The company id
        /// </summary>
        public string DepartmentId
        {
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

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
