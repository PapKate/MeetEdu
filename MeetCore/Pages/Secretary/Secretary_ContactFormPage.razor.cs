using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Reflection;

namespace MeetCore
{
    /// <summary>
    /// The contact form page
    /// </summary>
    public partial class Secretary_ContactFormPage : BasePage
    {
        #region Private Members

        private readonly List<Type> mImageVectors = new() { typeof(CoffeeContactVector), typeof(EmailContactVector), typeof(CellPhoneCalendarVector) };

        private string? mText = string.Empty;

        private bool mIsFirstNameChecked = true;
        private bool mIsLastNameChecked = true;
        private bool mIsEmailChecked = true;
        private bool mIsPhoneNumberChecked = false;

        private int mSelectedIndex = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department of the current secretary
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
        public Secretary_ContactFormPage() : base()
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
            var response = await Client.GetDepartmentAsync(Department!.Id);

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

            var vector = mImageVectors.FirstOrDefault(x => x.GetTypeInfo().Name == Department?.ContactMessageTemplate?.VectorName);
            mSelectedIndex = vector is not null ? mImageVectors.IndexOf(vector) : 0;
            mText = Department?.ContactMessageTemplate?.Note ?? string.Empty;
            mIsEmailChecked = Department?.ContactMessageTemplate?.ContactMean == ContactMean.All 
                           || Department?.ContactMessageTemplate?.ContactMean != ContactMean.PhoneNumber ? true : false;

            mIsPhoneNumberChecked = Department?.ContactMessageTemplate?.ContactMean == ContactMean.All
                                 || Department?.ContactMessageTemplate?.ContactMean == ContactMean.PhoneNumber ? true : false;

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private void EmailCheckbox_IsCheckedChanged(bool value)
        {
            mIsEmailChecked = value;

            if (!mIsEmailChecked)
                mIsPhoneNumberChecked = true;
        }

        private void PhoneNumberCheckbox_IsCheckedChanged(bool value)
        {
            mIsPhoneNumberChecked = value;

            if (!mIsPhoneNumberChecked)
                mIsEmailChecked = true;
        }

        private void SaveFormDescription(string? value)
        {
            mText = value;
        }

        private void SetSelectedIndex(int index)
        {
            mSelectedIndex = index;
        }

        private ContactMean GetContactMean()
        {
            if (mIsEmailChecked && mIsPhoneNumberChecked)
                return ContactMean.All;
            else if (mIsEmailChecked && !mIsPhoneNumberChecked)
                return ContactMean.Email;
            else
                return ContactMean.PhoneNumber;
        }

        /// <summary>
        /// Saves the form changes and creates a <see cref="DepartmentContactMessageTemplate"/> for the department of the current <see cref="Department"/>
        /// </summary>
        private async void SaveButton_OnClick()
        {
            var vectorName = mImageVectors.ElementAt(mSelectedIndex).GetTypeInfo().Name;

            // Creates the request for updating the department
            var template = new DepartmentContactMessageTemplate()
            {
                Note = mText ?? string.Empty,
                VectorName = vectorName,
                ContactMean = GetContactMean()
            };

            // Updates the department
            var response = await Client.UpdateDepartmentAsync(Department!.Id, new DepartmentRequestModel()
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

        /// <summary>
        /// Refreshes the form to the default values
        /// </summary>
        private void CancelButton_OnClick()
        {
            mText = string.Empty;
            mIsEmailChecked = true;
            mIsPhoneNumberChecked = true;
            mSelectedIndex = 0;
            StateHasChanged();
        }

        #endregion
    }
}
