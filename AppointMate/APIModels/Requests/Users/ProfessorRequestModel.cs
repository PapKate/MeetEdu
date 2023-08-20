namespace AppointMate
{
    /// <summary>
    /// Request model used for a professor 
    /// </summary>
    public class ProfessorRequestModel : StaffMemberRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The personal websites 
        /// </summary>
        public IEnumerable<Uri>? Websites { get; set; }

        /// <summary>
        /// The field of study
        /// </summary>
        public string? Field { get; set; }

        /// <summary>
        /// The research interests
        /// </summary>
        public string? ResearchInterests { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorRequestModel() : base()
        {

        }

        #endregion
    }
}
