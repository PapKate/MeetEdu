namespace MeetBase.Web
{
    /// <summary>
    /// Represents a professor contact message document in the MongoDB
    /// </summary>
    public class ProfessorContactMessageResponseModel : BaseContactResponseModel, IProfessorIdentifiable<string?>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string? ProfessorId { get; set; }

        /// <summary>
        /// The professor
        /// </summary>
        public EmbeddedProfessorResponseModel? Professor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorContactMessageResponseModel() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="ProfessorContactMessageResponseModel"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedProfessorContactMessageResponseModel : EmbeddedBaseContactResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public string? ProfessorId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedProfessorContactMessageResponseModel() : base()
        {

        }

        #endregion
    }
}
