namespace MeetBase.Web
{
    /// <summary>
    /// Request model for an appointment 
    /// </summary>
    public class AppointmentRequestModel
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
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool? IsRemote { get; set; }

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
