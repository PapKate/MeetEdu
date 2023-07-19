using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents a weekly schedule document in the MongoDB
    /// </summary>
    public class WeeklyScheduleEntity : DateEntity, INoteable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="WorkHours"/> property
        /// </summary>
        private IEnumerable<DayOfWeekTimeRange>? mWorkHours;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The staff member id
        /// </summary>
        public ObjectId StaffMemberId { get; set; }

        /// <summary>
        /// The work hours
        /// </summary>
        public IEnumerable<DayOfWeekTimeRange> WorkHours
        {
            get => mWorkHours ?? Enumerable.Empty<DayOfWeekTimeRange>();
            set => mWorkHours = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeeklyScheduleEntity() : base()
        {

        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="WeeklyScheduleEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="staffMemberId">The staff member id</param>
        /// <returns></returns>
        public static WeeklyScheduleEntity FromRequestModel(WeeklyScheduleRequestModel model, ObjectId staffMemberId)
        {
            var entity = new WeeklyScheduleEntity();

            DI.Mapper.Map(model, entity);
            entity.StaffMemberId = staffMemberId;
            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="LabelResponseModel"/> from the current <see cref="WeeklyScheduleEntity"/>
        /// </summary>
        /// <returns></returns>
        public WeeklyScheduleResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<WeeklyScheduleResponseModel>(this);

        #endregion
    }
}
