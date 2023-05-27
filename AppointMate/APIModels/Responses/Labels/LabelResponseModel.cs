namespace AppointMate
{
    /// <summary>
    /// Represents a label
    /// </summary>
    public class LabelResponseModel : StandardResponseModel, IDescriptable
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
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelResponseModel()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="LabelResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedLabelResponseModel : EmbeddedStandardResponseModel, INameable, IColorable
    {
        #region Public Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedLabelResponseModel() : base()
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
