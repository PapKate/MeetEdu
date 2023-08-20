namespace AppointMate
{
    /// <summary>
    /// Request model used for a company
    /// </summary>
    public class CompanyRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The categories
        /// </summary>
        public IEnumerable<DepartmentType>? Categories { get; set; }

        /// <summary>
        /// A flag indicating whether the company provides at home services
        /// </summary>
        public bool? HasAtHomeServices { get; set; } 

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
        /// The work hours
        /// </summary>
        public WeeklySchedule? WorkHours { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public Billing? Billing { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public Shipping? Shipping { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<string>? Labels { get; set; }

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
