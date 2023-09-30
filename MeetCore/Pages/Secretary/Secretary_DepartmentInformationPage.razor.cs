using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The department information page
    /// </summary>
    public partial class Secretary_DepartmentInformationPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The department
        /// </summary>
        private DepartmentResponseModel mDepartment => StateManager.Department!;

        /// <summary>
        /// The request
        /// </summary>
        private DepartmentRequestModel mRequest = new();

        /// <summary>
        /// The type of the department
        /// </summary>
        private DepartmentType mDepartmentType = DepartmentType.Science;

        /// <summary>
        /// The location
        /// </summary>
        private Location mLocation = new Location();

        /// <summary>
        /// A flag indicating whether the form is visible or not
        /// </summary>
        private bool mIsEditActive = true;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Secretary_DepartmentInformationPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            mLocation = mDepartment.Location ?? new();
        }

        #endregion
    }
}
