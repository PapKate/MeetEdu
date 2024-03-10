using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing the room
    /// </summary>
    public partial class UpdateLayoutDialog<T>
        where T : LayoutRequestModel, new()
    {
        #region Private Members

        /// <summary>
        /// The photo input label
        /// </summary>
        private string mPhotoLabel = "Layout image";

        private IBrowserFile? mFile;

        private ImageDisplayTheme mTheme;

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
        public UpdateModel<T>? Model { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateLayoutDialog() : base()
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
            if(Model is not null)
            {
                mTheme = Model?.Model?.DisplayTheme ?? ImageDisplayTheme.Left;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the changes
        /// </summary>
        private async void Save()
        {
            if(Model is not null)
            {
                Model.Model!.DisplayTheme = mTheme;
                Model.Model.Color = Model.Model?.Color?.Replace("#", string.Empty) ?? string.Empty;
                
                if (mFile is not null)
                {
                    Model.File = await mFile.ToIFormFileAsync(mPhotoLabel);
                }
            }
            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion
    }
}
