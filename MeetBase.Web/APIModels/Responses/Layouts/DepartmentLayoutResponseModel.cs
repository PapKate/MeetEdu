namespace MeetBase.Web
{
    /// <summary>
    /// Represents a department layout response model
    /// </summary>
    public class DepartmentLayoutResponseModel : LayoutResponseModel, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mDepartmentId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentLayoutResponseModel() : base()
        {

        }

        #endregion
    }
}
