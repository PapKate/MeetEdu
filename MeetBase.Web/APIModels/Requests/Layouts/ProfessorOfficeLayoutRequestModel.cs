namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a professor office layout
    /// </summary>
    public class ProfessorOfficeLayoutRequestModel : LayoutRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The staff member id
        /// </summary>
        public string? ProfessorId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorOfficeLayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
