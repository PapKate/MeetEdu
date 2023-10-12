using MeetBase;

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
        public DepartmentLayoutRoom? Model { get; set; }

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

        private async void BrowserFileUploaded(IBrowserFile file)
        {
            if(Model is not null)
            {
                var buffers = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffers);
                var imageType = file.ContentType;
                var imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
                Model.ImageUrl = new Uri(imgUrl);
            }
        }

        #endregion
    }
}
