namespace AppointMate
{
    /// <summary>
    /// The base response model
    /// </summary>
    public class BaseRequestModel 
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseRequestModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The base for all the embedded response models
    /// </summary>
    public class BaseEmbeddedRequestModel : BaseRequestModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEmbeddedRequestModel() : base()
        {

        }

        #endregion
    }
}
