namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a department contact message
    /// </summary>
    public class DepartmentContactMessageRequestModel : BaseContactRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string? DepartmentId { get; set; }

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentContactMessageRequestModel() : base()
        {

        }

        #endregion
    }
}
