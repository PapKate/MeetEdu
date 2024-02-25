using MeetBase;
using MeetBase.Web;

using MudBlazor;

using System.Reflection;

namespace MeetCore
{
    /// <summary>
    /// The contact form page
    /// </summary>
    public partial class Professor_ContactFormPage : FormPage
    {
        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel Professor => StateManager.Professor!;

        /// <summary>
        /// The user of the current <see cref="Professor"/>
        /// </summary>
        public UserResponseModel User => StateManager.User!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Professor_ContactFormPage() : base()
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

            // Gets the department
            var response = await Client.GetProfessorAsync(Professor!.Id);

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

            var vector = mImageVectors.FirstOrDefault(x => x.GetTypeInfo().Name == Professor?.ContactMessageTemplate?.VectorName);
            mSelectedIndex = vector is not null ? mImageVectors.IndexOf(vector) : 0;
            mDescription = Professor?.ContactMessageTemplate?.Description ?? string.Empty;
            mNote = Professor?.ContactMessageTemplate?.Note ?? string.Empty;

            StateHasChanged();
        }


        /// <summary>
        /// Saves the form changes and creates a <see cref="AppointmentContactMessageTemplate"/> for the current <see cref="Professor"/>
        /// </summary>
        protected override async void SaveButton_OnClick(string vectorName)
        {
            // Creates the request for updating the department
            var template = new AppointmentContactMessageTemplate()
            {
                Description = mDescription ?? string.Empty,
                Note = mNote ?? string.Empty,
                VectorName = vectorName,
            };

            // Updates the department
            var response = await Client.UpdateProfessorAsync(Professor!.Id, new ProfessorRequestModel()
            {
                ContactMessageTemplate = template
            });

            // If there was an error...
            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.ErrorMessage);
                // Show the error
                Snackbar.Add(response.ErrorMessage, Severity.Error);
                // Return
                return;
            }

            StateHasChanged();
        }

        #endregion
    }
}
