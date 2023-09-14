using MeetBase.Blazor;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// Provides abstractions over the <see cref="StateManager"/>
    /// </summary>
    public class StateManagablePage : BasePage, IDisposable
    {
        #region Public Properties

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        public StateManagerCore? StateManager { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StateManagablePage() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            // If the manager exists...
            if(StateManager is not null)
                // Unsubscribes from the state has changed event
                StateManager.OnStateChange -= StateHasChanged;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // If the manager exists...
            if(StateManager is not null)
                // Subscribes to the state has changed event
                StateManager.OnStateChange += StateHasChanged;

            OnInitializedCore();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its initial parameters from its parent in the render tree
        /// </summary>
        protected virtual void OnInitializedCore()
        {

        }

        #endregion
    }

    /// <summary>
    /// The state manager for the <see cref="MeetCore"/> app
    /// </summary>
    public class StateManagerCore : StateManager
    {
        #region Public Properties

        /// <summary>
        /// A flag indicating whether the staff member is a secretary
        /// </summary>
        public bool IsSecretary { get; set; } = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StateManagerCore() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The method that will be accessed by the sender component to update the state
        /// </summary>
        public void SetIsSecretary(bool isSecretary)
        {
            IsSecretary = isSecretary;
            NotifyStateChanged();
        }

        #endregion
    }
}
