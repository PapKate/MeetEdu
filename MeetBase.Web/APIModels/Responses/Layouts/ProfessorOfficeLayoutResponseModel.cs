namespace MeetBase.Web
{
    /// <summary>
    /// Represents a department layout response model
    /// </summary>
    public class ProfessorOfficeLayoutResponseModel : LayoutResponseModel, IProfessorIdentifiable<string>
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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorOfficeLayoutResponseModel() : base()
        {

        }

        #endregion
    }
}
