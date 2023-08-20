using AutoMapper;
using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a professor document in the MongoDB
    /// </summary>
    public class ProfessorEntity : StaffMemberEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        /// <summary>
        /// The member of the <see cref="ResearchInterests"/> property
        /// </summary>
        private string? mResearchInterests;

        /// <summary>
        /// The member of the <see cref="Websites"/> property
        /// </summary>
        private IEnumerable<Uri>? mWebsites;

        #endregion

        #region Public Properties

        /// <summary>
        /// The personal websites 
        /// </summary>
        public IEnumerable<Uri> Websites 
        { 
            get => mWebsites ?? Enumerable.Empty<Uri>();
            set => mWebsites = value;
        }

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        /// <summary>
        /// The research interests
        /// </summary>
        public string ResearchInterests
        {
            get => mResearchInterests ?? string.Empty;
            set => mResearchInterests = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProfessorEntity() : base()
        {

        }

        #endregion

    }

    /// <summary>
    /// A minimal version of the <see cref="ProfessorEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedProfessorEntity : EmbeddedBaseEntity
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Field"/> property
        /// </summary>
        private string? mField;

        #endregion

        #region Public Properties

        /// <summary>
        /// The field of study
        /// </summary>
        public string Field
        {
            get => mField ?? string.Empty;
            set => mField = value;
        }

        /// <summary>
        /// The staff member
        /// </summary>
        public EmbeddedStaffMemberEntity? StaffMember { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedProfessorEntity() : base()
        {

        }

        #endregion
    }
}
