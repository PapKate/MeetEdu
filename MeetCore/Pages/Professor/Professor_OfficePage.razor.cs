using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The office page
    /// </summary>
    public partial class Professor_OfficePage : BasePage
    {
        #region Private Members

        private List<ProfessorOfficeLayoutResponseModel>? mLayouts;

        private readonly List<LayoutPresenter<ProfessorOfficeLayoutResponseModel>> mLayoutPresenters = new();

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
        protected LayoutPresenter<ProfessorOfficeLayoutResponseModel> LayoutPresenter
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
        public Professor_OfficePage() : base()
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
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and adds a layout to the current department
        /// </summary>
        private async Task AddLayout()
        {
            // Creates the request for adding the layout
            var request = new ProfessorOfficeLayoutRequestModel()
            {
                ProfessorId = StateManager.Professor!.Id,
                Color = MeetBase.Blazor.PaletteColors.Gray
            };

            var response = await SetProfessorOfficeLayoutImageAsync(request, Client.AddProfessorOfficeLayoutAsync);
            GetLayouts();
        }

        /// <summary>
        /// Updates the current <paramref name="model"/>
        /// </summary>
        /// <param name="model">The layout</param>
        private async Task UpdateLayout(ProfessorOfficeLayoutResponseModel model)
        {
            var request = new ProfessorOfficeLayoutRequestModel()
            {
                ProfessorId = model.ProfessorId,
                Name = model.Name,
                Color = model.Color,
                Description = model.Description,
                Note = model.Note,
                DisplayTheme = model.DisplayTheme,
            };

            var response = await SetProfessorOfficeLayoutImageAsync(request, (request) => Client.UpdateProfessorOfficeLayoutAsync(model.Id, request));
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
        private async Task DeleteLayout(ProfessorOfficeLayoutResponseModel model)
        {
            await Client.DeleteProfessorOfficeLayoutAsync(model.Id);
            GetLayouts();
        }

        /// <summary>
        /// Gets the <see cref="ProfessorOfficeLayoutResponseModel"/>s of the current <see cref="StateManagerCore.Professor"/>
        /// </summary>
        private async void GetLayouts()
        {
            var response = await Client.GetProfessorOfficeLayoutsAsync(new() { IncludeProfessors = new List<string>() { StateManager.Professor!.Id } });

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

        private async Task<ProfessorOfficeLayoutResponseModel?> SetProfessorOfficeLayoutImageAsync(ProfessorOfficeLayoutRequestModel request,
                                                         Func<ProfessorOfficeLayoutRequestModel, Task<WebRequestResult<ProfessorOfficeLayoutResponseModel>>> requestAction)
        {
            var parameters = new DialogParameters<UpdateLayoutDialog<ProfessorOfficeLayoutRequestModel>> { { x => x.Model, new UpdateModel<ProfessorOfficeLayoutRequestModel>(request) } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateLayoutDialog<ProfessorOfficeLayoutRequestModel>>(null, parameters, mDialogOptions);

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
            if (result.Data is UpdateModel<ProfessorOfficeLayoutRequestModel> updatedModel)
            {
                // Performs the specified action
                var response = await requestAction(updatedModel.Model!);

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
                    var responseWithImage = await Client.SetProfessorOfficeLayoutImageAsync(response.Result.Id, updatedModel.File);

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
