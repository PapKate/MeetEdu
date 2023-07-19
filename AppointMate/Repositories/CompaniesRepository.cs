using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing companies
    /// </summary>
    public class CompaniesRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="CompaniesRepository"/>
        /// </summary>
        public static CompaniesRepository Instance { get; } = new CompaniesRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompaniesRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a company
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<CompanyEntity> AddCompanyAsync(CompanyRequestModel model) 
            => await AppointMateDbMapper.Companies.AddAsync(CompanyEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of companies 
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CompanyEntity>>> AddCompaniesAsync(IEnumerable<CompanyRequestModel> models) 
            => new WebServerFailable<IEnumerable<CompanyEntity>>(await AppointMateDbMapper.Companies.AddRangeAsync(models.Select(CompanyEntity.FromRequestModel).ToList()));

        /// <summary>
        /// Updates the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyEntity>> UpdateCompanyAsync(ObjectId id, CompanyRequestModel model)
        {
            var entity = await AppointMateDbMapper.Companies.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Companies));

            return entity;
        }

        /// <summary>
        /// Deletes the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The company id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyEntity>> DeleteCompanyAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Companies));

            await AppointMateDbMapper.Companies.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #region Labels

        /// <summary>
        /// Add a company label
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> AddCompanyLabelAsync(ObjectId companyId, LabelRequestModel model)
            => await AppointMateDbMapper.CompanyLabels.AddAsync(LabelEntity.FromRequestModel(model, companyId));

        /// <summary>
        /// Adds a list of company labels
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<LabelEntity>>> AddCompanyLabelsAsync(ObjectId companyId, IEnumerable<LabelRequestModel> models)
            => new WebServerFailable<IEnumerable<LabelEntity>>(await AppointMateDbMapper.CompanyLabels.AddRangeAsync(models.Select(x => LabelEntity.FromRequestModel(x, companyId)).ToList()));

        /// <summary>
        /// Updates the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> UpdateCompanyLabelAsync(ObjectId id, LabelRequestModel model)
        {
            var entity = await AppointMateDbMapper.CompanyLabels.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CompanyLabels));

            return entity;
        }

        /// <summary>
        /// Deletes the company label with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<LabelEntity>> DeleteCompanyLabelAsync(ObjectId id)
                => await AppointMateDbMapper.CompanyLabels.DeleteAsync(id);

        #endregion

        #region Layouts

        /// <summary>
        /// Add a company layout
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> AddCompanyLayoutAsync(ObjectId companyId, CompanyLayoutRequestModel model)
            => await AppointMateDbMapper.CompanyLayouts.AddAsync(CompanyLayoutEntity.FromRequestModel(model, companyId));

        /// <summary>
        /// Adds a list of company layouts 
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CompanyLayoutEntity>>> AddCompanyLayoutsAsync(ObjectId companyId, IEnumerable<CompanyLayoutRequestModel> models)
            => new WebServerFailable<IEnumerable<CompanyLayoutEntity>>(await AppointMateDbMapper.CompanyLayouts.AddRangeAsync(models.Select(x => CompanyLayoutEntity.FromRequestModel(x, companyId)).ToList()));

        /// <summary>
        /// Updates the layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> UpdateCompanyLayoutAsync(ObjectId layoutId, CompanyLayoutRequestModel model)
        {
            var entity = await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layoutId, model);

            if (entity is null)
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            return entity;
        }

        /// <summary>
        /// Deletes the company layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> DeleteCompanyLayoutAsync(ObjectId layoutId)
            => await AppointMateDbMapper.CompanyLayouts.DeleteAsync(layoutId);

        #region Rooms

        /// <summary>
        /// Add a company layout room
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> AddCompanyLayoutRoomAsync(ObjectId layoutId, CompanyLayoutRoomDataModel model)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);
            
            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));
          
            // Add the room to the layout
            layout.Rooms.Add(model);
            
            // Update the layout
            await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);

            // Return the layout
            return layout;
        }

        /// <summary>
        /// Add a list of rooms to the company layout 
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> AddCompanyLayoutRoomsAsync(ObjectId layoutId, IEnumerable<CompanyLayoutRoomDataModel> models)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            // A list for the rooms
            var rooms = layout.Rooms.ToList();

            // Add the rooms to the list
            rooms.AddRange(models);

            // Set as layout rooms the list
            layout.Rooms = rooms;

            // Update the layout
            await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);

            // Return the layout
            return layout;
        }

        /// <summary>
        /// Replaces the layout rooms with the specified <paramref name="models"/>
        /// of the company layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> UpdateCompanyLayoutRoomsAsync(ObjectId layoutId, IEnumerable<CompanyLayoutRoomDataModel> models)
        {
            return await ExecuteAgainstCompanyLayoutAsync(
                layoutId,
                async (layout) =>
                {
                    // A list for the rooms
                    var rooms = models.ToList();

                    // Set as layout rooms the list
                    layout.Rooms = rooms;

                    // Update the layout 
                    await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);
                });
        }

        /// <summary>
        /// Deletes the company layout rooms 
        /// from the company layout with the specified <paramref name="layoutId"/>
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> DeleteCompanyLayoutRoomsAsync(ObjectId layoutId)
        {
            return await ExecuteAgainstCompanyLayoutAsync(
                layoutId,
                async (layout) =>
                {
                    layout.Rooms.Clear();

                    // Update the layout 
                    await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);
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
        private static async Task<WebServerFailable<CompanyLayoutEntity>> ExecuteAgainstCompanyLayoutAsync(ObjectId layoutId, Func<CompanyLayoutEntity, Task> action)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            // Execute the action
            await action(layout);

            // Return the layout
            return layout;
        }

        #endregion
    }
}
