namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a professor contact message
    /// </summary>
    public class ProfessorContactMessageRequestModel : BaseContactRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string? ProfessorId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorContactMessageRequestModel() : base()
        {

        }

        #endregion
    }

}
