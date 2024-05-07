using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetEdu
{
    /// <summary>
    /// The professor contact form
    /// </summary>
    public partial class ProfessorContactForm
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

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

        /// <summary>
        /// The text of the set date button
        /// </summary>
        private string mSetDateButtonText = "Set date";

        /// <summary>
        /// The text button
        /// </summary>
        private TextButton mSetDateButton;

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
        /// Sets the date for the contact
        /// </summary>
        private async void SetDate_OnClick()
        {
            if (Professor?.WeeklySchedule is null || mRule is null)
            {
                return;
            }

            var availableSlots = QuartzHelpers.CalculateAppointmentDates(mRule.DateFrom, mRule.DateTo, Professor.WeeklySchedule.WeeklyHours, mRule.Duration, mRule.StartMinutes, new());

            var supportedSlots = new List<IReadOnlyRangeable<DateTimeOffset>>();

            foreach (var weeklyHour in Professor.WeeklySchedule.WeeklyHours)
            {
                var selectResult = availableSlots.Where(x => x.Minimum.DayOfWeek == weeklyHour.DayOfWeek
                                                               && x.Minimum.Day == x.Maximum.Day
                                                               && new TimeOnly(x.Minimum.Hour, x.Minimum.Minute) >= weeklyHour.Start
                                                               && new TimeOnly(x.Maximum.Hour, x.Maximum.Minute) <= weeklyHour.End).ToList();

                supportedSlots.AddRange(selectResult);
            }

            var parameters = new DialogParameters<SetAppointmentDateDialog> { { x => x.Slots, supportedSlots.OrderBy(x => x.Minimum) }, { x => x.Color, Professor.User!.Color } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<SetAppointmentDateDialog>(null, parameters, mDialogOptions);

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
            if (result.Data is IReadOnlyRangeable<DateTimeOffset> value)
            {
                mAppointment.DateStart = value.Minimum;
                mSetDateButton.Text = $"{value.Minimum.ToString("dd/MM/yyyy")} {value.Minimum.ToString("hh:mm")} - {value.Maximum.ToString("hh:mm")}";
                mSetDateButton.InvokeStateHasChanged();
            }
        }

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
            if (mRule is null || mPhoneNumber is null)
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
