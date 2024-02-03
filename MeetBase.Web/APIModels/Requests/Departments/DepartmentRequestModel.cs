namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a department
    /// </summary>
    public class DepartmentRequestModel : StandardRequestModel, IImageable
    {
        #region Public Properties

        /// <summary>
        /// The university id
        /// </summary>
        public string? UniversityId { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

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
        /// The layout description
        /// </summary>
        public string? LayoutDescription { get; set; }

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
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The contact message template
        /// </summary>
        public DepartmentContactMessageTemplate? ContactMessageTemplate { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<string>? LabelIds { get; set; }

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
