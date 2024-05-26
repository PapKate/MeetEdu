namespace MeetBase.Web
{
    /// <summary>
    /// Represents a department contact message 
    /// </summary>
    public class DepartmentContactMessageResponseModel : BaseContactResponseModel, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mDepartmentId;

        /// <summary>
        /// The member of the <see cref="SecretaryId"/> property
        /// </summary>
        private string? mSecretaryId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
        }

        /// <summary>
        /// The secretary id
        /// </summary>
        public string SecretaryId
        {
            get => mSecretaryId ?? string.Empty;
            set => mSecretaryId = value;
        }

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        /// <summary>
        /// The reply to the <see cref="BaseContactResponseModel.Message"/>
        /// </summary>
        public string? Reply {  get; set; }

        /// <summary>
        /// The secretary that replied
        /// </summary>
        public EmbeddedSecretaryResponseModel? Secretary { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentContactMessageResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="DepartmentContactMessageResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedDepartmentContactMessageResponseModel : EmbeddedBaseContactResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mDepartmentId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The department id
        /// </summary>
        public string DepartmentId
        {
            get => mDepartmentId ?? string.Empty;
            set => mDepartmentId = value;
        }

        /// <summary>
        /// The role
        /// </summary>
        public SecretaryRole Role { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedDepartmentContactMessageResponseModel() : base()
        {

        }

        #endregion
    }

}
