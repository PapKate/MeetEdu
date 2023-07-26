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
}
