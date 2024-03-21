using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    /// <summary>
    /// Provides abstractions over the <see cref="StateManager"/>
    /// </summary>
    public class StateManagablePage : ComponentBase, IDisposable
    {
        #region Public Properties

        /// <summary>
        /// The state management service
        /// </summary>
        [Inject]
        public StateManagerCore StateManager { get; set; } = default!;

        /// <summary>
        /// The session storage management service
        /// </summary>
        [Inject]
        public SessionStorageManager SessionStorageManager { get; set; } = default!;

        /// <summary>
        /// The client
        /// </summary>
        [Inject]
        protected MeetCoreClient Client { get; set; } = default!;

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
            // Unsubscribes from the state has changed event
            StateManager.OnStateChange -= StateHasChanged;
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override async void OnInitialized()
        {
            base.OnInitialized();

            // Subscribes to the state has changed event
            StateManager.OnStateChange += StateHasChanged;

            // If the state manager service is empty...
            if (StateManager.User is null)
            {
                // Gets the user id from the session storage
                var userId = await SessionStorageManager.GetValueAsync<string?>(SessionStorageManager.UserId);

                // If the user id does not exist...
                if (userId.IsNullOrEmpty())
                    // Return
                    return;

                // Gets the user
                var user = await Client.GetUserAsync(userId);
                // Gets the department from the database
                var department = await Client.GetDepartmentAsync(await SessionStorageManager.GetValueAsync<string>(SessionStorageManager.DepartmentId));
                
                // Gets from the session storage whether a secretary is logged in or not
                var isSecretary = await SessionStorageManager.GetValueAsync<bool>(SessionStorageManager.IsSecretary);
                // If it is a secretary logged in...
                if (isSecretary)
                {
                    // Gets the secretary from the database
                    var secretary = await Client.GetSecretaryAsync(await SessionStorageManager.GetValueAsync<string>(SessionStorageManager.SecretaryId));
                    StateManager.SetLoggedInUserData(isSecretary, user.Result, secretary.Result, department.Result);
                }
                // Else...
                else
                {
                    // Gets the professor from the database
                    var professor = await Client.GetProfessorAsync(await SessionStorageManager.GetValueAsync<string>(SessionStorageManager.ProfessorId));
                    StateManager.SetLoggedInUserData(isSecretary, user.Result, professor.Result, department.Result);
                }
            }
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
        public void SetLoggedInUserData(bool isSecretary, UserResponseModel user, SecretaryResponseModel secretary, DepartmentResponseModel department)
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
        public void SetLoggedInUserData(bool isSecretary, UserResponseModel user, ProfessorResponseModel professor, DepartmentResponseModel department)
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
