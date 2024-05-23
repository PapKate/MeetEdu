using Microsoft.AspNetCore.SignalR;

using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountsHub : Hub
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public ConnectionsManager ConnectionsManager { get; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountsHub(ConnectionsManager connectionsManager) : base()
        {
            ConnectionsManager = connectionsManager ?? throw new ArgumentNullException(nameof(connectionsManager));
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            // If no HTTP context is provided...
            if(httpContext is null)
                // Return
                return Task.CompletedTask;
            
            httpContext.TryGetProfessorId(out var professorId);
            httpContext.TryGetSecretaryId(out var secretaryId);

            ConnectionsManager.AddConnection(new ActiveConnectionInfo(httpContext.GetUserId().ToObjectId(), Context.ConnectionId)
            {
                ProfessorId = professorId?.ToObjectId(),
                SecretaryId = secretaryId?.ToObjectId()
            });

            return base.OnConnectedAsync();
        }

        /// <inheritdoc/>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectionsManager.RemoveConnection(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        #endregion
    }

    /// <summary>
    /// Provides methods for handling users connections
    /// </summary>
    public class ConnectionsManager
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Connections"/> property
        /// </summary>
        private readonly List<ActiveConnectionInfo> mConnections = new();

        #endregion
        
        #region Public Properties

        /// <summary>
        /// The active connections
        /// </summary>
        public IEnumerable<ActiveConnectionInfo> Connections => mConnections;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConnectionsManager() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the <paramref name="userConnection"/>
        /// </summary>
        /// <param name="userConnection">The connection</param>
        public void AddConnection(ActiveConnectionInfo userConnection)
        {
            lock (mConnections)
            {
                mConnections.Add(userConnection);
            }
        }

        /// <summary>
        /// Removes the connection with the specified <paramref name="connectionId"/>
        /// </summary>
        /// <param name="connectionId">The unique connection id</param>
        /// <returns></returns>
        public ActiveConnectionInfo? RemoveConnection(string connectionId)
        {
            lock (mConnections)
            {
                // Get the connection
                var connection = mConnections.FirstOrDefault(x => x.ConnectionId == connectionId);

                // If there is a connection...
                if (connection != null)
                    // Remove the connection
                    mConnections.Remove(connection);

                return connection;
            }
        }

        /// <summary>
        /// Returns the connections that belong to the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns></returns>
        public IEnumerable<ActiveConnectionInfo> GetUserConnections(ObjectId userId)
            => mConnections.Where(x => x.UserId == userId).ToList();

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class ActiveConnectionInfo : IReadOnlyDateCreatable
    {
        #region Public Properties

        /// <summary>
        /// The user id
        /// </summary>
        public ObjectId UserId { get; }

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId? ProfessorId { get; init; }

        /// <summary>
        /// The secretary id
        /// </summary>
        public ObjectId? SecretaryId { get; init; }

        /// <summary>
        /// The connection id
        /// </summary>
        public string ConnectionId { get; }

        /// <inheritdoc/>
        public DateTimeOffset DateCreated { get; } = DateTimeOffset.Now;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="connectionId"></param>
        public ActiveConnectionInfo(ObjectId userId, string connectionId)
        {
            UserId = userId;
            ConnectionId = connectionId ?? throw new ArgumentNullException(nameof(connectionId));
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class AccountsHubClient
    {
        #region Public Properties

        /// <summary>
        /// The context
        /// </summary>
        public IHubContext<AccountsHub> Context { get; }

        /// <summary>
        /// The connections manager
        /// </summary>
        public ConnectionsManager ConnectionsManager { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountsHubClient(IHubContext<AccountsHub> context, ConnectionsManager connectionsManager) : base()
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ConnectionsManager = connectionsManager ?? throw new ArgumentNullException(nameof(connectionsManager));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SendAppointmentsCreatedAsync(IEnumerable<AppointmentResponseModel> appointments)
        {
            foreach (var group in appointments.GroupBy(x => x.ProfessorId))
            {
                foreach(var connection in ConnectionsManager.GetUserConnections(group.Key.ToObjectId()))
                {
                    await Context.Clients.Client(connection.ConnectionId.ToString()).SendAsync(HubConstants.AppointmentsCreatedMethodName, group.ToList());
                }
            }
        }

        #endregion
    }

}
