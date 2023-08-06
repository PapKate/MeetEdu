namespace AppointMate
{
    /// <summary>
    /// Query arguments that provide pagination
    /// </summary>
    public class APIArgs : BaseArgs, IOffsetable, IPaginatable
    {
        #region Private Methods

        /// <summary>
        /// The member of the <see cref="Offset"/> property
        /// </summary>
        private int mOffset = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// The index of the page starting from 0.
        /// </summary>
        public virtual int Page { get; set; } = 0;

        /// <summary>
        /// Maximum number of entries to be returned in result set.
        /// </summary>
        public virtual int PerPage { get; set; } = 10;

        /// <summary>
        /// Offset the result set by a specific number of items.
        /// </summary>
        public virtual int Offset
        {
            get => mOffset;

            set
            {
                if (value < 0)
                    mOffset = value;

                mOffset = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public APIArgs() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// Arguments used for retrieving data that belong to a company
    /// </summary>
    public class StandardAPIArgs : APIArgs
    {
        #region Public Properties

        /// <summary>
        /// Limit the result to entries with specific company ids
        /// </summary>
        public IEnumerable<string>? IncludeCompanies { get; set; }

        /// <summary>
        /// Limit the result to entries without specific company ids
        /// </summary>
        public IEnumerable<string>? ExcludeCompanies { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StandardAPIArgs() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// Arguments used for retrieving grouped services
    /// </summary>
    public class GroupedServiceAPIArgs : StandardAPIArgs
    {
        #region Private Methods

        /// <summary>
        /// The member of the <see cref="InnerOffset"/> property
        /// </summary>
        private int mInnerOffset = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// The index of the page starting from 0.
        /// </summary>
        public virtual int InnerPage { get; set; } = 0;

        /// <summary>
        /// Maximum number of entries to be returned in result set.
        /// </summary>
        public virtual int InnerPerPage { get; set; } = 3;

        /// <summary>
        /// Offset the result set by a specific number of items.
        /// </summary>
        public virtual int InnerOffset
        {
            get => mInnerOffset;

            set
            {
                if (value < 0)
                    mInnerOffset = value;

                mInnerOffset = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GroupedServiceAPIArgs() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// Arguments used for retrieving services
    /// </summary>
    public class CustomerServiceAPIArgs : StandardAPIArgs
    {
        #region Public Properties

        /// <summary>
        /// Limit the result to entries with specific customer ids
        /// </summary>
        public IEnumerable<string>? IncludeCustomers { get; set; }

        /// <summary>
        /// Limit the result to entries without specific customer ids
        /// </summary>
        public IEnumerable<string>? ExcludeCustomers { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServiceAPIArgs() : base()
        {

        }

        #endregion
    }
}
