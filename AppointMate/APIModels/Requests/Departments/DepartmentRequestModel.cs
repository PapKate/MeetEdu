namespace AppointMate
{
    /// <summary>
    /// Request model for a department
    /// </summary>
    public class DepartmentRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The category
        /// </summary>
        public DepartmentType? Category { get; set; }

        /// <summary>
        /// The fields of study
        /// </summary>
        public IEnumerable<string>? Fields { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The number of staff members
        /// </summary>
        public uint? TotalStaffMembers { get; set; }

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
        public IEnumerable<EmbeddedLabelResponseModel>? Labels { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentRequestModel() : base()
        {

        }

        #endregion
    }
}
