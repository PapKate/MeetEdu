namespace AppointMate
{
    /// <summary>
    /// The standard response model
    /// </summary>
    public class StandardRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string? Color { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StandardRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name ?? string.Empty;

        #endregion
    }

    /// <summary>
    /// The embedded standard response model
    /// </summary>
    public class EmbeddedStandardRequestModel : BaseEmbeddedRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        public string? Color { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedStandardRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name ?? string.Empty;

        #endregion
    }

}
