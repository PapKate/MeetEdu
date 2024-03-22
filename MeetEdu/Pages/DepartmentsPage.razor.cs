using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MeetEdu
{
    /// <summary>
    /// The departments search page
    /// </summary>
    public partial class DepartmentsPage
    {
        #region Private Members

        /// <summary>
        /// The departments
        /// </summary>
        private IEnumerable<DepartmentResponseModel>? mDepartments;

        #endregion

        #region Public Properties

        /// <summary>
        /// The search text
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string? SearchText { get; set; }

        /// <summary>
        /// The university id
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string? UniversityId { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetEduController Controller { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentsPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var args = new DepartmentAPIArgs();

            //if (!UniversityId.IsNullOrEmpty())
            //    args.IncludeUniversities = new List<string>() { UniversityId };

            var result = await Controller.GetDepartmentsAsync(args);

            if (result.Value.IsNullOrEmpty())
                return;

            mDepartments = result.Value;
        }

        #endregion
    }
}
