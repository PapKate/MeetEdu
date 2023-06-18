using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
            var company = await AppointMateDbMapper.Companies.FirstOrDefaultAsync(x => x.Id == companyId);

            if(company is null)
                return WebServerFailable.NotFound(companyId, nameof(AppointMateDbMapper.Companies));

            DI.Mapper.Map(model, company);
            return await AppointMateDbMapper.Companies.UpdateAsync(company);
        }

        /// <summary>
        /// Deletes the company with the specified <paramref name="companyId"/>
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CompanyEntity>> DeleteCompanyAsync(ObjectId companyId) 
            => await AppointMateDbMapper.Companies.DeleteAsync(companyId);

        #endregion
    }
}
