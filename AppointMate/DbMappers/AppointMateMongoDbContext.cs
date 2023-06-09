namespace AppointMate
{
    /// <summary>
    /// A <see cref="MongoDbContext{TMongoDbContext}"/> used for handling the database requests related to AppointMate
    /// </summary>
    public static class AppointMateMongoDbContext
    {
        #region Constants

        /// <summary>
        /// The AppointMate signature used for identifying the AppointMate entities
        /// </summary>
        public const string Signature = "AppointMate_";

        /// <summary>
        /// The categories collection name
        /// </summary>
        public const string CategoriesCollectionName = Signature + "Categories";

        /// <summary>
        /// The companies collection name
        /// </summary>
        public const string CompaniesCollectionName = Signature + "Companies";

        /// <summary>
        /// The company categories collection name
        /// </summary>
        public const string CompanyCategoriesCollectionName = Signature + "CompanyCategories";

        /// <summary>
        /// The staff members collection name
        /// </summary>
        public const string StaffMembersCollectionName = Signature + "StaffMembers";

        /// <summary>
        /// The staff member weekly schedules collection name
        /// </summary>
        public const string StaffMemberWeeklySchedulesCollectionName = Signature + "StaffMemberWeeklySchedules";

        /// <summary>
        /// The customer labels collection name
        /// </summary>
        public const string CustomerLabelsCollectionName = Signature + "CustomerLabels";

        /// <summary>
        /// The customers collection name
        /// </summary>
        public const string CustomersCollectionName = Signature + "Customers";

        #endregion
    }
}
