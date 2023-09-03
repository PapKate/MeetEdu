namespace MeetEdu
{
    /// <summary>
    /// A service with the related companies
    /// </summary>
    public class ServiceCompaniesResult : INameable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="Services"/> property
        /// </summary>
        private IEnumerable<AppointmentRuleEntity>? mServices;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Name
        /// </summary>
        public string Name
        {
            get => mName ?? string.Empty;
            set => mName = value;
        }

        /// <summary>
        /// The services
        /// </summary>
        public IEnumerable<AppointmentRuleEntity> Services
        {
            get => mServices ?? Enumerable.Empty<AppointmentRuleEntity>();
            set => mServices = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceCompaniesResult(string name, IEnumerable<AppointmentRuleEntity> models) : base()
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Services = models ?? throw new ArgumentNullException(nameof(Services));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// A string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

        #endregion
    }
}
