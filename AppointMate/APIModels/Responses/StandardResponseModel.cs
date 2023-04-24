namespace AppointMate
{
    /// <summary>
    /// The standard response model
    /// </summary>
    public class StandardResponseModel : DateResponseModel
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
    }
}
