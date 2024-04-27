namespace MeetBase
{
    /// <summary>
    /// Represents a department contact message template
    /// </summary>
    public class DepartmentContactMessageTemplate : AppointmentContactMessageTemplate
    {
        #region Public Properties

        /// <summary>
        /// The secretary role
        /// </summary>
        public SecretaryRole Role { get; set; }

        /// <summary>
        /// The mean of contact
        /// </summary>
        public ContactMean ContactMean { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentContactMessageTemplate() : base()
        {

        }

        #endregion
    }
}
