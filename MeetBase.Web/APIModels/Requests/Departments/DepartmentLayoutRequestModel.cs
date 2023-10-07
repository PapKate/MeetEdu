namespace MeetBase.Web
{
    /// <summary>
    /// Request model for a department layout
    /// </summary>
    public class DepartmentLayoutRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string? DepartmentId { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The layout
        /// </summary>
        public IList<DepartmentLayoutRoom>? Rooms { get; set; }

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
