using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MeetCore
{
    public partial class AppointmentsDataGrid
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        #endregion

        #region Public Properties

        /// <summary>
        /// The appointments
        /// </summary>
        [Parameter]
        public IEnumerable<AppointmentResponseModel>? Appointments { get; set; }

        /// <summary>
        /// A flag indicating whether the <see cref="Appointments"/> are editable or not
        /// </summary>
        [Parameter]
        public bool IsEditable { get; set; }

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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentsDataGrid() : base()
        {

        }

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        /// <summary>
        /// Edits the specified <paramref name="appointment"/>
        /// </summary>
        /// <param name="appointment">The rule</param>
        private async void ActionButton_OnClick(AppointmentResponseModel appointment)
        {
            await ActionButtonClicked.InvokeAsync(appointment);
        }

        /// <summary>
        /// Sets the color for the specified <paramref name="status"/>
        /// </summary>
        /// <param name="status">The status</param>
        /// <returns></returns>
        private string SetStatusColor(AppointmentStatus status)
        {
            switch (status)
            {
                case AppointmentStatus.Pending:
                    return PaletteColors.Amber;
                case AppointmentStatus.Confirmed:
                    return PaletteColors.Blue;
                case AppointmentStatus.Completed:
                    return PaletteColors.Green;
                case AppointmentStatus.Canceled:
                    return PaletteColors.Red;
                default:
                    return PaletteColors.Amber;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the action button of an appointment is clicked
        /// </summary>
        [Parameter]
        public EventCallback<AppointmentResponseModel> ActionButtonClicked { get; set; }

        #endregion
    }
}
