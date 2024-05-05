using Microsoft.AspNetCore.Components.Forms;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.IO;
using System.Net;
using System.Threading;

namespace MeetEdu
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
            => await MeetEduDbMapper.Departments.AddAsync(await DepartmentEntity.FromRequestModelAsync(model), cancellationToken);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> UpdateDepartmentAsync(ObjectId id, DepartmentRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.Departments.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Departments));

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
            var entity = await MeetEduDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.Departments));

            await MeetEduDbMapper.Departments.DeleteAsync(id, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Sets the image of the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="file">The file</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentEntity>> SetDepartmentImageAsync(ObjectId id, IFormFile file, CancellationToken cancellationToken = default)
        {
            return await RepositoryHelpers.SetImageAsync<DepartmentRequestModel, DepartmentEntity>(
                                                id,
                                                $"{MeetEduConstants.Departments.ToLower()}/",
                                                file,
                                                (model) => UpdateDepartmentAsync(id, model, cancellationToken),
                                                cancellationToken);
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
            => await MeetEduDbMapper.DepartmentLabels.AddAsync(LabelEntity.FromRequestModel(model, departmentId), cancellationToken);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateDepartmentLabelAsync(ObjectId id, LabelRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.DepartmentLabels.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.DepartmentLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the department label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteDepartmentLabelAsync(ObjectId id, CancellationToken cancellationToken = default)
                => await MeetEduDbMapper.DepartmentLabels.DeleteAsync(id, cancellationToken);

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
            => await MeetEduDbMapper.DepartmentContactMessages.AddAsync(DepartmentContactMessageEntity.FromRequestModel(model, departmentId), cancellationToken);

        /// <summary>
        /// Updates the contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> UpdateDepartmentContactMessageAsync(ObjectId id, DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
        {
            var entity = await MeetEduDbMapper.DepartmentContactMessages.UpdateAsync(id, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(MeetEduDbMapper.DepartmentContactMessages));

            return entity;
        }

        /// <summary>
        /// Deletes the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentContactMessageEntity>> DeleteDepartmentContactMessageAsync(ObjectId id, CancellationToken cancellationToken = default)
                => await MeetEduDbMapper.DepartmentContactMessages.DeleteAsync(id, cancellationToken);

        #endregion

        #region Layouts

        /// <summary>
        /// Sets a department layout image
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="file">The file</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> SetDepartmentLayoutImageAsync(ObjectId layoutId, IFormFile file, CancellationToken cancellationToken = default)
        {
            return await RepositoryHelpers.SetImageAsync<DepartmentLayoutRequestModel, DepartmentLayoutEntity>(
                                                layoutId, 
                                                $"{MeetEduConstants.Departments.ToLower()}/{MeetEduConstants.Layouts.ToLower()}/",
                                                file,
                                                (model) => UpdateDepartmentLayoutAsync(layoutId, model, cancellationToken),
                                                cancellationToken);
        }

        /// <summary>
        /// Add a department layout
        /// </summary>
        /// <param name="layoutId">The department id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> AddDepartmentLayoutAsync(ObjectId layoutId, DepartmentLayoutRequestModel model, CancellationToken cancellationToken = default) 
            => await MeetEduDbMapper.DepartmentLayouts.AddAsync(DepartmentLayoutEntity.FromRequestModel(model, layoutId), cancellationToken);

        /// <summary>
        /// Updates the layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> UpdateDepartmentLayoutAsync(ObjectId layoutId, DepartmentLayoutRequestModel model, CancellationToken cancellationToken)
        {
            var entity = await MeetEduDbMapper.DepartmentLayouts.UpdateAsync(layoutId, model, cancellationToken);

            if (entity is null)
                return WebServerFailable.NotFound(layoutId, nameof(MeetEduDbMapper.DepartmentLayouts));

            return entity;
        }

        /// <summary>
        /// Deletes the department layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<WebServerFailable<DepartmentLayoutEntity>> DeleteDepartmentLayoutAsync(ObjectId layoutId, CancellationToken cancellationToken = default)
            => await MeetEduDbMapper.DepartmentLayouts.DeleteAsync(layoutId, cancellationToken);

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
            var layout = await MeetEduDbMapper.DepartmentLayouts.FirstOrDefaultAsync(layoutId, cancellationToken);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(MeetEduDbMapper.DepartmentLayouts));

            // Execute the action
            await action(layout);

            // Return the layout
            return layout;
        }

        #endregion
    }
}
