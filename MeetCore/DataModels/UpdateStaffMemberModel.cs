using MeetBase;
using MeetBase.Web;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a staff member
    /// </summary>
    public class UpdateStaffMemberModel<TStaffMember> : UpdateModel<UserRequestModel>
        where TStaffMember : StaffMemberRequestModel, new()
    {
        #region Public Properties

        /// <summary>
        /// The staff member
        /// </summary>
        public TStaffMember? StaffMember { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public UpdateStaffMemberModel() : base()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateStaffMemberModel(UserRequestModel model, TStaffMember staffMember) : base(model)
        {
            StaffMember = staffMember;
        }

        #endregion
    }
}
