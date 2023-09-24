using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The application container
    /// </summary>
    public partial class ApplicationContainer : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// The child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationContainer() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            StateManager.OnStateChange -= StateHasChanged;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            StateManager.OnStateChange += StateHasChanged;
        }

        #endregion
    }
}
