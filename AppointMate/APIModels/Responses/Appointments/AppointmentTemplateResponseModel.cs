using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents an appointment template 
    /// </summary>
    public class AppointmentTemplateResponseModel : StandardResponseModel, IDescriptable, INoteable, IProfessorIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="ProfessorId"/> property
        /// </summary>
        private string? mProfessorId;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

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
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool HasRemoteOption { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentTemplateResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="AppointmentTemplateResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedAppointmentTemplateResponseModel : EmbeddedStandardResponseModel, IProfessorIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="ProfessorId"/> property
        /// </summary>
        private string? mProfessorId;

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
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool HasRemoteOption { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedAppointmentTemplateResponseModel() : base()
        {

        }

        #endregion
    }
}
