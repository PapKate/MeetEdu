using MeetBase;

namespace MeetCore
{
    /// <summary>
    /// The model for adding a secretary
    /// </summary>
    public class AddSecretaryModel : AddStaffMemberModel
    {
        #region Public Properties

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddSecretaryModel() : base()
        {

        }

        #endregion
    }
}
