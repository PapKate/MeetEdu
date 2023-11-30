using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing the room
    /// </summary>
    public partial class UpdateLayoutRoomDialog
    {
        #region Private Members

        /// <summary>
        /// The photo input label
        /// </summary>
        private string mPhotoLabel = "Layout image";

        private IBrowserFile mFile;

        #endregion

        #region Public Properties

        /// <summary>
        /// The dialog instance
        /// </summary>
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        /// <summary>
        /// THe model
        /// </summary>
        [Parameter]
        public DepartmentLayoutRequestModel? Model { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateLayoutRoomDialog() : base()
        {

        }

        #endregion

        #region Private Methods

        private void Save()
        {
            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
