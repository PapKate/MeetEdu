namespace MeetCore
{
    /// <summary>
    /// The side menu component
    /// </summary>
    public partial class SideMenu
    {
        #region Private Members

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating whether the staff member is a secretary
        /// </summary>
        public bool IsSecretary { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenu() : base()
        {

        }

        #endregion
    }
}
