using MeetBase;

using System.Xml.Linq;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a secretary
    /// </summary>
    public class UpdateSecretaryModel
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
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The birthday
        /// </summary>
        public DateOnly? DateOfBirth { get; set; }

        /// <summary>
        /// The location information
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        /// <summary>
        /// The quote
        /// </summary>
        public string? Quote { get; set; }

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

    /// <summary>
    /// The model for updating a staff member weekly schedule
    /// </summary>
    public class UpdateScheduleModel
    {
        #region Public Properties

        /// <summary>
        /// The staff member color
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The weekly schedule
        /// </summary>
        public WeeklySchedule? WeeklySchedule { get; set; }

        /// <summary>
        /// The lectures
        /// </summary>
        public IEnumerable<Lecture>? Lectures { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateScheduleModel() : base()
        {

        }

        #endregion
    }
}
