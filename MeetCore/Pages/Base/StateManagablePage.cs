using MeetBase.Blazor;
using MeetBase.Web;

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
        public StateManagerCore StateManager { get; set; } = default!;

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
        public bool IsSecretary { get; set; }

        /// <summary>
        /// The user
        /// </summary>
        public UserResponseModel? User { get; set; }

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel? Secretary { get; set; }

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorResponseModel? Professor { get; set; }

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel? Department { get; set; }

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
        /// Resets the sate manager values
        /// </summary>
        public void ResetManager()
        {
            IsSecretary = false;
            User = null;
            Secretary = null;
            Professor = null;
        }

        /// <summary>
        /// Sets the values for the <see cref="User"/> and  <see cref="Secretary"/> properties
        /// </summary>
        /// <remarks>The method that will be accessed by the sender component to update the state</remarks>
        public void SetLoginUserData(bool isSecretary, UserResponseModel user, SecretaryResponseModel secretary, DepartmentResponseModel department)
        {
            IsSecretary = isSecretary;
            User = user;
            Secretary = secretary;
            Department = department;
            NotifyStateChanged();
        }

        /// <summary>
        /// Sets the values for the <see cref="User"/> and  <see cref="Professor"/> properties
        /// </summary>
        /// <remarks>The method that will be accessed by the sender component to update the state</remarks>
        public void SetLoginUserData(bool isSecretary, UserResponseModel user, ProfessorResponseModel professor, DepartmentResponseModel department)
        {
            IsSecretary = isSecretary;
            User = user;
            Professor = professor;
            Department = department;

            NotifyStateChanged();
        }

        #endregion
    }
}
