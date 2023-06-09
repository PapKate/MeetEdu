namespace AppointMate
{
    /// <summary>
    /// Represents a label
    /// </summary>
    public class LabelRequestModel : StandardRequestModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        #endregion

        #region Public Properties

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelRequestModel()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="LabelResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedLabelRequestModel : EmbeddedStandardRequestModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedLabelRequestModel() : base()
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
