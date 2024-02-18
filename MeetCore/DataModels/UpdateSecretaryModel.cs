using MeetBase;
using MeetBase.Web;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a secretary
    /// </summary>
    public class UpdateSecretaryModel : UpdateStaffMemberModel<SecretaryRequestModel>
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
        public UpdateSecretaryModel() : base()
        {

        }

        #endregion
    }
}
