using MeetBase;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The dialog for editing the room
    /// </summary>
    public partial class UpdateLayoutDialog
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
        public UpdateLayoutModel? Model { get; set; }

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
                mTheme = Model.Model.DisplayTheme ?? ImageDisplayTheme.Left;
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
                Model.Model.DisplayTheme = mTheme;
                Model.Model.Color = Model.Model?.Color?.Replace("#", string.Empty);
                if (mFile is not null)
                {
                    using var stream = mFile.OpenReadStream(512000000);
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);

                    Model.File = new FormFile(memoryStream, 0, stream.Length, null, mPhotoLabel)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg"
                    };
                }
            }
            MudDialog.Close(DialogResult.Ok(Model));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion

        #region Classes

        /// <summary>
        /// The dialog's model
        /// </summary>
        public class UpdateLayoutModel
        {
            #region Public Properties

            /// <summary>
            /// The model
            /// </summary>
            public DepartmentLayoutRequestModel Model { get; set; }

            /// <summary>
            /// The image
            /// </summary>
            public IFormFile? File { get; set; }

            #endregion

            #region Constructors

            /// <summary>
            /// Default constructor
            /// </summary>
            public UpdateLayoutModel(DepartmentLayoutRequestModel model) : base()
            {
                Model = model;
            }

            #endregion
        }

        #endregion
    }
}
