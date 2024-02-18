namespace MeetBase.Web
{
    /// <summary>
    /// Request model for an appointment template
    /// </summary>
    public class AppointmentRuleRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string? ProfessorId { get; set; }

        /// <summary>
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool? HasRemoteOption { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan? Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentRuleRequestModel() : base()
        {

        }

        #endregion
    }
}
