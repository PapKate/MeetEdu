namespace AppointMate
{
    /// <summary>
    /// Request model used for a staff member
    /// </summary>
    public class StaffMemberRequestModel : UserRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The quote
        /// </summary>
        public string? Quote { get; set; }

        /// <summary>
        /// A flag indicating whether they are an owner
        /// </summary>
        public bool? IsOwner { get; set; }

        /// <summary>
        /// The roles
        /// </summary>
        public IEnumerable<string>? Roles { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklyScheduleResponseModel? WorkHours { get; set; }

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
