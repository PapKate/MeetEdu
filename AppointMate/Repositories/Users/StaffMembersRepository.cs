using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing staff members
    /// </summary>
    public class StaffMembersRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="StaffMembersRepository"/>
        /// </summary>
        public static StaffMembersRepository Instance { get; } = new StaffMembersRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StaffMembersRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a staff member
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<StaffMemberEntity> AddStaffMemberAsync(StaffMemberRequestModel model)
            => await AppointMateDbMapper.StaffMembers.AddAsync(StaffMemberEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of staff members 
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<StaffMemberEntity>>> AddStaffMembersAsync(IEnumerable<StaffMemberRequestModel> models)
            => new WebServerFailable<IEnumerable<StaffMemberEntity>>(await AppointMateDbMapper.StaffMembers.AddRangeAsync(models.Select(StaffMemberEntity.FromRequestModel).ToList()));

        /// <summary>
        /// Updates the staff member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<StaffMemberEntity>> UpdateStaffMemberAsync(ObjectId id, StaffMemberRequestModel model)
        {
            var entity = await AppointMateDbMapper.StaffMembers.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.StaffMembers));

            return entity;
        }

        /// <summary>
        /// Deletes the staff member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<StaffMemberEntity>> DeleteStaffMemberAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.StaffMembers.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.StaffMembers));

            await AppointMateDbMapper.StaffMembers.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #region Weekly Schedules

        /// <summary>
        /// Add a weekly schedule
        /// </summary>
        /// <param name="staffMemberId">The staff member id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<WeeklyScheduleEntity>> AddStaffMemberWeeklyScheduleAsync(ObjectId staffMemberId, WeeklyScheduleRequestModel model)
            => await AppointMateDbMapper.StaffMemberWeeklySchedules.AddAsync(WeeklyScheduleEntity.FromRequestModel(model, staffMemberId));

        /// <summary>
        /// Adds a list of weekly schedules
        /// </summary>
        /// <param name="staffMemberId">The staff member id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<WeeklyScheduleEntity>>> AddStaffMemberWeeklySchedulesAsync(ObjectId staffMemberId, IEnumerable<WeeklyScheduleRequestModel> models)
            => new WebServerFailable<IEnumerable<WeeklyScheduleEntity>>(await AppointMateDbMapper.StaffMemberWeeklySchedules.AddRangeAsync(models.Select(x => WeeklyScheduleEntity.FromRequestModel(x, staffMemberId)).ToList()));

        /// <summary>
        /// Updates the weekly schedule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<WeeklyScheduleEntity>> UpdateStaffMemberWeeklyScheduleAsync(ObjectId id, WeeklyScheduleRequestModel model)
        {
            var entity = await AppointMateDbMapper.StaffMemberWeeklySchedules.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.StaffMemberWeeklySchedules));

            return entity;
        }

        /// <summary>
        /// Deletes the weekly schedule with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<WeeklyScheduleEntity>> DeleteStaffMemberWeeklyScheduleAsync(ObjectId id)
                => await AppointMateDbMapper.StaffMemberWeeklySchedules.DeleteAsync(id);

        #endregion

        #region Labels

        /// <summary>
        /// Add a staff member label
        /// </summary>
        /// <param name="staffMemberId">The staff member id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> AddStaffMemberLabelAsync(ObjectId staffMemberId, LabelRequestModel model)
            => await AppointMateDbMapper.StaffMemberLabels.AddAsync(LabelEntity.FromRequestModel(model, staffMemberId));

        /// <summary>
        /// Adds a list of staff member labels
        /// </summary>
        /// <param name="staffMemberId">The staff member id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<LabelEntity>>> AddStaffMemberLabelsAsync(ObjectId staffMemberId, IEnumerable<LabelRequestModel> models)
            => new WebServerFailable<IEnumerable<LabelEntity>>(await AppointMateDbMapper.StaffMemberLabels.AddRangeAsync(models.Select(x => LabelEntity.FromRequestModel(x, staffMemberId)).ToList()));

        /// <summary>
        /// Updates the staff member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateStaffMemberLabelAsync(ObjectId id, LabelRequestModel model)
        {
            var entity = await AppointMateDbMapper.StaffMemberLabels.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.StaffMemberLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the staff member label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteStaffMemberLabelAsync(ObjectId id)
                => await AppointMateDbMapper.StaffMemberLabels.DeleteAsync(id);

        #endregion

        #endregion
    }
}
