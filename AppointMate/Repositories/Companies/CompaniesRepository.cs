using MongoDB.Driver;

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

        public async Task<WebServerFailable<CompanyEntity>> AddCompanyAsync(CompanyRequestModel model)
        {

        }

        #endregion
    }
}
