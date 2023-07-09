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
        public async Task<WebServerFailable<CompanyEntity>> AddCompanyAsync(CompanyRequestModel model) 
            => await AppointMateDbMapper.Companies.AddAsync(EntityHelpers.FromRequestModel<CompanyEntity>(model));

        /// <summary>
        /// Adds a list of companies 
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CompanyEntity>>> AddCompaniesAsync(IEnumerable<CompanyRequestModel> models) 
            => new WebServerFailable<IEnumerable<CompanyEntity>>(await AppointMateDbMapper.Companies.AddRangeAsync(models.Select(EntityHelpers.FromRequestModel<CompanyEntity>).ToList()));

        /// <summary>
        /// Updates the company with the specified <paramref name="companyId"/>
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyEntity>> UpdateCompanyAsync(ObjectId companyId, CompanyRequestModel model)
        {
            var entity = await AppointMateDbMapper.Companies.UpdateAsync(companyId, model);

            if (entity is null)
                return WebServerFailable.NotFound(companyId, nameof(AppointMateDbMapper.Companies));

            return entity;
        }

    /// <summary>
    /// Deletes the company with the specified <paramref name="companyId"/>
    /// </summary>
    /// <param name="companyId">The company id</param>
    /// <returns></returns>
    public async Task<WebServerFailable<CompanyEntity>> DeleteCompanyAsync(ObjectId companyId) 
            => await AppointMateDbMapper.Companies.DeleteAsync(companyId);

        #region Categories

        /// <summary>
        /// Add a company category
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CategoryEntity>> AddCompanyCategoryAsync(ObjectId companyId, CategoryRequestModel model)
            => await AppointMateDbMapper.CompanyCategories.AddAsync(EntityHelpers.FromRequestModel<CategoryEntity>(model, x => x.ObjectId = companyId));

        /// <summary>
        /// Adds a list of company categories 
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CategoryEntity>>> AddCompanyCategoriesAsync(ObjectId companyId, IEnumerable<CategoryRequestModel> models)
            => new WebServerFailable<IEnumerable<CategoryEntity>>(await AppointMateDbMapper.CompanyCategories.AddRangeAsync(models.Select(x => EntityHelpers.FromRequestModel<CategoryEntity>(x, e => e.ObjectId = companyId)).ToList()));

        /// <summary>
        /// Updates the company with the specified <paramref name="categoryId"/>
        /// </summary>
        /// <param name="categoryId">The company id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CategoryEntity>> UpdateCompanyCategoryAsync(ObjectId categoryId, CategoryRequestModel model)
        {
            var entity = await AppointMateDbMapper.CompanyCategories.UpdateAsync(categoryId, model);

            if (entity is null)
                return WebServerFailable.NotFound(categoryId, nameof(AppointMateDbMapper.CompanyCategories));

            return entity;
        }

    /// <summary>
    /// Deletes the company category with the specified <paramref name="categoryId"/>
    /// </summary>
    /// <param name="categoryId">The category id</param>
    /// <returns></returns>
    public async Task<WebServerFailable<CategoryEntity>> DeleteCompanyCategoryAsync(ObjectId categoryId)
            => await AppointMateDbMapper.CompanyCategories.DeleteAsync(categoryId);

        #endregion

        #region Layouts

        /// <summary>
        /// Add a company layout
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> AddCompanyLayoutAsync(ObjectId companyId, CompanyLayoutRequestModel model)
            => await AppointMateDbMapper.CompanyLayouts.AddAsync(EntityHelpers.FromRequestModel<CompanyLayoutEntity>(model, x => x.CompanyId = companyId));

        /// <summary>
        /// Adds a list of company layouts 
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CompanyLayoutEntity>>> AddCompanyLayoutsAsync(ObjectId companyId, IEnumerable<CompanyLayoutRequestModel> models)
            => new WebServerFailable<IEnumerable<CompanyLayoutEntity>>(await AppointMateDbMapper.CompanyLayouts.AddRangeAsync(models.Select(x => EntityHelpers.FromRequestModel<CompanyLayoutEntity>(x, e => e.CompanyId = companyId)).ToList()));

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
        public async Task<WebServerFailable<CompanyLayoutRoomEntity>> AddCompanyLayoutRoomAsync(ObjectId layoutId, CompanyLayoutRoomRequestModel model)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);
            
            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            // A list for the rooms
            var rooms = new List<CompanyLayoutRoomEntity>();

            // Adds to the list the rooms that exist in the layout
            rooms.AddRange(layout.Rooms);

            // Create the entity with the mapped values from the request model
            var room = DI.Mapper.Map<CompanyLayoutRoomRequestModel, CompanyLayoutRoomEntity>(model);

            // Add the room to the list
            rooms.Add(room);

            // Set as layout rooms the list
            layout.Rooms = rooms;

            // Update the layout
            await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);

            // Return the layout
            return room;
        }

        /// <summary>
        /// Add a list of rooms to the company layout 
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CompanyLayoutRoomEntity>>> AddCompanyLayoutRoomsAsync(ObjectId layoutId, IEnumerable<CompanyLayoutRoomRequestModel> models)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            // A list for the rooms
            var rooms = new List<CompanyLayoutRoomEntity>();

            // Adds to the list the rooms that exist in the layout
            rooms.AddRange(layout.Rooms);

            // Create the entity with the mapped values from the request model
            var newRooms = models.Select(DI.Mapper.Map<CompanyLayoutRoomRequestModel, CompanyLayoutRoomEntity>).ToList();

            // Add the room to the list
            rooms.AddRange(newRooms);

            // Set as layout rooms the list
            layout.Rooms = rooms;

            // Update the layout
            await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);

            // Return the layout
            return new WebServerFailable<IEnumerable<CompanyLayoutRoomEntity>>(layout.Rooms);
        }

        /// <summary>
        /// Add a company layout room
        /// </summary>
        /// <param name="layoutId">The layout id</param>
        /// <param name="roomId">The room id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutRoomEntity>> UpdateCompanyLayoutRoomAsync(ObjectId layoutId, ObjectId roomId, CompanyLayoutRoomRequestModel model)
        {
            // Get the layout with the specified id
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);

            // If the layout does not exist...
            if (layout is null)
                // Return not found
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            // Get the room with the specified id
            var room = layout.Rooms.FirstOrDefault(x => x.Id == roomId);

            // If the room does not exist...
            if (room is null)
                // Return not found
                return WebServerFailable.NotFound(roomId, $"Room in {nameof(AppointMateDbMapper.CompanyLayouts)}");

            // Update the entity with the mapped values from the request model
            room = DI.Mapper.Map<CompanyLayoutRoomRequestModel, CompanyLayoutRoomEntity>(model);

            // Update the layout 
            await RepositoryHelpers.UpdateAsync(AppointMateDbMapper.CompanyLayouts, layout);

            // Return the room
            return room;
        }

        #endregion

        #endregion

        #endregion
    }
}
