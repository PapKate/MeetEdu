namespace MeetBase.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorMessageResponseModel
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorMessageResponseModel() : base()
        {

        }

        #endregion
    }
}
