using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Only entity
    /// </summary>
    public class MemberSavedProfessorEntity : DateEntity, IProfessorIdentifiable<ObjectId>, IMemberIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// The member id
        /// </summary>
        public ObjectId MemberId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemberSavedProfessorEntity() : base()
        {

        }

        #endregion
    }
}
