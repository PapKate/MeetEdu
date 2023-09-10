namespace MeetBase.Web
{
    /// <summary>
    /// The date response model
    /// </summary>
    public class DateResponseModel : BaseResponseModel
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
        public DateResponseModel() : base()
        {

        }

        #endregion
    }
}
