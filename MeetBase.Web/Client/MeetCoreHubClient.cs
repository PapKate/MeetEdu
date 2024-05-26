using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace MeetBase.Web
{
    /// <summary>
    /// Provides events used for communication between the Client and the Server
    /// </summary>
    public class MeetCoreHubClient
    {
        #region Private Member

        private HubConnection mConnection;

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating whether the hub is connected or not
        /// </summary>
        public bool IsConnected { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetCoreHubClient(MeetCoreClient client) : base()
        {
            mConnection = CreateConnection(client);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the existing connection and opens a new one using the currently connected user
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync(MeetCoreClient client)
        {
            await mConnection.StopAsync();
            IsConnected = false;

            await mConnection.DisposeAsync();
            await Closed(null);

            mConnection = CreateConnection(client);
            await mConnection.StartAsync();
            IsConnected = true;
        }

        /// <summary>
        /// Closes the existing connection
        /// </summary>
        /// <returns></returns>
        public async Task DisposeAsync()
        {
            IsConnected = false;
            await mConnection.StopAsync();
            await mConnection.DisposeAsync();
        }

        /// <summary>
        /// Starts a connection to the server
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
            await mConnection.StartAsync();
            IsConnected = true;
        }

        /// <summary>
        /// Closes a connection to the server
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectAsync()
        {
            await mConnection.StartAsync();
        }

        #endregion

        #region Private Methods

        private HubConnection CreateConnection(MeetCoreClient client)
        {
            var connection = new HubConnectionBuilder()
                .AddNewtonsoftJsonProtocol(options => NewtonsoftHelpers.ConfigureSerializer(options.PayloadSerializerSettings))
                .WithUrl("https://localhost:44307" + HubConstants.Route, opts =>
                {
                    opts.AccessTokenProvider = () => Task.FromResult(client.Token);
                })
                .WithNeverEndingAutomaticReconnectAttempts()
                .Build();

            connection.On<IEnumerable<AppointmentResponseModel>>(HubConstants.AppointmentsCreatedMethodName, models =>
            {
                AppointmentsCreated(this, models);
            });

            connection.On<IEnumerable<DepartmentContactMessageResponseModel>>(HubConstants.MessagesCreatedMethodName, models =>
            {
                MessagesCreated(this, models);
            });

            connection.Closed += async (x) => await Closed(x);
            connection.Reconnected += async (x) => await Reconnected(x);
            connection.Reconnecting += async (x) => await Reconnecting(x);

            return connection;
        }

        #endregion

        #region Public Events

        /// <inheritdoc cref="HubConnection.Closed"/>
        public event Func<Exception?, Task> Closed = _ => Task.CompletedTask;

        /// <inheritdoc cref="HubConnection.Reconnecting"/>
        public event Func<Exception?, Task> Reconnecting = _ => Task.CompletedTask;

        /// <inheritdoc cref="HubConnection.Reconnected"/>
        public event Func<string?, Task> Reconnected = _ => Task.CompletedTask;

        /// <summary>
        /// Event that fires every time appointments are created
        /// </summary>
        public event EventHandler<IEnumerable<AppointmentResponseModel>> AppointmentsCreated = (sender, e) => { };

        /// <summary>
        /// Event that fires every time messages are created
        /// </summary>
        public event EventHandler<IEnumerable<DepartmentContactMessageResponseModel>> MessagesCreated = (sender, e) => { };

        #endregion

    }

    /// <summary>
    /// Extension methods for the <see cref="IHubConnectionBuilder"/>
    /// </summary>
    public static class IHubConnectionBuilderExtensions
    {
        /// <summary>
        /// Configures the <see cref="HubConnection"/> to automatically
        /// attempt to never stop trying to reconnect if the connection 
        /// is lost.
        /// </summary>
        /// <param name="hubConnectionBuilder">The builder</param>
        /// <returns></returns>
        public static IHubConnectionBuilder WithNeverEndingAutomaticReconnectAttempts(this IHubConnectionBuilder hubConnectionBuilder)
            => hubConnectionBuilder.WithAutomaticReconnect(NeverEndingReconnectRetryPolicy.Instance);
    }

    /// <summary>
    /// An implementation of the <see cref="IRetryPolicy"/> that never stops trying to connect to the SignalR hub
    /// </summary>
    public class NeverEndingReconnectRetryPolicy : IRetryPolicy
    {
        #region Constants

        /// <summary>
        /// The retry time span
        /// </summary>
        public static readonly TimeSpan RetryTimeSpan = new TimeSpan(0, 0, 5);

        #endregion

        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="NeverEndingReconnectRetryPolicy"/>
        /// </summary>
        public static NeverEndingReconnectRetryPolicy Instance { get; } = new NeverEndingReconnectRetryPolicy();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected NeverEndingReconnectRetryPolicy() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public TimeSpan? NextRetryDelay(RetryContext retryContext) => RetryTimeSpan;

        #endregion
    }
}
