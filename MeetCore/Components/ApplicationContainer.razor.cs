using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// The application container
    /// </summary>
    public partial class ApplicationContainer
    {
        #region Public Properties

        /// <summary>
        /// The child content
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        protected StateManagerCore StateManager { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationContainer() : base()
        {

        }

        #endregion
    }
}
