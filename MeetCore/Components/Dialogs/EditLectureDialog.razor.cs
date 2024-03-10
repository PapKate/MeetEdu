using MeetBase;
using Microsoft.AspNetCore.Components;
using MudBlazor;

using static MeetBase.Blazor.PaletteColors;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing a lecture
    /// </summary>
    public partial class EditLectureDialog : BaseEditScheduleDialog
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public Lecture Model { get; set; } = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditLectureDialog() : base()
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

            mColor = Model.Color.IsNullOrEmpty() ? LightGray : Model.Color;

            if (Model is not null && !Model.WeeklyHours.IsNullOrEmpty())
                mWeeklyHours = Model.WeeklyHours.ToList();
            else
                AddNew();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the changes
        /// </summary>
        private void Save()
        {
            mWeeklyHours.RemoveAll(x => x == default);

            Model.Color = mColor ?? string.Empty;
            Model.WeeklyHours = mWeeklyHours;

            MudDialog.Close(DialogResult.Ok(Model));
        }

        #endregion
    }
}
