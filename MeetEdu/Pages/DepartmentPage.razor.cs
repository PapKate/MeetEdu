using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The department page
    /// </summary>
    public partial class DepartmentPage
    {
        #region Private Members

        /// <summary>
        /// The layouts
        /// </summary>
        private IEnumerable<LayoutResponseModel>? mLayouts;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel? Department { get; set; }

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
        public DepartmentPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Id is null)
                return;

            var result = await Controller.GetDepartmentAsync(Id);

            if (result?.Value is null)
                return;

            Department = result.Value;

            var layoutsResult = await Controller.GetDepartmentLayoutsAsync(new DepartmentRelatedAPIArgs() { IncludeDepartments = new List<string>() { Department.Id } });

            mLayouts = layoutsResult.Value;
        }

        #endregion
    }
}
