using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing departments
    /// </summary>
    public class DepartmentsRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="DepartmentsRepository"/>
        /// </summary>
        public static DepartmentsRepository Instance { get; } = new DepartmentsRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DepartmentsRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a department
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<DepartmentEntity> AddDepartmentAsync(DepartmentRequestModel model) 
            => await AppointMateDbMapper.Departments.AddAsync(DepartmentEntity.FromRequestModel(model));

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> UpdateDepartmentAsync(ObjectId id, DepartmentRequestModel model)
        {
            var entity = await AppointMateDbMapper.Departments.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Departments));

            return entity;
        }

        /// <summary>
        /// Deletes the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The department id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> DeleteDepartmentAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Departments));

            await AppointMateDbMapper.Departments.DeleteAsync(id);

            return entity;
        }

        #region Labels

        /// <summary>
        /// Add a department label
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> AddDepartmentLabelAsync(ObjectId departmentId, LabelRequestModel model)
            => await AppointMateDbMapper.DepartmentLabels.AddAsync(LabelEntity.FromRequestModel(model, departmentId));

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateDepartmentLabelAsync(ObjectId id, LabelRequestModel model)
        {
            var entity = await AppointMateDbMapper.DepartmentLabels.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.DepartmentLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the department label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteDepartmentLabelAsync(ObjectId id)
                => await AppointMateDbMapper.DepartmentLabels.DeleteAsync(id);

        #endregion

        #region Contact Messages

        /// <summary>
        /// Add a department contact message 
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> AddDepartmentContactMessageAsync(ObjectId departmentId, DepartmentContactMessageRequestModel model)
            => await AppointMateDbMapper.DepartmentContactMessages.AddAsync(DepartmentContactMessageEntity.FromRequestModel(model, departmentId));

        /// <summary>
        /// Updates the contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> UpdateDepartmentContactMessageAsync(ObjectId id, DepartmentContactMessageRequestModel model)
        {
            var entity = await AppointMateDbMapper.DepartmentContactMessages.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.DepartmentContactMessages));

            return entity;
        }

        /// <summary>
        /// Deletes the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> DeleteDepartmentContactMessageAsync(ObjectId id)
                => await AppointMateDbMapper.DepartmentContactMessages.DeleteAsync(id);

        #endregion

        #region Layouts

        /// <summary>
        /// Add a department layout
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutAsync(ObjectId departmentId, DepartmentLayoutRequestModel model)
            => await AppointMateDbMapper.DepartmentLayouts.AddAsync(DepartmentLayoutEntity.FromRequestModel(model, departmentId));

        /// <summary>
        /// Updates the layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> UpdateDepartmentLayoutAsync(ObjectId layoutId, DepartmentLayoutRequestModel model, CancellationToken cancellationToken)
        {
            var entity = await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layoutId, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.DepartmentLayouts));

            return entity;
        }

        /// <summary>
        /// Deletes the department layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> DeleteDepartmentLayoutAsync(ObjectId layoutId)
            => await AppointMateDbMapper.DepartmentLayouts.DeleteAsync(layoutId);

        #region Rooms

        /// <summary>
        /// Add a department layout room
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutRoomAsync(ObjectId layoutId, DepartmentLayoutDataModel model)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId);
            
            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.DepartmentLayouts));
          
            // Add the room to the layout
            layout.Rooms.Add(model);
            
            // Update the layout
            await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout);

            // Return the layout
            return layout;
        }

        /// <summary>
        /// Add a list of rooms to the department layout 
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutRoomsAsync(ObjectId layoutId, IEnumerable<DepartmentLayoutDataModel> models)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.DepartmentLayouts));

            // A list for the rooms
            var rooms = layout.Rooms.ToList();

            // Add the rooms to the list
            rooms.AddRange(models);

            // Set as layout rooms the list
            layout.Rooms = rooms;

            // Update the layout
            await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout);

            // Return the layout
            return layout;
        }

        /// <summary>
        /// Replaces the layout rooms with the specified <paramref name="models"/>
        /// of the department layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> UpdateDepartmentLayoutRoomsAsync(ObjectId layoutId, IEnumerable<DepartmentLayoutDataModel> models)
        {
            return await ExecuteAgainstDepartmentLayoutAsync(
                layoutId,
                async (layout) =>
                {
                    // A list for the rooms
                    var rooms = models.ToList();

                    // Set as layout rooms the list
                    layout.Rooms = rooms;

                    // Update the layout 
                    await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout);
                });
        }

        /// <summary>
        /// Deletes the department layout rooms 
        /// from the department layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> DeleteDepartmentLayoutRoomsAsync(ObjectId layoutId)
        {
            return await ExecuteAgainstDepartmentLayoutAsync(
                layoutId,
                async (layout) =>
                {
                    layout.Rooms.Clear();

                    // Update the layout 
                    await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout);
                });
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes the specified <paramref name="action"/> against the layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="action">The action</param>
        /// <returns></returns>
        private static async Task<WebServerFailable<DepartmentLayoutEntity>> ExecuteAgainstDepartmentLayoutAsync(ObjectId layoutId, Func<DepartmentLayoutEntity, Task> action)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.DepartmentLayouts));

            // Execute the action
            await action(layout);

            // Return the layout
            return layout;
        }

        #endregion
    }
}
