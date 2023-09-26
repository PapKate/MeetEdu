using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing the schedule
    /// </summary>
    public partial class UpdateScheduleDialog
    {
        #region Public Properties

        /// <summary>
        /// A flag indicating whether the <see cref="MudDialog"/> for editing the work hours is open or not
        /// </summary>
        private bool mIsDialogOpen;

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        /// <summary>
        /// The weekly schedule
        /// </summary>
        private WeeklySchedule mWeeklySchedule = new WeeklySchedule();

        /// <summary>
        /// The work hours
        /// </summary>
        private List<DayOfWeekTimeRange> mWorkHours = new List<DayOfWeekTimeRange>();

        private DayOfWeek mTempValue = DayOfWeek.Sunday;

        #endregion

        #region Private Methods

        /// <summary>
        /// Closes the dialog
        /// </summary>
        private void CloseDialog() => mIsDialogOpen = false;

        /// <summary>
        /// Updates the secretary and user info
        /// </summary>
        private async void SaveChanges()
        {
            CloseDialog();
            StateHasChanged();
        }

        private void CancelChanges()
        {
            CloseDialog();
        }
        #endregion
    }
}
