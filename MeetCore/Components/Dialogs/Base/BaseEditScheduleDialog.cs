using MeetBase;

using Microsoft.AspNetCore.Components;

using MudBlazor;
using MudBlazor.Utilities;

namespace MeetCore
{
    /// <summary>
    /// The base dialog for editing a schedule
    /// </summary>
    public class BaseEditScheduleDialog : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The color
        /// </summary>
        protected string? mColor;

        /// <summary>
        /// The index
        /// </summary>
        protected int mIndex = 0;

        /// <summary>
        /// The weekly schedule hours
        /// </summary>
        protected List<DayOfWeekTimeRange> mWeeklyHours = new();

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEditScheduleDialog() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the starting hour to the specified <paramref name="shift"/>
        /// </summary>
        /// <param name="shift">The shift</param>
        /// <param name="value">The value</param>
        protected void SetStartingHour(DayOfWeekTimeRange shift, TimeSpan? value)
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
        protected void SetEndingingHour(DayOfWeekTimeRange shift, TimeSpan? value)
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
        protected void SetDayOfWeek(DayOfWeekTimeRange shift, DayOfWeek value)
        {
            var index = mWeeklyHours.IndexOf(shift);
            mWeeklyHours[index] = new(shift.Text, value, shift.Start, shift.End);
        }

        /// <summary>
        /// Sets the day of week to the specified <paramref name="shift"/>
        /// </summary>
        /// <param name="shift">The shift</param>
        /// <param name="value">The value</param>
        protected void SetText(DayOfWeekTimeRange shift, string value)
        {
            var index = mWeeklyHours.IndexOf(shift);
            mWeeklyHours[index] = new(value, shift.DayOfWeek, shift.Start, shift.End);
        }

        /// <summary>
        /// Adds a new <see cref="DayOfWeekTimeRange"/> to the <see cref="mWeeklyHours"/> list
        /// </summary>
        protected void AddNew()
        {
            mWeeklyHours.Insert(0, default);
            StateHasChanged();
        }

        /// <summary>
        /// Closes the dialog
        /// </summary>
        protected void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
