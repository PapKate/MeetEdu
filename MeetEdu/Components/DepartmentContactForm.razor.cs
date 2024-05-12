using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetEdu
{
    /// <summary>
    /// The department contact form
    /// </summary>
    public partial class DepartmentContactForm
    {
        #region Private Members

        /// <summary>
        /// The appointment
        /// </summary>
        private DepartmentContactMessageRequestModel mContactMessage = new();

        /// <summary>
        /// The phone number
        /// </summary>
        private PhoneNumber? mPhoneNumber;

        /// <summary>
        /// The vector component
        /// </summary>
        private DynamicComponent? mVectorComponent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The color
        /// </summary>
        [Parameter]
        public string? Color { get; set; }

        /// <summary>
        /// The department message template
        /// </summary>
        [Parameter]
        public DepartmentContactMessageTemplate? FormTemplate { get; set; }

        #endregion

        #region Protected Properties

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
        public DepartmentContactForm() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                if (mVectorComponent?.Instance is BaseVector vector)
                {
                    vector.Color = Color ?? PaletteColors.Gray;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Resets the contact form
        /// </summary>
        private void ResetForm()
        {
            mPhoneNumber = null;
            mContactMessage = new();
            StateHasChanged();
        }

        /// <summary>
        /// Fires the event to send the message
        /// </summary>
        private async void SendButton_OnClick()
        {
            if (FormTemplate is null)
            {
                // Shows the error
                Snackbar.Add($"Error: No form was found!", Severity.Error);

                // Returns
                return;
            }

            if (mPhoneNumber is null || mContactMessage == default
             || mContactMessage.Message.IsNullOrEmpty()
             || mContactMessage.FirstName.IsNullOrEmpty()
             || mContactMessage.LastName.IsNullOrEmpty())
            {
                // Shows the error
                Snackbar.Add($"Error: Please fill all form inputs and try again!", Severity.Error);

                // Returns
                return;
            }

            mContactMessage.PhoneNumber = mPhoneNumber;
            // TODO: mContactMessage.MemberId = "id";


            await SendButtonCliked.InvokeAsync(mContactMessage);

            ResetForm();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the send button is clicked
        /// </summary>
        [Parameter]
        public EventCallback<DepartmentContactMessageRequestModel> SendButtonCliked { get; set; }

        #endregion
    }
}
