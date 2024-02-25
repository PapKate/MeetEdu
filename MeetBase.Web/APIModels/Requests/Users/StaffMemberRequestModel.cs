namespace MeetBase.Web
{
    /// <summary>
    /// Request model used for a staff member
    /// </summary>
    public abstract class StaffMemberRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// The department id
        /// </summary>
        public string? DepartmentId { get; set; }

        /// <summary>
        /// The quote
        /// </summary>
        public string? Quote { get; set; }

        /// <summary>
        /// The weekly schedule
        /// </summary>
        public WeeklySchedule? WeeklySchedule { get; set; }

        /// <summary>
        /// The appointment template
        /// </summary>
        public AppointmentContactMessageTemplate? ContactMessageTemplate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StaffMemberRequestModel() : base()
        {

        }

        #endregion
    }
}
