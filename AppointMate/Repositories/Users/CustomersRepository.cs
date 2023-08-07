using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using SharpCompress.Common;

using System.ComponentModel.Design;

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
        public async Task<WebServerFailable<CustomerEntity>> AddCustomerAsync(CustomerRequestModel model)
        {
            // Gets the user with the specified id
            var user = await AppointMateDbMapper.Users.FirstOrDefaultAsync(x => x.Id.ToString() == model.UserId);

            if (user is null)
                return AppointMateWebServerConstants.InvalidRegistrationCredentialsErrorMessage;

            return await AppointMateDbMapper.Customers.AddAsync(CustomerEntity.FromRequestModel(model));
        }

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
        /// Deletes the customer with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The customer id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerEntity>> DeleteCustomerAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == id);

            // If the customer does not exist...
            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.Customers));

            // Gets the customer services
            var services = await AppointMateDbMapper.CustomerServices.SelectAsync(x => x.CustomerId == id && x.DateStart < DateTimeOffset.Now);

            // If the customer has past services...
            if (services is not null)
                return new WebServerFailable<CustomerEntity>(entity).ToUnsuccessfulWebServerFailable("The user cannot be removed from the customers list because they have an existing service.");

            // Deletes the customer
            await AppointMateDbMapper.Customers.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #region Point Offset Logs

        /// <summary>
        /// Adds a point offset log to the customer with the specified <paramref name="customerId"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<UserPointOffsetLogEntity>> AddCustomerPointOffsetLogAsync(ObjectId customerId, CustomerPointOffsetLogRequestModel model)
        {
            // Get the customer with the specified id
            var customer = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            // If the customer does not exist...
            if (customer is null)
                return WebServerFailable.NotFound(customerId, nameof(AppointMateDbMapper.Customers));

            // Gets the previous point offset for the specified customer
            var previousPointOffsets = await AppointMateDbMapper.CustomerPointOffsetLogs.SelectAsync(x => x.Customer == customer.ToEmbeddedEntity());

            // Gets the last point offset that was created
            var previousPointOffset = previousPointOffsets.OrderByDescending(x => x.DateCreated).FirstOrDefault();

            // Sets the old points to the previous new points if exist, else to 0
            var oldPoints = previousPointOffset?.NewPoints ?? 0;

            // If the addition of the old points with the offset is negative...
            if (oldPoints + model.Offset < 0)
                return AppointMateWebServerConstants.InsufficientCustomerPointsErrorMessage;

            var entity = UserPointOffsetLogEntity.FromRequestModel(customer, model);

            entity.OldPoints = oldPoints;
            entity.NewPoints = (uint)(oldPoints + model.Offset);

            // Returns the entity
            return entity;
        }

        #endregion

        #region Notes

        /// <summary>
        /// Adds a note to the customer with the specified <paramref name="customerId"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerNoteEntity>> AddCustomerNoteAsync(ObjectId customerId, CustomerNoteRequestModel model)
        {
            // Get the customer with the specified id
            var customer = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            // If the customer does not exist...
            if (customer is null)
                return WebServerFailable.NotFound(customerId, nameof(AppointMateDbMapper.Customers));

            var entity = CustomerNoteEntity.FromRequestModel(customer, model);

            await AppointMateDbMapper.CustomerNotes.AddAsync(entity);

            // Returns the entity
            return entity;
        }


        /// <summary>
        /// Adds a list of notes to the customer with the specified <paramref name="customerId"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="models">The models</param>
        /// <returns></returns>
        public async Task<WebServerFailable<IEnumerable<CustomerNoteEntity>>> AddCustomerNotesAsync(ObjectId customerId, IEnumerable<CustomerNoteRequestModel> models)
        {
            // Get the customer with the specified id
            var customer = await AppointMateDbMapper.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            // If the customer does not exist...
            if (customer is null)
                return WebServerFailable.NotFound(customerId, nameof(AppointMateDbMapper.Customers));

            return new WebServerFailable<IEnumerable<CustomerNoteEntity>>(await AppointMateDbMapper.CustomerNotes.AddRangeAsync(models.Select(x => CustomerNoteEntity.FromRequestModel(customer, x)).ToList()));
        }


        /// <summary>
        /// Updates the customer note with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerNoteEntity>> UpdateCustomerNoteAsync(ObjectId id, CustomerNoteRequestModel model)
        {
            var entity = await AppointMateDbMapper.CustomerNotes.UpdateAsync(id, model);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerNotes));

            return entity;
        }

        /// <summary>
        /// Deletes the customer note with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        public async Task<WebServerFailable<CustomerNoteEntity>> DeleteCustomerNoteAsync(ObjectId id)
        {
            var entity = await AppointMateDbMapper.CustomerNotes.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return WebServerFailable.NotFound(id, nameof(AppointMateDbMapper.CustomerNotes));

            await AppointMateDbMapper.CustomerNotes.DeleteOneAsync(x => x.Id == id);

            return entity;
        }

        #endregion

        #endregion
    }
}
