using System.Security.Principal;

namespace AppointMate
{
    /// <summary>
    /// The standard response model
    /// </summary>
    public class StandardResponseModel : DateResponseModel, INameable, IColorable
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get => mName ?? string.Empty;
            set => mName = value;
        }

        /// <summary>
        /// The color
        /// </summary>
        public string Color
        {
            get => mColor ?? string.Empty;
            set => mColor = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StandardResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

        #endregion
    }

    /// <summary>
    /// The embedded standard response model
    /// </summary>
    public class EmbeddedStandardResponseModel : IEmbeddedIdentifiable, INameable, IColorable
    {
        #region Private Properties

        /// <summary>
        /// The member of the <see cref="Id"/> property
        /// </summary>
        private string? mId;

        /// <summary>
        /// The member of the <see cref="Source"/> property
        /// </summary>
        private string? mSource;

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

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

        /// <summary>
        /// The id of the entity that was used for creating the current 
        /// </summary>
        public string Source
        {
            get => mSource ?? string.Empty;
            set => mSource = value;
        }

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get => mName ?? string.Empty;
            set => mName = value;
        }

        /// <summary>
        /// The color
        /// </summary>
        public string Color
        {
            get => mColor ?? string.Empty;
            set => mColor = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedStandardResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

        #endregion
    }

}
