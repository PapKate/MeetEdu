using Microsoft.AspNetCore.Components;
using MudBlazor;

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

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

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

        #region Private Methods

        /// <summary>
        /// Sends the message to the respective staff members
        /// </summary>
        /// <param name="message">The message</param>
        private async void DepartmentContactForm_SendOnClick(DepartmentContactMessageRequestModel message)
        {
            if (Department is null)
                return;

            var messageResponse = await Controller.AddDepartmentContactMessageAsync(Department.Id, message);

            if (messageResponse.Value is null)
            {
                // Shows the error
                Snackbar.Add("Error: Message not sent", Severity.Error);

                // Returns
                return;
            }

            Snackbar.Add("Success: Message sent!", Severity.Success);
        }

        #endregion
    }
}
