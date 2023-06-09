namespace AppointMate
{
    public class CustomerPointOffsetLogRequestModel 
    {
        #region Public Properties

        /// <summary>
        /// The creation date
        /// </summary>
        public DateTimeOffset? DateCreated { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerResponseModel? Customer { get; set; }

        /// <summary>
        /// The old points of the customer
        /// </summary>
        public uint? OldPoints { get; set; }

        /// <summary>
        /// The offset
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// A flag indicating whether the offset was positive or not
        /// </summary>
        public bool? IsPositive { get; set; }

        /// <summary>
        /// The new points of the customer
        /// </summary>
        public uint? NewPoints { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerPointOffsetLogRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Note: {Note}, Offset: {Offset}";

        #endregion
    }
}
