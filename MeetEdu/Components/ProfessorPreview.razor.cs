using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using static MeetBase.Blazor.CssVariables;

namespace MeetEdu
{
    /// <summary>
    /// The professor preview
    /// </summary>
    public partial class ProfessorPreview
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        [Parameter]
        public ProfessorResponseModel? Model { get; set; }

        /// <summary>
        /// A flag indicating whether it is a preview or not
        /// </summary>
        [Parameter]
        public bool IsPreview { get; set; }

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
        public ProfessorPreview() : base()
        {

        }

        #endregion

        #region Private Methods

        private string SetStyle()
        {
            if (IsPreview)
            {
                return $"cursor: pointer; {BorderBrushVariable.SetCssColor(Model!.User!.Color)}";
            }
            else
            {
                return "border: none;";
            }
        }

        private void ProfessorContainer_OnClick()
        {
            if (!IsPreview)
                return;
            // If no navigation manager is found...
            if (NavigationManager is null)
                // Returns
                return;

            NavigationManager.NavigateToProfessorPage(Model!.Id);
        }

        #endregion
    }
}
