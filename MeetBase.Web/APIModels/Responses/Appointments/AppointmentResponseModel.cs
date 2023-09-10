namespace MeetBase.Web
{
    /// <summary>
    /// Represents an appointment 
    /// </summary>
    public class AppointmentResponseModel : DepartmentContactMessageResponseModel, IProfessorIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="ProfessorId"/> property
        /// </summary>
        private string? mProfessorId;

        /// <summary>
        /// The member of the <see cref="RuleId"/> property
        /// </summary>
        private string? mRuleId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string ProfessorId
        {
            get => mProfessorId ?? string.Empty;
            set => mProfessorId = value;
        }

        /// <summary>
        /// The appointment rule id
        /// </summary>
        public string RuleId
        {
            get => mRuleId ?? string.Empty;
            set => mRuleId = value;
        }

        /// <summary>
        /// The date
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// The time that the appointment begins
        /// </summary>
        public TimeOnly TimeStart { get; set; }

        /// <summary>
        /// The time that the appointment ends
        /// </summary>
        public TimeOnly TimeEnd { get; set; }

        /// <summary>
        /// A flag indicating whether the subscription was canceled or not
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// The date the customer subscription was canceled
        /// </summary>
        public DateTimeOffset? DateCanceled { get; set; }

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// The rule
        /// </summary>
        public EmbeddedAppointmentRuleResponseModel? Rule { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentResponseModel() : base()
        {

        }

        #endregion
    }


    /// <summary>
    /// A minimal version of the <see cref="AppointmentResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedAppointmentResponseModel : EmbeddedDepartmentContactMessageResponseModel, IProfessorIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="ProfessorId"/> property
        /// </summary>
        private string? mProfessorId;

        /// <summary>
        /// The member of the <see cref="RuleId"/> property
        /// </summary>
        private string? mRuleId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string ProfessorId
        {
            get => mProfessorId ?? string.Empty;
            set => mProfessorId = value;
        }

        /// <summary>
        /// The appointment rule id
        /// </summary>
        public string RuleId
        {
            get => mRuleId ?? string.Empty;
            set => mRuleId = value;
        }

        /// <summary>
        /// The date
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// The time that the appointment begins
        /// </summary>
        public TimeOnly TimeStart { get; set; }

        /// <summary>
        /// The time that the appointment ends
        /// </summary>
        public TimeOnly TimeEnd { get; set; }

        /// <summary>
        /// A flag indicating whether the subscription was canceled or not
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// The subject
        /// </summary>
        public EmbeddedAppointmentRuleResponseModel? Rule { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedAppointmentResponseModel() : base()
        {

        }

        #endregion
    }
}
