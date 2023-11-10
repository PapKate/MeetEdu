using MeetBase;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a professor
    /// </summary>
    public class UpdateProfessorModel : UpdateStaffMemberModel
    {
        #region Public Properties

        /// <summary>
        /// The role
        /// </summary>
        public ProfessorRank Rank { get; set; }

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
