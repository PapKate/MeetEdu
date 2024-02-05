using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The department layout page
    /// </summary>
    public partial class Secretary_DepartmentLayoutPage : BasePage
    {
        #region Private Members

        private string mText = string.Empty;

        private List<DepartmentLayoutResponseModel>? mLayouts;

        private readonly List<LayoutPresenter> mLayoutPresenters = new();

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private readonly DialogOptions mDialogOptions = new() { FullWidth = true };

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

        /// <summary>
        /// The layout presenter instance
        /// </summary>
        protected LayoutPresenter LayoutPresenter
        {
            set
            {
                mLayoutPresenters.Add(value);
            }
        }

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
        private async Task AddLayout()
        {
            // Creates the request for adding the layout
            var request = new DepartmentLayoutRequestModel()
            {
                DepartmentId = StateManager.Department!.Id,
                Color = MeetBase.Blazor.PaletteColors.Gray
            };

            var response = await SetDepartmentLayoutImageAsync(request, Client.AddDepartmentLayoutAsync);
            GetLayouts();
        }

        /// <summary>
        /// Updates the current <paramref name="model"/>
        /// </summary>
        /// <param name="model">The layout</param>
        private async Task UpdateLayout(DepartmentLayoutResponseModel model)
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

            var response = await SetDepartmentLayoutImageAsync(request, (request) => Client.UpdateDepartmentLayoutAsync(model.Id, request));
            if (response is not null)
            {
                var layout = mLayouts!.First(x => x.Id == response.Id);
                var index = mLayouts!.IndexOf(layout);
                mLayouts?.Remove(layout);
                mLayouts?.Insert(index, response);
                mLayoutPresenters.First(x => x.Layout!.Id == response.Id).ReplaceLayout(response);
            }
        }

        /// <summary>
        /// Deletes the current <paramref name="model"/>
        /// </summary>
        /// <param name="model">The layout</param>
        private async Task DeleteLayout(DepartmentLayoutResponseModel model)
        {
            await Client.DeleteDepartmentLayoutAsync(model.Id);
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
            mLayouts = response.Result.ToList();

            StateHasChanged();
        }

        private async Task<DepartmentLayoutResponseModel?> SetDepartmentLayoutImageAsync(DepartmentLayoutRequestModel request, 
                                                         Func<DepartmentLayoutRequestModel, Task<WebRequestResult<DepartmentLayoutResponseModel>>> requestAction)
        {
            var parameters = new DialogParameters<UpdateLayoutDialog> { { x => x.Model, new UpdateModel<DepartmentLayoutRequestModel>(request) } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateLayoutDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return
                return null;
            }

            // If the result is of the specified type...
            if (result.Data is UpdateModel<DepartmentLayoutRequestModel> updatedModel)
            {
                // Performs the specified action
                var response = await requestAction(updatedModel.Model);

                // If there was an error...
                if (!response.IsSuccessful)
                {
                    Console.WriteLine(response.ErrorMessage);
                    // Show the error
                    Snackbar.Add(response.ErrorMessage, Severity.Error);
                    // Return
                    return null;
                }

                // If an image was set...
                if (updatedModel.File is not null)
                {
                    // Adds the model
                    var responseWithImage = await Client.SetDepartmentLayoutImageAsync(response.Result.Id, updatedModel.File);

                    // If there was an error...
                    if (!responseWithImage.IsSuccessful)
                    {
                        Console.WriteLine(responseWithImage.ErrorMessage);
                        // Show the error
                        Snackbar.Add(responseWithImage.ErrorMessage, Severity.Error);
                        // Return
                        return null;
                    }

                    return responseWithImage.Result;
                }
                return response.Result;
            }
            return null;
        }

        #endregion
    }
}
