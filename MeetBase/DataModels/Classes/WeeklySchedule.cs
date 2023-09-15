using System.Xml.Linq;

namespace MeetBase
{
    /// <summary>
    /// The weekly schedule
    /// </summary>
    public class WeeklySchedule
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="WorkHours"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mWorkHours;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The work hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> WorkHours
        {
            get => mWorkHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mWorkHours = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeeklySchedule() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The lecture schedule
    /// </summary>
    public class Lecture : INameable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="LectureHours"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mLectureHours;

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get => mName ?? string.Empty;
            set => mName = value;
        }

        /// <summary>
        /// The color
        /// </summary>
        public string Color
        {
            get => mColor ?? string.Empty;
            set => mColor = value;
        }

        /// <summary>
        /// The lecture hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> LectureHours
        {
            get => mLectureHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mLectureHours = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Lecture() : base()
        {

        }

        #endregion
    }
}
