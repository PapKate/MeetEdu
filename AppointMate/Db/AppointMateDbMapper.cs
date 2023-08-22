using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// Global access points for the MeetEdu application database 
    /// </summary>
    public static class AppointMateDbMapper
    {
        #region Constants

        /// <summary>
        /// The AppointMate signature used for identifying the AppointMate entities
        /// </summary>
        public const string Signature = "MeetEdu_";

        /// <summary>
        /// The universities collection name
        /// </summary>
        public const string UniversitiesCollectionName = Signature + "Universities";

        /// <summary>
        /// The university labels collection name
        /// </summary>
        public const string UniversityLabelsCollectionName = Signature + "UniversityLabels";

        /// <summary>
        /// The departments collection name
        /// </summary>
        public const string DepartmentsCollectionName = Signature + "Departments";

        /// <summary>
        /// The department labels collection name
        /// </summary>
        public const string DepartmentLabelsCollectionName = Signature + "DepartmentLabels";

        /// <summary>
        /// The department contact collection name
        /// </summary>
        public const string DepartmentContactMessagesCollectionName = Signature + "DepartmentContactMessages";

        /// <summary>
        /// The department layouts collection name
        /// </summary>
        public const string DepartmentLayoutsCollectionName = Signature + "DepartmentLayouts";

        /// <summary>
        /// The appointment templates collection name
        /// </summary>
        public const string AppointmentRulesCollectionName = Signature + "AppointmentTemplates";

        /// <summary>
        /// The appointments collection name
        /// </summary>
        public const string AppointmentsCollectionName = Signature + "Appointments";

        /// <summary>
        /// The users collection name
        /// </summary>
        public const string UsersCollectionName = Signature + "Users";

        /// <summary>
        /// The professors collection name
        /// </summary>
        public const string ProfessorsCollectionName = Signature + "Professors";

        /// <summary>
        /// The secretaries collection name
        /// </summary>
        public const string SecretariesCollectionName = Signature + "Secretaries";

        /// <summary>
        /// The members collection name
        /// </summary>
        public const string MembersCollectionName = Signature + "Members";

        /// <summary>
        /// The member saved departments collection name
        /// </summary>
        public const string MemberSavedDepartmentsCollectionName = Signature + "MemberSavedDepartments";

        /// <summary>
        /// The member saved professors collection name
        /// </summary>
        public const string MemberSavedProfessorsCollectionName = Signature + "MemberSavedProfessors";

        #endregion

        #region Universities

        /// <summary>
        /// The universities collection
        /// </summary>
        public static IMongoCollection<UniversityEntity> Universities => DI.Database.GetCollection<UniversityEntity>(UniversitiesCollectionName);

        /// <summary>
        /// The university labels collection
        /// </summary>
        public static IMongoCollection<LabelEntity> UniversityLabels => DI.Database.GetCollection<LabelEntity>(UniversityLabelsCollectionName);

        #endregion

        #region Departments

        /// <summary>
        /// The departments collection
        /// </summary>
        public static IMongoCollection<DepartmentEntity> Departments => DI.Database.GetCollection<DepartmentEntity>(DepartmentsCollectionName);

        /// <summary>
        /// The department labels collection
        /// </summary>
        public static IMongoCollection<LabelEntity> DepartmentLabels => DI.Database.GetCollection<LabelEntity>(DepartmentLabelsCollectionName);

        /// <summary>
        /// The department layouts collection
        /// </summary>
        public static IMongoCollection<DepartmentLayoutEntity> DepartmentLayouts => DI.Database.GetCollection<DepartmentLayoutEntity>(DepartmentLayoutsCollectionName);

        /// <summary>
        /// The department contact messages collection
        /// </summary>
        public static IMongoCollection<DepartmentContactMessageEntity> DepartmentContactMessages => DI.Database.GetCollection<DepartmentContactMessageEntity>(DepartmentContactMessagesCollectionName);

        #endregion

        #region Appointments

        /// <summary>
        /// The appointment templates collection
        /// </summary>
        public static IMongoCollection<AppointmentRuleEntity> AppointmentRules => DI.Database.GetCollection<AppointmentRuleEntity>(AppointmentRulesCollectionName);

        /// <summary>
        /// The appointments collection
        /// </summary>
        public static IMongoCollection<AppointmentEntity> Appointments => DI.Database.GetCollection<AppointmentEntity>(AppointmentsCollectionName);

        #endregion

        #region Users

        /// <summary>
        /// The users collection
        /// </summary>
        public static IMongoCollection<UserEntity> Users => DI.Database.GetCollection<UserEntity>(UsersCollectionName);
        
        #endregion

        #region Members

        /// <summary>
        /// The members collection
        /// </summary>
        public static IMongoCollection<MemberEntity> Members => DI.Database.GetCollection<MemberEntity>(MembersCollectionName);

        /// <summary>
        /// The member saved departments collection
        /// </summary>
        public static IMongoCollection<MemberSavedDepartmentEntity> MemberSavedDepartments => DI.Database.GetCollection<MemberSavedDepartmentEntity>(MemberSavedDepartmentsCollectionName);

        /// <summary>
        /// The member saved departments collection
        /// </summary>
        public static IMongoCollection<MemberSavedProfessorEntity> MemberSavedProfessors => DI.Database.GetCollection<MemberSavedProfessorEntity>(MemberSavedProfessorsCollectionName);

        #endregion

        #region Professors

        /// <summary>
        /// The professors collection
        /// </summary>
        public static IMongoCollection<ProfessorEntity> Professors => DI.Database.GetCollection<ProfessorEntity>(ProfessorsCollectionName);

        #endregion

        #region Secretaries

        /// <summary>
        /// The secretaries collection
        /// </summary>
        public static IMongoCollection<SecretaryEntity> Secretaries => DI.Database.GetCollection<SecretaryEntity>(SecretariesCollectionName);

        #endregion
    }
}
