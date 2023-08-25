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
    /// Arguments used for retrieving data that belong to a department
    /// </summary>
    public class DepartmentRelatedAPIArgs : APIArgs
    {
        #region Public Properties

        /// <summary>
        /// Limit the result to entries with specific university ids
        /// </summary>
        public IEnumerable<string>? IncludeUniversities { get; set; }

        /// <summary>
        /// Limit the result to entries without specific university ids
        /// </summary>
        public IEnumerable<string>? ExcludeUniversities { get; set; }

        /// <summary>
        /// Limit the result to entries with specific department ids
        /// </summary>
        public IEnumerable<string>? IncludeDepartments { get; set; }

        /// <summary>
        /// Limit the result to entries without specific department ids
        /// </summary>
        public IEnumerable<string>? ExcludeDepartments { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentRelatedAPIArgs() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// Arguments used for retrieving members
    /// </summary>
    public class MemberRelatedAPIArgs : DepartmentRelatedAPIArgs
    {
        #region Public Properties

        /// <summary>
        /// Limit the result to entries with specific staff member ids
        /// </summary>
        public IEnumerable<string>? IncludeStaffMembers { get; set; }

        /// <summary>
        /// Limit the result to entries without specific staff member ids
        /// </summary>
        public IEnumerable<string>? ExcludeStaffMembers { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemberRelatedAPIArgs() : base()
        {

        }

        #endregion
    }

}
