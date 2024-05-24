using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using MudBlazor;
using System.Data;

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
        private TextButton? mSetDateButton;

        /// <summary>
        /// A flag indicating whether the form is visible or not
        /// </summary>
        private bool mIsFormVisible = true;

        /// <summary>
        /// Additional CSS class for the contact form container
        /// </summary>
        private string mFormCssClass = string.Empty;

        /// <summary>
        /// Additional CSS class for the appointment information
        /// </summary>
        private string mAppointmentInfoCssStyle = "hidden";

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
        /// The <see cref="IJSRuntime"/> service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        /// <summary>
        /// The controller
        /// </summary>
        [Inject]
        protected MeetEduController Controller { get; set; } = default!;
        
        /// <summary>
        /// The hub client
        /// </summary>
        [Inject]
        protected AccountsHubClient HubClient { get; set; } = default!;

        /// <summary>
        /// A flag indicating whether the form is visible or not
        /// </summary>
        protected bool IsFormVisible
        {
            get => mIsFormVisible;

            set
            {
                mIsFormVisible = value;

                if (!mIsFormVisible)
                {
                    mFormCssClass = "hidden";
                    mAppointmentInfoCssStyle = string.Empty;
                    return;
                }

                mFormCssClass = string.Empty;
                mAppointmentInfoCssStyle = "hidden";
            }
        }

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
            //  If no rule is set...
            if (mRule is null)
            {
                // Shows the error
                Snackbar.Add("Error: Please select a rule and try again!", Severity.Error);

                // Returns
                return;
            }

            //  If the professor has no office hours...
            if (Professor?.WeeklySchedule is null)
            {
                // Shows the error
                Snackbar.Add("Error: Cannot find time slot available for an appointment!", Severity.Error);

                // Returns
                return;
            }

            // Gets the reserved appointment time slots 
            var reservedTimeSlots = await GetReservedTimeSlotsAsync();

            // Gets the available time slots for an appointment
            var availableSlots = GetAvailableTimeSlots(reservedTimeSlots ?? new());

            // If no appointment time slot is available...
            if (availableSlots.IsNullOrEmpty())
            {
                // Shows the error
                Snackbar.Add("Error: Cannot find time slot available for an appointment!", Severity.Error);

                // Returns
                return;
            }

            var parameters = new DialogParameters<SetAppointmentDateDialog> { { x => x.Slots, availableSlots.OrderBy(x => x.Minimum) }, { x => x.Color, Professor.User!.Color } };

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
                mSetDateButton!.Text = $"{value.Minimum.ToString("dd/MM/yyyy")} {value.Minimum.ToString("HH:mm")} - {value.Maximum.ToString("HH:mm")}";
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
            mIsRemote = false;
            StateHasChanged();
        }

        /// <summary>
        /// Fires the event to send the message
        /// </summary>
        private async void SendButton_OnClick()
        {
            if (Professor is null)
            {
                // Shows the error
                Snackbar.Add($"Error: No professor was found!", Severity.Error);

                // Returns
                return;
            }

            if (mRule is null || mPhoneNumber is null || mAppointment is null || mAppointment.DateStart == DateTimeOffset.MinValue)
            {
                // Shows the error
                Snackbar.Add($"Error: Please fill all for inputs and try again!", Severity.Error);

                // Returns
                return;
            }
            mAppointment.IsRemote = mIsRemote;
            mAppointment.PhoneNumber = mPhoneNumber;
            mAppointment.RuleId = mRule.Id;
            mAppointment.ProfessorId = Professor!.Id;

            // TODO: mAppointment.MemberId = "id";

            var appointmentResponse = await Controller.AddAppointmentAsync(mAppointment);

            if (appointmentResponse.Result is null)
            {
                // Shows the error
                Snackbar.Add($"Error: {appointmentResponse.Value}", Severity.Error);

                // Returns
                return;
            }
            
            await HubClient.SendAppointmentsCreatedAsync(new List<AppointmentResponseModel>() { (AppointmentResponseModel)((ObjectResult)appointmentResponse.Result)!.Value! });
            
            IsFormVisible = false;
            StateHasChanged();

            Snackbar.Add("Success: Appointment created!", Severity.Success);
        }

        /// <summary>
        /// Generates and downloads the calendar event
        /// </summary>
        private async void DownloadEvent_OnClick()
        {
            var eventContent = CalendarHelpers.GenerateCalendarEvent(mRule!.Name, mAppointment.Message, mAppointment.DateStart.DateTime, mRule.Duration);

            await JSRunTimeHelpers.DownloadCalendarEventsAsync(JSRuntime, eventContent, mRule.Name);
        }

        /// <summary>
        /// Resets and shows the form 
        /// </summary>
        private void ReturnToForm_OnClick()
        {
            ResetForm();
            IsFormVisible = true;
        }

        /// <summary>
        /// Gets the reserved time slots of the <see cref="Professor"/>
        /// </summary>
        private async Task<List<IReadOnlyRangeable<DateTimeOffset>>?> GetReservedTimeSlotsAsync()
        {
            // Gets the professor appointments
            var professorAppointmentsResponse = await Controller.GetAppointmentsAsync(new() { IncludeProfessors = new List<string>() { Professor!.Id } });

            // Gets the reserved appointment time slots 
            var reservedTimeSlots = professorAppointmentsResponse.Value?
                                        .Select(x => (IReadOnlyRangeable<DateTimeOffset>)new MeetBase.Range<DateTimeOffset>(x.DateStart, x.DateStart + mRule!.Duration)).ToList();

            return reservedTimeSlots;
        }

        /// <summary>
        /// Gets the available time slots for an appointment
        /// </summary>
        /// <param name="reservedTimeSlots">The reserved time slots from other appointments</param>
        /// <returns></returns>
        private List<IReadOnlyRangeable<DateTimeOffset>> GetAvailableTimeSlots(List<IReadOnlyRangeable<DateTimeOffset>> reservedTimeSlots)
        {
            // If the date start of the rule is older than now get now else the date 
            var dateFrom = mRule!.DateFrom < DateTimeOffset.Now ? DateTimeOffset.Now : mRule.DateFrom;

            // Calculates the available appointment time slots
            var availableSlots = QuartzHelpers.CalculateAppointmentDates(
                dateFrom,
                mRule.DateTo,
                Professor!.WeeklySchedule!.WeeklyHours,
                mRule.Duration,
                mRule.StartMinutes,
                reservedTimeSlots ?? new());

            availableSlots = availableSlots.Where(x => x.Maximum <= DateTimeOffset.Now.AddDays(21)).ToList();

            var supportedSlots = new List<IReadOnlyRangeable<DateTimeOffset>>();

            // For each office hour of the professor...
            foreach (var weeklyHour in Professor.WeeklySchedule.WeeklyHours)
            {
                // Gets the available time slots per date
                var selectResult = availableSlots.Where(x => x.Minimum.DayOfWeek == weeklyHour.DayOfWeek
                                                               && x.Minimum.Day == x.Maximum.Day
                                                               && new TimeOnly(x.Minimum.Hour, x.Minimum.Minute) >= weeklyHour.Start
                                                               && new TimeOnly(x.Maximum.Hour, x.Maximum.Minute) <= weeklyHour.End).ToList();

                supportedSlots.AddRange(selectResult);
            }

            return supportedSlots;
        }

        #endregion
    }
}
