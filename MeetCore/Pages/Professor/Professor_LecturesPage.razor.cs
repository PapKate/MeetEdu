using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The professor lectures page
    /// </summary>
    public partial class Professor_LecturesPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private List<Lecture> mLectures = new();

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
        public Professor_LecturesPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            mLectures = new();
            mLectures.AddRange(Professor.Lectures);

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Removes the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The rule</param>
        private async void RemoveLecture(Lecture model)
        {
            mLectures.Remove(model);

            var request = new ProfessorRequestModel()
            {
                Lectures = mLectures
            };

            // Updates the professor
            var response = await Client.UpdateProfessorAsync(Professor.Id, request);

            // If there was an error...
            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            StateManager.Professor = response.Result;
            mLectures = new();
            mLectures.AddRange(Professor.Lectures);
            StateHasChanged();

        }

        /// <summary>
        /// Edits the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The rule</param>
        private async void EditLecture(Lecture model)
        {
            var parameters = new DialogParameters<EditLectureDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<EditLectureDialog>(null, parameters, mDialogOptions);

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
            if (result.Data is Lecture updatedModel)
            {
                var updatedLectures = new List<Lecture>();
                updatedLectures.AddRange(mLectures);

                if(updatedLectures.Contains(model))
                {
                    var index = updatedLectures.IndexOf(model);
                    updatedLectures.Remove(model);

                    updatedLectures.Insert(index, updatedModel);
                }
                else
                    updatedLectures.Add(model);

                var request = new ProfessorRequestModel()
                {
                    Lectures = updatedLectures
                };

                // Updates the professor
                var response = await Client.UpdateProfessorAsync(Professor.Id, request);

                // If there was an error...
                if (!response.IsSuccessful)
                {
                    Console.WriteLine(response.ErrorMessage);
                    // Show the error
                    Snackbar.Add(response.ErrorMessage, Severity.Error);
                    // Return
                    return;
                }

                StateManager.Professor = response.Result;
                mLectures = new();
                mLectures.AddRange(Professor.Lectures);
                StateHasChanged();
            }
        }

        #endregion
    }
}
