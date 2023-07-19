using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AppointMate
{
    /// <summary>
    /// Provides methods for managing customers
    /// </summary>
    public class CustomersRepository
    {
        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="CustomersRepository"/>
        /// </summary>
        public static CustomersRepository Instance { get; } = new CustomersRepository();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomersRepository() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a customer
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<CustomerEntity> AddCustomerAsync(CustomerRequestModel model)
            => await AppointMateDbMapper.Customers.AddAsync(CustomerEntity.FromRequestModel(model));

        /// <summary>
        /// Adds a list of customers
        /// </summary>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CustomerEntity>>> AddCustomersAsync(IEnumerable<CustomerRequestModel> models)
            => new WebServerFailable<IEnumerable<CustomerEntity>>(await AppointMateDbMapper.Customers.AddRangeAsync(models.Select(CustomerEntity.FromRequestModel).ToList()));

        /// <summary>
        /// Updates the customer with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerEntity>> UpdateCustomerAsync(ObjectId id, CustomerRequestModel model)
        {
            var entity = await AppointMateDbMapper.Customers.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Customers));

            return entity;
        }

        /// <summary>
        /// Deletes the company with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The company id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerEntity>> DeleteCompanyAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Customers));

            await AppointMateDbMapper.Customers.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #endregion
    }
}
