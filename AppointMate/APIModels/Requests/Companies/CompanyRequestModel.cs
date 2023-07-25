namespace AppointMate
{
    /// <summary>
    /// Request model used for a company
    /// </summary>
    public class CompanyRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The category
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The radius for the distance where at home services can be provided in Km
        /// </summary>
        public double? AtHomeRadius { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyRequestModel() : base()
        {

        }

        #endregion
    }
}
