using MeetBase;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing the schedule
    /// </summary>
    public partial class UpdateScheduleDialog : BaseEditScheduleDialog
    {
        #region Private Members

        /// <summary>
        /// The name
        /// </summary>
        private string? mName;

        /// <summary>
        /// The note
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

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

            if (Model.WeeklySchedule is null || Model.WeeklySchedule.Color.IsNullOrEmpty())
                mColor = LightGray;
            else
                mColor = Model.WeeklySchedule.Color;
            mName = Model.WeeklySchedule?.Name ?? string.Empty;
            mNote = Model.WeeklySchedule?.Note ?? string.Empty;

            if (Model.WeeklySchedule is not null && !Model.WeeklySchedule.WeeklyHours.IsNullOrEmpty())
                mWeeklyHours = Model.WeeklySchedule.WeeklyHours.ToList();
            else
                AddNew();
        }

        #endregion

        #region Private Methods

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

        #endregion
    }
}
