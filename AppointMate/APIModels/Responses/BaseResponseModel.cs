namespace AppointMate
{
    /// <summary>
    /// The base response model
    /// </summary>
    public class BaseResponseModel
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="Id"/> property
        /// </summary>
        private string? mId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        public string Id 
        { 
            get => mId ?? string.Empty;
            set => mId = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseResponseModel() : base()
        {

        }

        #endregion
    }
}
