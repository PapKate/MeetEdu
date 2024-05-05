using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace MeetEdu
{
    /// <summary>
    /// The professor contact form
    /// </summary>
    public partial class ProfessorContactForm
    {
        #region Private Members

        /// <summary>
        /// The appointment
        /// </summary>
        private AppointmentRequestModel mAppointment = new();

        /// <summary>
        /// The phone number
        /// </summary>
        private PhoneNumber? mPhoneNumber;

        /// <summary>
        /// The flag indicating whether the appointment is gonna be remote or not
        /// </summary>
        private bool mIsRemote;

        /// <summary>
        /// The selected rule
        /// </summary>
        private AppointmentRuleResponseModel? mRule;

        /// <summary>
        /// The vector component
        /// </summary>
        private DynamicComponent? mVectorComponent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor
        /// </summary>
        [Parameter]
        public ProfessorResponseModel? Professor { get; set; }

        /// <summary>
        /// The appointment template
        /// </summary>
        [Parameter]
        public AppointmentContactMessageTemplate? FormTemplate { get; set; }

        /// <summary>
        /// The appointment rules
        /// </summary>
        [Parameter]
        public IEnumerable<AppointmentRuleResponseModel>? AppointmentRules { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetEduController Controller { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorContactForm() : base()
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
                    vector.Color = Professor!.User!.Color;
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
            mRule = null;
            mPhoneNumber = null;
            mAppointment = new();
            StateHasChanged();
        }

        /// <summary>
        /// Fires the event to send the message
        /// </summary>
        private async void SendButton_OnClick()
        {
            if(mRule is null || mPhoneNumber is null)
            {
                return;
            }

            mAppointment.PhoneNumber = mPhoneNumber;
            mAppointment.RuleId = mRule.Id;
            mAppointment.ProfessorId = Professor!.Id;

            mAppointment.DateStart = DateTime.Now.AddDays(2);
            // TODO: mAppointment.MemberId = "id";
         
            if (Professor is null)
                return;

            var appointmentResponse = await Controller.AddAppointmentAsync(mAppointment);

            if (appointmentResponse.Value is null)
            {
                Snackbar.Add($"Error: {appointmentResponse.Value}", Severity.Error);

                return;
            }

            // TODO: Success message
            Snackbar.Add("Success: Appointment created!", Severity.Success);
        }

        #endregion
    }
}
