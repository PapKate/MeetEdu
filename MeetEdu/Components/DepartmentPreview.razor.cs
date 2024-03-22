using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MeetEdu
{
    /// <summary>
    /// The department preview
    /// </summary>
    public partial class DepartmentPreview
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public DepartmentResponseModel? Model { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The navigation manager service
        /// </summary>
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// The JS runtime service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentPreview() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("ShowLeafletMap", Model!.Id, 38.289857021650526, 21.795411784348772);
            }

        }

        #endregion

        #region Private Methods

        private void DepartmentContainer_OnClick()
        {
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            NavigationManager.NavigateToDepartmentPage(Model!.Id);
        }

        #endregion
    }
}
