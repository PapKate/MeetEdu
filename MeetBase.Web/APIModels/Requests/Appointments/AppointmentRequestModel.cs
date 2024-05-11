namespace MeetBase.Web
{
    /// <summary>
    /// Request model for an appointment 
    /// </summary>
    public class AppointmentRequestModel : BaseContactRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string? ProfessorId { get; set; }

        /// <summary>
        /// The appointment rule id
        /// </summary>
        public string? RuleId { get; set; }

        /// <summary>
        /// The date
        /// </summary>
        public DateTimeOffset DateStart { get; set; }

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool? IsRemote { get; set; }

        /// <summary>
        /// The calendar event
        /// </summary>
        public string? CalendarEvent { get; set; }

        /// <summary>
        /// The link of the meeting
        /// </summary>
        /// <remarks>ex.: Google Meet, Teams Meeting etc.</remarks>
        public Uri? MeetLink { get; set; }

        /// <summary>
        /// THe status
        /// </summary>
        public AppointmentStatus Status { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentRequestModel() : base()
        {

        }

        #endregion
    }
}
