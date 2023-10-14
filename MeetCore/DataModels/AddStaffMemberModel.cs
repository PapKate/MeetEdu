using MeetBase;

namespace MeetCore
{
    /// <summary>
    /// The model for adding a staff member
    /// </summary>
    public abstract class AddStaffMemberModel
    {
        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string? Color { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddStaffMemberModel() : base()
        {

        }

        #endregion
    }
}
