using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Represents a label
    /// </summary>
    public class LabelResponseModel : StandardResponseModel, IDescriptable, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mCompanyId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string DepartmentId
        {
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelResponseModel()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="LabelResponseModel"/> used when embedding
    /// </summary>
    public class EmbeddedLabelResponseModel : EmbeddedStandardResponseModel, IDepartmentIdentifiable<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="DepartmentId"/> property
        /// </summary>
        private string? mCompanyId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public string DepartmentId
        {
            get => mCompanyId ?? string.Empty;
            set => mCompanyId = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedLabelResponseModel() : base()
        {

        }

        #endregion
    }
}
