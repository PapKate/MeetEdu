namespace MeetEdu
{
    /// <summary>
    /// Web server constants related to the MeetEdu application
    /// </summary>
    public static class MeetEduWebServerConstants
    {
        #region Public Properties

        /// <summary>
        /// Error message indicating no companies were found with the specified ids
        /// </summary>
        public const string NoCompaniesWereFoundWithTheSpecifiedIdsErrorMessage = "No companies were found with the specified ids.";

        /// <summary>
        /// Error message indicating invalid registration credentials
        /// </summary>
        public const string NoSessionWasCreatedErrorMessage = "No session was created. Please add a session and try again.";

        /// <summary>
        /// Error message indicating an invalid username or an invalid password
        /// </summary>
        public const string InvalidLogInCredentialsErrorMessage = "Invalid username or password!";

        /// <summary>
        /// Error message indicating invalid registration credentials
        /// </summary>
        public const string InvalidRegistrationCredentialsErrorMessage = "Please provide all required details to register for an account!";

        /// <summary>
        /// Error message indicating insufficient customer points
        /// </summary>
        public const string InsufficientCustomerPointsErrorMessage = "Insufficient customer points";

        /// <summary>
        /// Error message indicating no customer id was specified
        /// </summary>
        public const string NoCustomerIdSpecifiedInTheRequestErrorMessage = "No customer id was specified in the request";

        /// <summary>
        /// Error message indicating no appointment template id was specified
        /// </summary>
        public const string NoAppointmentTemplateIdSpecifiedInTheRequestErrorMessage = "No appointment template id was specified in the request";

        /// <summary>
        /// Error message indicating no service id was specified
        /// </summary>
        public const string NoServiceIdSpecifiedInTheRequestErrorMessage = "No service id was specified in the request";

        /// <summary>
        /// Error message indicating that the purchased amount of a customer service can not be set to less than the paid amount
        /// </summary>
        public const string TheCustomerServicePurchasedAmountCanNotBeSetToLessThanThePaidAmountErrorMessage = "The customer service purchased amount can not be set to less than the paid amount!";

        /// <summary>
        /// Error message indicating that the purchased amount of a customer service can not be set to less than the amount of the scheduled payments
        /// </summary>
        public const string TheCustomerServicePurchasedAmountCanNotBeSetToLessThanTheAmountOfTheScheduledPaymentsErrorMessage = "The customer service purchased amount can not be set to less than the amount of the scheduled payments!";

        #endregion
    }
}
