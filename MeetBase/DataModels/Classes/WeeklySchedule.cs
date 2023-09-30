using System.Drawing;
using System.Xml.Linq;

namespace MeetBase
{
    /// <summary>
    /// The weekly schedule
    /// </summary>
    public class WeeklySchedule : INameable, IColorable, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="WeeklyHours"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mWeeklyHours;

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

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
        /// The weekly schedule hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> WeeklyHours
        {
            get => mWeeklyHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mWeeklyHours = value;
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
    public class Lecture : WeeklySchedule
    {
        #region Public Properties

        /// <summary>
        /// A flag indicating whether it is remote or not
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// The link to the remote lecture
        /// </summary>
        public string? Link { get; set; }

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
