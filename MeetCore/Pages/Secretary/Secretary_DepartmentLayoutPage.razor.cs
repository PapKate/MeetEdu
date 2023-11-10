using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore
{
    /// <summary>
    /// The department layout page
    /// </summary>
    public partial class Secretary_DepartmentLayoutPage : BasePage
    {
        #region Private Members

        private bool mIsLayoutDescripitonReadOnly = true;

        private string mLayoutDescription = string.Empty;
        
        private string mLayoutDescriptionInput = string.Empty;

        private string mLayoutId = string.Empty;

        private IEnumerable<DepartmentLayoutRoom> mLayoutRooms = new List<DepartmentLayoutRoom>();

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
        protected override async void OnInitialized()
        {
            var response = await Client.GetDepartmentLayoutsAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            if(!response.IsSuccessful)
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
            var layout = response.Result.First();
            mLayoutId = layout.Id;
            mLayoutDescription = layout.Description;
            mLayoutDescriptionInput = mLayoutDescription;
            mLayoutRooms = response.Result.SelectMany(x => x.Rooms).ToList();
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private async void SaveButton_Onclick()
        {
            mIsLayoutDescripitonReadOnly = true;
            mLayoutDescription = mLayoutDescriptionInput;

            // Creates the request for updating the layout
            var request = new DepartmentLayoutRequestModel()
            {
                Description = mLayoutDescription
            };

            // Updates the layout
            var response = await Client.UpdateDepartmentLayoutAsync(mLayoutId, request);

            // If there was an error...
            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }
        }

        /// <summary>
        /// Adds a layout or a room if the layout exists
        /// </summary>
        private void AddButton_Onclick()
        {
            // If the department has no layout...
            if (mLayoutId.IsNullOrEmpty())
            {
                // Creates and adds a layout
                AddLayout();
                return;
            }

            UpdateLayout(new());
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
            };

            // Adds the layout
            var response = await Client.AddDepartmentLayoutAsync(request);

            // If there was an error...
            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            mLayoutId = response.Result.Id;

            StateHasChanged();
        }

        private async void UpdateLayout(DepartmentLayoutRoom room)
        {
            var list = mLayoutRooms.ToList();

            var index = list.IndexOf(room);

            var parameters = new DialogParameters<UpdateLayoutRoomDialog> { { x => x.Model, room } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<UpdateLayoutRoomDialog>(null, parameters, mDialogOptions);

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
            if (result.Data is DepartmentLayoutRoom updatedModel)
            {
                if(index < 0)
                    list.Add(room);
                else
                    list[index] = room;

                // Creates the request for updating the layout
                var layoutRequest = new DepartmentLayoutRequestModel()
                {
                    Rooms = list
                };

                // Updates the layout
                var layoutResponse = await Client.UpdateDepartmentLayoutAsync(mLayoutId, layoutRequest);

                // If there was an error...
                if (!layoutResponse.IsSuccessful)
                {
                    Console.WriteLine(layoutResponse.ErrorMessage);
                    // Show the error
                    Snackbar.Add(layoutResponse.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                mLayoutRooms = new List<DepartmentLayoutRoom>();
                StateHasChanged();
                mLayoutRooms = layoutResponse.Result.Rooms;
                StateHasChanged();
            }
        }

        #endregion
    }
}
