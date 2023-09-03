namespace MeetEdu
{
    /// <summary>
    /// The date entity
    /// </summary>
    public class DateEntity : BaseEntity
    {
        #region Public Properties

        /// <summary>
        /// The creation date
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// The last modification date
        /// </summary>
        public DateTimeOffset DateModified { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DateEntity() : base()
        {

        }

        #endregion
    }

}
