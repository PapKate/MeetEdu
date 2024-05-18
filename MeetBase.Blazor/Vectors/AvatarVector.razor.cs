namespace MeetBase.Blazor
{
    /// <summary>
    /// The avatar vector
    /// </summary>
    public partial class AvatarVector : BaseVector
    {
        #region Constants

        /// <summary>
        /// The hoodie light color CSS variable
        /// </summary>
        public const string HoodieLight = "--hoodieLight";

        /// <summary>
        /// The hoodie dark color CSS variable
        /// </summary>
        public const string HoodieDark = "--hoodieDark";

        /// <summary>
        /// The hoodie shadow color CSS variable
        /// </summary>
        public const string HoodieShadow = "--hoodieShadow";

        #endregion

        #region Public Properties

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Name => "AvatarVector";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AvatarVector() : base()
        {

        }

        #endregion
    }
}
