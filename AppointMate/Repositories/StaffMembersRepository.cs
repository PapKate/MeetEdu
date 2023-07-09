namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing staff members
    /// </summary>
    public class StaffMembersRepository
    {
        #region Private Members

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly AppointMateUserManager? mUserManager;

        #endregion

        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="StaffMembersRepository"/>
        /// </summary>
        public static StaffMembersRepository Instance { get; } = new StaffMembersRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StaffMembersRepository() : base()
        {

        }

        #endregion

        #region Public Methods



        #endregion
    }
}
