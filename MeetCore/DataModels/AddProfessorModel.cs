using MeetBase;

namespace MeetCore
{
    /// <summary>
    /// The model for adding a professor
    /// </summary>
    public class AddProfessorModel : AddStaffMemberModel
    {
        #region Public Properties

        /// <summary>
        /// The rank
        /// </summary>
        public ProfessorRank Rank { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddProfessorModel() : base()
        {

        }

        #endregion
    }
}
