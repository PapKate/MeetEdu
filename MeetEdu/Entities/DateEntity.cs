namespace MeetEdu
{
    /// <summary>
    /// The date entity
    /// </summary>
    public class DateEntity : BaseEntity, IDateCreatable, IDateModifiable
    {
        #region Public Properties

        /// <summary>
        /// The creation date
        /// </summary>
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// The last modification date
        /// </summary>
        public DateTimeOffset DateModified { get; set; } = DateTimeOffset.Now;

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
