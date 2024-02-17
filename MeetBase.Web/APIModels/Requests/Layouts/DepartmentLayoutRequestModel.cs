namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a department layout request model
    /// </summary>
    public class DepartmentLayoutRequestModel : LayoutRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string? DepartmentId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentLayoutRequestModel() : base()
        {

        }

        #endregion
    }
}
