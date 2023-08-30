using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Threading;

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
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> AddDepartmentAsync(DepartmentRequestModel model, CancellationToken cancellationToken = default) 
            => await AppointMateDbMapper.Departments.AddAsync(DepartmentEntity.FromRequestModel(model), cancellationToken);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> UpdateDepartmentAsync(ObjectId id, DepartmentRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.Departments.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Departments));

            return entity;
        }

        /// <summary>
        /// Deletes the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The department id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> DeleteDepartmentAsync(ObjectId id, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Departments));

            await AppointMateDbMapper.Departments.DeleteAsync(id, cancellationToken);

            return entity;
        }

        #region Labels

        /// <summary>
        /// Add a department label
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> AddDepartmentLabelAsync(ObjectId departmentId, LabelRequestModel model, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.DepartmentLabels.AddAsync(LabelEntity.FromRequestModel(model, departmentId), cancellationToken);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateDepartmentLabelAsync(ObjectId id, LabelRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.DepartmentLabels.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.DepartmentLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the department label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteDepartmentLabelAsync(ObjectId id, CancellationToken cancellationToken = default)
                => await AppointMateDbMapper.DepartmentLabels.DeleteAsync(id, cancellationToken);

        #endregion

        #region Contact Messages

        /// <summary>
        /// Add a department contact message 
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> AddDepartmentContactMessageAsync(ObjectId departmentId, DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.DepartmentContactMessages.AddAsync(DepartmentContactMessageEntity.FromRequestModel(model, departmentId), cancellationToken);

        /// <summary>
        /// Updates the contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> UpdateDepartmentContactMessageAsync(ObjectId id, DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await AppointMateDbMapper.DepartmentContactMessages.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.DepartmentContactMessages));

            return entity;
        }

        /// <summary>
        /// Deletes the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> DeleteDepartmentContactMessageAsync(ObjectId id, CancellationToken cancellationToken = default)
                => await AppointMateDbMapper.DepartmentContactMessages.DeleteAsync(id, cancellationToken);

        #endregion

        #region Layouts

        /// <summary>
        /// Add a department layout
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutAsync(ObjectId departmentId, DepartmentLayoutRequestModel model, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.DepartmentLayouts.AddAsync(DepartmentLayoutEntity.FromRequestModel(model, departmentId), cancellationToken);

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
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> DeleteDepartmentLayoutAsync(ObjectId layoutId, CancellationToken cancellationToken = default)
            => await AppointMateDbMapper.DepartmentLayouts.DeleteAsync(layoutId, cancellationToken);

        #region Rooms

        /// <summary>
        /// Add a department layout room
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutRoomAsync(ObjectId layoutId, DepartmentLayoutRoomDataModel model, CancellationToken cancellationToken = default)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId, cancellationToken);
            
            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.DepartmentLayouts));
          
            // Add the room to the layout
            layout.Rooms.Add(model);
            
            // Update the layout
            await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout, cancellationToken);

            // Return the layout
            return layout;
        }

        /// <summary>
        /// Add a list of rooms to the department layout 
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutRoomsAsync(ObjectId layoutId, IEnumerable<DepartmentLayoutRoomDataModel> models, CancellationToken cancellationToken = default)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId, cancellationToken);

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
            await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout, cancellationToken);

            // Return the layout
            return layout;
        }

        /// <summary>
        /// Replaces the layout rooms with the specified <paramref name="models"/>
        /// of the department layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> UpdateDepartmentLayoutRoomsAsync(ObjectId layoutId, IEnumerable<DepartmentLayoutRoomDataModel> models, CancellationToken cancellationToken = default)
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
                    await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout, cancellationToken);
                });
        }

        /// <summary>
        /// Deletes the department layout rooms 
        /// from the department layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> DeleteDepartmentLayoutRoomsAsync(ObjectId layoutId, CancellationToken cancellationToken = default)
        {
            return await ExecuteAgainstDepartmentLayoutAsync(
                layoutId,
                async (layout) =>
                {
                    layout.Rooms.Clear();

                    // Update the layout 
                    await AppointMateDbMapper.DepartmentLayouts.UpdateAsync(layout, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        private static async Task<WebServerFailable<DepartmentLayoutEntity>> ExecuteAgainstDepartmentLayoutAsync(ObjectId layoutId, Func<DepartmentLayoutEntity, Task> action, CancellationToken cancellationToken = default)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId, cancellationToken);

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
