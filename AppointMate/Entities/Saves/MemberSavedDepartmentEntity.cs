using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// Only entity
    /// </summary>
    public class MemberSavedDepartmentEntity : DateEntity, IDepartmentIdentifiable<ObjectId>, IMemberIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The company id
        /// </summary>
        public ObjectId DepartmentId { get; set; }

        /// <summary>
        /// The member id
        /// </summary>
        public ObjectId MemberId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MemberSavedDepartmentEntity() : base()
        {

        }

        #endregion
    }
}
