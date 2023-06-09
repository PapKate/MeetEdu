namespace AppointMate
{
    /// <summary>
    /// Request model used for a shipping
    /// </summary>
    public class ShippingRequestModel : LocationRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShippingRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{FirstName} {LastName} {Address}";

        #endregion
    }
}
