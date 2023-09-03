namespace MeetEdu
{
    /// <summary>
    /// The base response model
    /// </summary>
    public abstract class BaseResponseModel : IIdentifiable<string>
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

    /// <summary>
    /// The base for all the embedded response models
    /// </summary>
    public abstract class EmbeddedBaseResponseModel : BaseResponseModel, IEmbeddableIdentifiable<string>
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="Source"/> property
        /// </summary>
        private string? mSource;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id of the entity that was used for creating the current 
        /// </summary>
        public string Source
        {
            get => mSource ?? string.Empty;
            set => mSource = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedBaseResponseModel() : base()
        {

        }

        #endregion
    }
}
