using System.Drawing;
using System.Globalization;
using System.Xml.Linq;

namespace MeetBase
{
    /// <summary>
    /// The weekly schedule
    /// </summary>
    public class WeeklySchedule : Lecture, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

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
    public class Lecture : INameable, IColorable
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
            set => mColor = value.ToHex();
        }

        /// <summary>
        /// The weekly schedule hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> WeeklyHours
        {
            get => mWeeklyHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mWeeklyHours = value;
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

    /// <summary>
    /// The website
    /// </summary>
    public class Website : INameable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

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
        /// The link
        /// </summary>
        public Uri? Link { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Website() : base()
        {

        }

        #endregion
    }
}
