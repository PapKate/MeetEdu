namespace MeetBase
{
    /// <summary>
    /// Represents the contact form massage template for an appointment of a staff member
    /// </summary>
    public class AppointmentContactMessageTemplate
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="VectorName"/> property
        /// </summary>
        private string? mVectorName;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the vector component
        /// </summary>
        public string VectorName
        {
            get => mVectorName ?? string.Empty;
            set => mVectorName = value;
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
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentContactMessageTemplate() : base()
        {

        }

        #endregion
    }
}
