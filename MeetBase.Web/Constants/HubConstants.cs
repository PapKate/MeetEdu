namespace MeetBase.Web
{
    /// <summary>
    /// The Hub method names
    /// </summary>
    public static class HubConstants
    {
        /// <summary>
        /// The SignlR URL 
        /// </summary>
        public const string Route = "/hubs/accounts";

        /// <summary>
        /// The name of the event that fires when appointments are created
        /// </summary>
        public const string AppointmentsCreatedMethodName = "AppointmentsCreated";

        /// <summary>
        /// The name of the event that fires when messages are created
        /// </summary>
        public const string MessagesCreatedMethodName = "MessagesCreated";
    }
}
