using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The contact form rules page
    /// </summary>
    public partial class Professor_ContactFormRulesPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private IEnumerable<AppointmentRuleResponseModel> mRules = new List<AppointmentRuleResponseModel>();

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel Professor => StateManager.Professor!;

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel Department => StateManager.Department!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The client
        /// </summary>
        [Inject]
        protected MeetCoreClient Client { get; set; } = default!;

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

        /// <summary>
        /// The dialog service
        /// </summary>
        [Inject]
        protected IDialogService DialogService { get; set; } = default!;

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
        public Professor_ContactFormRulesPage() : base()
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

            //var response = await Client.GetDepartmentContactMessagesAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            //// If there was an error...
            //if (!response.IsSuccessful)
            //{
            //    // Show the error
            //    Snackbar.Add(response.ErrorMessage, Severity.Info);
            //    // Return
            //    return;
            //}
            //mContactMessages = response.Result;

            mRules = new List<AppointmentRuleResponseModel>()
            {
                new()
                {
                    Name = "Name",
                    Color = "343899",
                    Description = "This is a very long long long description  very long long long description  very long long long description  very long long long description  very long long long description  very long long long description  very long long long description",
                    Duration = new TimeSpan(0, 35, 00)
                }
            };

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Edits the specified <paramref name="rule"/>
        /// </summary>
        /// <param name="rule">The rule</param>
        private async void EditRule(AppointmentRuleResponseModel rule)
        {
            var model = new AppointmentRuleRequestModel()
            {
                Color = rule.Color,
                Name = rule.Name,
                Note = rule.Note,
                Description = rule.Description,
                Duration = rule.Duration,
                HasRemoteOption = rule.HasRemoteOption,
                ProfessorId = Professor.Id
            };

            var parameters = new DialogParameters<AppointmentRuleDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<AppointmentRuleDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return
                return;
            }
        }

        #endregion
    }
}
