using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using SharpCompress.Common;

using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace AppointMate
{
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
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CategoryEntity>> AddCompanyCategoryAsync(ObjectId companyId, CategoryRequestModel model)
            => await AppointMateDbMapper.CompanyCategories.AddAsync(EntityHelpers.FromRequestModel<CategoryEntity>(model, x => x.ObjectId = companyId));

        /// <summary>
        /// Adds a list of company categories 
        /// </summary>
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
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> AddCompanyLayoutAsync(ObjectId companyId, CompanyLayoutRequestModel model)
            => await AppointMateDbMapper.CompanyLayouts.AddAsync(EntityHelpers.FromRequestModel<CompanyLayoutEntity>(model, x => x.CompanyId = companyId));

        /// <summary>
        /// Adds a list of company layouts 
        /// </summary>
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
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyLayoutEntity>> AddCompanyLayoutRoomAsync(ObjectId layoutId, CompanyLayoutRoomRequestModel model)
        {
            var layout = await AppointMateDbMapper.CompanyLayouts.FirstOrDefaultAsync(layoutId);
            
            if (layout is null)
                return WebServerFailable.NotFound(layoutId, nameof(AppointMateDbMapper.CompanyLayouts));

            var rooms = new List<CompanyLayoutRoomEntity>();

            rooms.AddRange(layout.Rooms);

            var room = DI.Mapper.Map<CompanyLayoutRoomRequestModel, CompanyLayoutRoomEntity>(model);

            rooms.Add(room);

            layout.Rooms = rooms;

            await AppointMateDbMapper.CompanyLayouts.UpdateAsync(layout);

            return layout;
        }

        #endregion

        #endregion

        #endregion
    }
}
