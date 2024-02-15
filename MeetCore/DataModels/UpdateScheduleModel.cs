using MeetBase;
using MeetBase.Web;

namespace MeetCore
{
    /// <summary>
    /// The model for updating a weekly schedule
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

    /// <summary>
    /// The model for viewing a department inbox message
    /// </summary>
    public class DepartmentMesageModel
    {
        #region Public Properties

        /// <summary>
        /// The staff member color
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The model
        /// </summary>
        public DepartmentContactMessageResponseModel? Model { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentMesageModel() : base()
        {

        }

        #endregion
    }
}
