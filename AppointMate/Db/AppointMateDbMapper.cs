using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// Global access points for the AppointMate application database 
    /// </summary>
    public static class AppointMateDbMapper
    {
        #region Constants

        /// <summary>
        /// The AppointMate signature used for identifying the AppointMate entities
        /// </summary>
        public const string Signature = "AppointMate_";

        /// <summary>
        /// The companies collection name
        /// </summary>
        public const string CompaniesCollectionName = Signature + "Companies";

        /// <summary>
        /// The company categories collection name
        /// </summary>
        public const string CompanyCategoriesCollectionName = Signature + "CompanyCategories";

        /// <summary>
        /// The company layouts collection name
        /// </summary>
        public const string CompanyLayoutsCollectionName = Signature + "CompanyLayouts";

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

        /// <summary>
        /// The customer notes collection name
        /// </summary>
        public const string CustomerNotesCollectionName = Signature + "CustomerNotes";

        /// <summary>
        /// The customer point offset logs collection name
        /// </summary>
        public const string CustomerCustomerPointOffsetLogsCollectionName = Signature + "CustomerPointOffsetLogs";

        /// <summary>
        /// The payment methods collection name
        /// </summary>
        public const string PaymentMethodsCollectionName = Signature + "PaymentMethods";

        /// <summary>
        /// The users collection name
        /// </summary>
        public const string UsersCollectionName = Signature + "Users";

        /// <summary>
        /// The user favorite companies collection name
        /// </summary>
        public const string UserFavoriteCompaniesCollectionName = Signature + "UserFavoriteCompanies";

        /// <summary>
        /// The services collection name
        /// </summary>
        public const string ServicesCollectionName = Signature + "Services";

        /// <summary>
        /// The services collection name
        /// </summary>
        public const string ServiceCategoriesCollectionName = Signature + "ServiceCategories";

        /// <summary>
        /// The customer services collection name
        /// </summary>
        public const string CustomerServicesCollectionName = Signature + "CustomerServices";

        /// <summary>
        /// The customer service reviews collection name
        /// </summary>
        public const string CustomerServiceReviewsCollectionName = Signature + "CustomerServiceReviews";

        /// <summary>
        /// The customer service sessions collection name
        /// </summary>
        public const string CustomerServiceSessionsCollectionName = Signature + "CustomerServiceSessions";

        /// <summary>
        /// The customer service payments collection name
        /// </summary>
        public const string CustomerServicePaymentsCollectionName = Signature + "CustomerServicePayments";

        /// <summary>
        /// The customer service scheduled payments collection name
        /// </summary>
        public const string CustomerServiceScheduledPaymentsCollectionName = Signature + "CustomerServiceScheduledPayments";

        #endregion

        #region Categories

        /// <summary>
        /// The company categories collection
        /// </summary>
        public static IMongoCollection<CategoryEntity> CompanyCategories => DI.Database.GetCollection<CategoryEntity>(CompanyCategoriesCollectionName);

        /// <summary>
        /// The service categories collection
        /// </summary>
        public static IMongoCollection<CategoryEntity> SerivceCategories => DI.Database.GetCollection<CategoryEntity>(ServiceCategoriesCollectionName);

        #endregion

        #region Companies

        /// <summary>
        /// The companies collection
        /// </summary>
        public static IMongoCollection<CompanyEntity> Companies => DI.Database.GetCollection<CompanyEntity>(CompaniesCollectionName);

        /// <summary>
        /// The company layouts collection
        /// </summary>
        public static IMongoCollection<CompanyLayoutEntity> CompanyLayouts => DI.Database.GetCollection<CompanyLayoutEntity>(CompanyLayoutsCollectionName);

        #endregion

        #region Labels

        /// <summary>
        /// The customer labels collection
        /// </summary>
        public static IMongoCollection<LabelEntity> CustomerLabels => DI.Database.GetCollection<LabelEntity>(CustomerLabelsCollectionName);

        #endregion

        #region Notes

        /// <summary>
        /// The customer notes collection
        /// </summary>
        public static IMongoCollection<CustomerNoteEntity> CustomerNotes => DI.Database.GetCollection<CustomerNoteEntity>(CustomerNotesCollectionName);

        #endregion

        #region Payments

        /// <summary>
        /// The payment methods collection
        /// </summary>
        public static IMongoCollection<PaymentMethodEntity> PaymentMethods => DI.Database.GetCollection<PaymentMethodEntity>(PaymentMethodsCollectionName);

        /// <summary>
        /// The customer service payments collection
        /// </summary>
        public static IMongoCollection<CustomerServicePaymentEntity> CustomerServicePayments => DI.Database.GetCollection<CustomerServicePaymentEntity>(CustomerServicePaymentsCollectionName);

        /// <summary>
        /// The customer service scheduled payments collection
        /// </summary>
        public static IMongoCollection<CustomerServiceScheduledPaymentEntity> CustomerServiceScheduledPayments => DI.Database.GetCollection<CustomerServiceScheduledPaymentEntity>(CustomerServiceScheduledPaymentsCollectionName);

        #endregion

        #region Points

        /// <summary>
        /// The customer point offset logs collection
        /// </summary>
        public static IMongoCollection<CustomerPointOffsetLogEntity> CustomerCustomerPointOffsetLogs => DI.Database.GetCollection<CustomerPointOffsetLogEntity>(CustomerCustomerPointOffsetLogsCollectionName);

        #endregion

        #region Reviews

        /// <summary>
        /// The customer service reviews collection
        /// </summary>
        public static IMongoCollection<CustomerServiceReviewEntity> CustomerServiceReviews => DI.Database.GetCollection<CustomerServiceReviewEntity>(CustomerServiceReviewsCollectionName);

        #endregion

        #region Services

        /// <summary>
        /// The services collection
        /// </summary>
        public static IMongoCollection<ServiceEntity> Services => DI.Database.GetCollection<ServiceEntity>(ServicesCollectionName);

        /// <summary>
        /// The customer services collection
        /// </summary>
        public static IMongoCollection<CustomerServiceEntity> CustomerServices => DI.Database.GetCollection<CustomerServiceEntity>(CustomerServicesCollectionName);

        /// <summary>
        /// The customer service sessions collection
        /// </summary>
        public static IMongoCollection<CustomerServiceSessionEntity> CustomerServiceSessions => DI.Database.GetCollection<CustomerServiceSessionEntity>(CustomerServiceSessionsCollectionName);

        #endregion

        #region Users

        /// <summary>
        /// The users collection
        /// </summary>
        public static IMongoCollection<UserEntity> Users => DI.Database.GetCollection<UserEntity>(UsersCollectionName);

        #endregion

        #region Customers

        /// <summary>
        /// The customers collection
        /// </summary>
        public static IMongoCollection<CustomerEntity> Customers => DI.Database.GetCollection<CustomerEntity>(CustomersCollectionName);

        #endregion

        #region Staff Members

        /// <summary>
        /// The staff members collection
        /// </summary>
        public static IMongoCollection<StaffMemberEntity> StaffMembers => DI.Database.GetCollection<StaffMemberEntity>(StaffMembersCollectionName);

        /// <summary>
        /// The staff members weekly schedules collection
        /// </summary>
        public static IMongoCollection<StaffMemberEntity> StaffMemberWeeklySchedules => DI.Database.GetCollection<StaffMemberEntity>(StaffMemberWeeklySchedulesCollectionName);

        #endregion
    }
}
