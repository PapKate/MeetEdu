namespace MeetBase.Blazor
{
    /// <summary>
    /// The department vector
    /// </summary>
    public partial class DepartmentVector : BaseVector
    {
        #region Constants

        /// <summary>
        /// The shadow color CSS variable
        /// </summary>
        public const string Shadow = "--shadow";

        /// <summary>
        /// The shadow dark color CSS variable
        /// </summary>
        public const string ShadowDark = "--shadowDark";

        /// <summary>
        /// The door dark color CSS variable
        /// </summary>
        public const string DoorDark = "--doorDark";

        /// <summary>
        /// The door color CSS variable
        /// </summary>
        public const string Door = "--door";

        #endregion

        #region Public Properties

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Name => "DepartmentVector";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentVector() : base()
        {

        }

        #endregion
    }
}
