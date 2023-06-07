namespace AppointMate
{
    /// <summary>
    /// Represents a label
    /// </summary>
    public class LabelEntity : StandardEntity, IDescriptable
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
        public LabelEntity()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="LabelEntity"/> used when embedding
    /// </summary>
    public class EmbeddedLabelEntity : EmbeddedStandardEntity
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedLabelEntity() : base()
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
