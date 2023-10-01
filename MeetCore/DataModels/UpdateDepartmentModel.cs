using MeetBase;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a department
    /// </summary>
    public class UpdateDepartmentModel
    {
        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The category
        /// </summary>
        public DepartmentType Category { get; set; }

        /// <summary>
        /// The fields of study
        /// </summary>
        public IEnumerable<string>? Fields { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        public WeeklySchedule? WorkHours { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The labels
        /// </summary>
        public IEnumerable<string>? LabelIds { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateDepartmentModel() : base()
        {

        }

        #endregion
    }
}
