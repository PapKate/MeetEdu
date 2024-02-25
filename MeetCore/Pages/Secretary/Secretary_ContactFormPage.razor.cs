using MeetBase;
using MeetBase.Web;

using MudBlazor;

using System.Reflection;

namespace MeetCore
{
    /// <summary>
    /// The contact form page
    /// </summary>
    public partial class Secretary_ContactFormPage : FormPage
    {
        #region Private Members

        /// <summary>
        /// A flag indicating whether the first name is checked
        /// </summary>
        private bool mIsFirstNameChecked = true;

        /// <summary>
        /// A flag indicating whether the last name is checked
        /// </summary>
        private bool mIsLastNameChecked = true;

        /// <summary>
        /// A flag indicating whether the email is checked
        /// </summary>
        private bool mIsEmailChecked = true;

        /// <summary>
        /// A flag indicating whether the phone number is checked
        /// </summary>
        private bool mIsPhoneNumberChecked = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department of the current secretary
        /// </summary>
        public DepartmentResponseModel Department => StateManager.Department!;

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
            mDescription = Department?.ContactMessageTemplate?.Description ?? string.Empty;
            mNote = Department?.ContactMessageTemplate?.Note ?? string.Empty;
            mIsEmailChecked = Department?.ContactMessageTemplate?.ContactMean == ContactMean.All
                           || Department?.ContactMessageTemplate?.ContactMean != ContactMean.PhoneNumber ? true : false;

            mIsPhoneNumberChecked = Department?.ContactMessageTemplate?.ContactMean == ContactMean.All
                                 || Department?.ContactMessageTemplate?.ContactMean == ContactMean.PhoneNumber ? true : false;

            StateHasChanged();
        }

        /// <summary>
        /// Sets the phone number and email option values accordingly
        /// </summary>
        /// <param name="value">The email option value</param>
        protected void EmailCheckbox_IsCheckedChanged(bool value)
        {
            mIsEmailChecked = value;

            if (!mIsEmailChecked)
                mIsPhoneNumberChecked = true;
        }

        /// <summary>
        /// Sets the phone number and email option values accordingly
        /// </summary>
        /// <param name="value">The phone number option value</param>
        protected void PhoneNumberCheckbox_IsCheckedChanged(bool value)
        {
            mIsPhoneNumberChecked = value;

            if (!mIsPhoneNumberChecked)
                mIsEmailChecked = true;
        }

        /// <summary>
        /// Gets the contact mean for the form template
        /// </summary>
        /// <returns></returns>
        protected ContactMean GetContactMean()
        {
            if (mIsEmailChecked && mIsPhoneNumberChecked)
                return ContactMean.All;
            else if (mIsEmailChecked && !mIsPhoneNumberChecked)
                return ContactMean.Email;
            else
                return ContactMean.PhoneNumber;
        }

        /// <summary>
        /// Refreshes the form to the default values
        /// </summary>
        protected override void CancelButton_OnClick()
        {
            mIsEmailChecked = true;
            mIsPhoneNumberChecked = true;
        }

        /// <summary>
        /// Saves the form changes and creates a <see cref="DepartmentContactMessageTemplate"/> for the department of the current <see cref="Department"/>
        /// </summary>
        protected override async void SaveButton_OnClick(string vectorName)
        {
            // Creates the request for updating the department
            var template = new DepartmentContactMessageTemplate()
            {
                Description = mDescription ?? string.Empty,
                Note = mNote ?? string.Empty,
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

        #endregion
    }
}
