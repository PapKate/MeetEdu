namespace AppointMate
{
    /// <summary>
    /// The company response model
    /// </summary>
    public class CompanyResponseModel : StandardResponseModel, IImageable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Category"/> property
        /// </summary>
        private string? mCategory;

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="WorkHours"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mWorkHours;

        #endregion

        #region Public Properties

        /// <summary>
        /// The category
        /// </summary>
        public string Category
        {
            get => mCategory ?? string.Empty;
            set => mCategory = value;
        }

        /// <summary>
        /// The description
        /// </summary>
        public string Description 
        { 
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The work hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> WorkHours 
        { 
            get => mWorkHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mWorkHours = value;
        }

        /// <summary>
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        // Embedded Owner

        // Embedded Staff Member

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompanyResponseModel() : base()
        {

        }

        #endregion
    }
}
