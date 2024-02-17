using MeetBase;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing the schedule
    /// </summary>
    public partial class UpdateScheduleDialog
    {
        #region Private Members

        /// <summary>
        /// The name
        /// </summary>
        private string? mName;

        /// <summary>
        /// The color
        /// </summary>
        private string? mColor;

        /// <summary>
        /// The weekly schedule hours
        /// </summary>
        private List<DayOfWeekTimeRange> mWeeklyHours = new();

        /// <summary>
        /// The note
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public UpdateScheduleModel Model { get; set; } = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateScheduleDialog() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            mColor = Model.WeeklySchedule?.Color ?? LightGray;
            mName = Model.WeeklySchedule?.Name ?? string.Empty;
            mNote = Model.WeeklySchedule?.Note ?? string.Empty;

            if (Model.WeeklySchedule is not null && Model.WeeklySchedule.WeeklyHours is not null)
                mWeeklyHours = Model.WeeklySchedule.WeeklyHours.ToList();
            else
                AddNew();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the starting hour to the specified <paramref name="shift"/>
        /// </summary>
        /// <param name="shift">The shift</param>
        /// <param name="value">The value</param>
        private void SetStartingHour(DayOfWeekTimeRange shift, TimeSpan? value)
        {
            if (value is null)
                return;
            var index = mWeeklyHours.IndexOf(shift);
            mWeeklyHours[index] = new(shift.Text, shift.DayOfWeek, TimeOnly.FromTimeSpan(value.Value), shift.End);
        }

        /// <summary>
        /// Sets the ending hour to the specified <paramref name="shift"/>
        /// </summary>
        /// <param name="shift">The shift</param>
        /// <param name="value">The value</param>
        private void SetEndingingHour(DayOfWeekTimeRange shift, TimeSpan? value)
        {
            if (value is null)
                return;
            var index = mWeeklyHours.IndexOf(shift);
            mWeeklyHours[index] = new(shift.Text, shift.DayOfWeek, shift.Start, TimeOnly.FromTimeSpan(value.Value));
        }

        /// <summary>
        /// Sets the day of week to the specified <paramref name="shift"/>
        /// </summary>
        /// <param name="shift">The shift</param>
        /// <param name="value">The value</param>
        private void SetDayOfWeek(DayOfWeekTimeRange shift, DayOfWeek value)
        {
            var index = mWeeklyHours.IndexOf(shift);
            mWeeklyHours[index] = new(shift.Text, value, shift.Start, shift.End);
        }

        /// <summary>
        /// Sets the day of week to the specified <paramref name="shift"/>
        /// </summary>
        /// <param name="shift">The shift</param>
        /// <param name="value">The value</param>
        private void SetText(DayOfWeekTimeRange shift, string value)
        {
            var index = mWeeklyHours.IndexOf(shift);
            mWeeklyHours[index] = new(value, shift.DayOfWeek, shift.Start, shift.End);
        }

        private void AddNew()
        {
            mWeeklyHours.Insert(0, default);
            StateHasChanged();
        }

        private void Save()
        {
            mWeeklyHours.RemoveAll(x => x == default);

            Model.WeeklySchedule = new WeeklySchedule() 
            {
                Name = mName ?? string.Empty,
                Note = mNote ?? string.Empty,
                Color = mColor ?? string.Empty,
                WeeklyHours = mWeeklyHours
            };

            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
