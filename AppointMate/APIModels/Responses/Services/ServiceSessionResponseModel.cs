namespace AppointMate
{
    /// <summary>
    /// The session response model
    /// </summary>
    public class ServiceSessionResponseModel : StandardResponseModel, IDescribable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// The date and time
        /// </summary>
        public DayOfWeekTimeRange DateAndTime { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceSessionResponseModel() : base()
        {

        }

        #endregion
    }
}
