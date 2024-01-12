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
    public partial class UpdateLayoutRoomDialog
    {
        #region Private Members

        /// <summary>
        /// The photo input label
        /// </summary>
        private string mPhotoLabel = "Layout image";

        private IBrowserFile? mFile;

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
        public UpdateLayoutRoomDialog() : base()
        {

        }

        #endregion

        #region Private Methods

        private void Save()
        {
            if(Model is not null)
            {
                if(mFile is not null)
                {
                    using var stream = mFile.OpenReadStream();

                    if(stream != null)
                    {
                        Model.File = new FormFile(stream, 0, stream.Length, null, mPhotoLabel) 
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = "image/jpeg"
                        };
                    }
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
