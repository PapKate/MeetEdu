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
        /// The id of the container
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The additional CSS classes of the page container
        /// </summary>
        [Parameter]
        public string? CssClass { get; set; }

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
            Id = Guid.NewGuid().ToString();
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
