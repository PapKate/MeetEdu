using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MeetCore.UpdateLayoutDialog;

namespace MeetCore
{
    /// <summary>
    /// The department layout page
    /// </summary>
    public partial class Secretary_DepartmentLayoutPage : BasePage
    {
        #region Private Members

        private string mText = string.Empty;

        private IEnumerable<DepartmentLayoutResponseModel>? mLayouts;

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

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
        public Secretary_DepartmentLayoutPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            GetLayouts();
            mText = StateManager.Department!.LayoutDescription;
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the layout description of the current <see cref="StateManagerCore.Department"/>
        /// </summary>
        private async void SaveFormDescription(string? value)
        {
            mText = value ?? string.Empty;

            // Updates the department
            var response = await Client.UpdateDepartmentAsync(StateManager.Department!.Id, new DepartmentRequestModel() { LayoutDescription = mText });
            // If there was an error...
            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }
            StateManager.Department = response.Result;
        }

        /// <summary>
        /// Creates and adds a layout to the current department
        /// </summary>
        private async void AddLayout()
        {
            // Creates the request for adding the layout
            var request = new DepartmentLayoutRequestModel()
            {
                DepartmentId = StateManager.Department!.Id,
                Color = MeetBase.Blazor.PaletteColors.White
            };

            var parameters = new DialogParameters<UpdateLayoutDialog> { { x => x.Model, new UpdateLayoutModel(request) } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateLayoutDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return
                return;
            }

            // If the result is of the specified type...
            if (result.Data is UpdateLayoutModel updatedModel)
            {
                // Adds the model
                var response = await Client.AddDepartmentLayoutAsync(updatedModel.Model);
                
                // If there was an error...
                if (!response.IsSuccessful)
                {
                    Console.WriteLine(response.ErrorMessage);
                    // Show the error
                    Snackbar.Add(response.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }
                
                // If an image was set..
                if(updatedModel.File is not null)
                {
                    // Adds the model
                    var imageResponse = await Client.SetDepartmentLayoutImageAsync(response.Result.Id, updatedModel.File);

                    // If there was an error...
                    if (!imageResponse.IsSuccessful)
                    {
                        Console.WriteLine(imageResponse.ErrorMessage);
                        // Show the error
                        Snackbar.Add(imageResponse.ErrorMessage, Severity.Error);
                        // Return
                        return;
                    }
                }
            }
            GetLayouts();
        }

        /// <summary>
        /// Updates the current <paramref name="model"/>
        /// </summary>
        /// <param name="model">The layout</param>
        private async void UpdateLayout(DepartmentLayoutResponseModel model)
        {
            var request = new DepartmentLayoutRequestModel()
            {
                DepartmentId = model.DepartmentId,
                Name = model.Name,
                Color = model.Color,
                Description = model.Description,
                Note = model.Note,
                DisplayTheme = model.DisplayTheme,
            };

            var parameters = new DialogParameters<UpdateLayoutDialog> { { x => x.Model, new UpdateLayoutModel(request) } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateLayoutDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return
                return;
            }

            // If the result is of the specified type...
            if (result.Data is UpdateLayoutModel updatedModel)
            {
                // Updates the layout
                var response = await Client.UpdateDepartmentLayoutAsync(model.Id, updatedModel.Model);

                // If there was an error...
                if (!response.IsSuccessful)
                {
                    Console.WriteLine(response.ErrorMessage);
                    // Show the error
                    Snackbar.Add(response.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                // If an image was set..
                if (updatedModel.File is not null)
                {
                    // Adds the model
                    var imageResponse = await Client.SetDepartmentLayoutImageAsync(response.Result.Id, updatedModel.File);

                    // If there was an error...
                    if (!imageResponse.IsSuccessful)
                    {
                        Console.WriteLine(imageResponse.ErrorMessage);
                        // Show the error
                        Snackbar.Add(imageResponse.ErrorMessage, Severity.Error);
                        // Return
                        return;
                    }
                }
            }
            
            GetLayouts();
        }

        /// <summary>
        /// Gets the <see cref="DepartmentLayoutResponseModel"/>s of the current <see cref="StateManagerCore.Department"/>
        /// </summary>
        private async void GetLayouts() 
        {
            var response = await Client.GetDepartmentLayoutsAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Info);
                return;
            }

            if (!response.Result.Any())
            {
                Snackbar.Add("No layouts", Severity.Error);
                return;
            }
            mLayouts = response.Result;
            StateHasChanged();
        }

        #endregion
    }
}
