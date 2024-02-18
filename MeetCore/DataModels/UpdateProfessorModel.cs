using MeetBase;
using MeetBase.Web;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a professor
    /// </summary>
    public class UpdateProfessorModel : UpdateStaffMemberModel<ProfessorRequestModel>
    {
        #region Public Properties

        /// <summary>
        /// The role
        /// </summary>
        public ProfessorRank Rank { get; set; }

        /// <summary>
        /// The personal websites 
        /// </summary>
        public IEnumerable<Website>? Websites { get; set; }

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
        public UpdateProfessorModel() : base()
        {

        }

        #endregion
    }
}
