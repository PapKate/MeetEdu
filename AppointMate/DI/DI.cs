using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// The default application services that should be available everywhere in the application
    /// </summary>
    public static class DI
    {
        #region Private Members
        /// <summary>
        /// The member of the <see cref="Database"/> property
        /// </summary>
        private static readonly Lazy<IMongoDatabase> mDatabase = new(() =>
        {
            return new MongoClient(new MongoClientSettings()).GetDatabase("AppointMate");
        });

        /// <summary>
        /// The member of the <see cref="Mapper"/> property
        /// </summary>
        private static readonly Lazy<Mapper> mMapper = new(() => DI.GetRequiredService<Mapper>());

        #endregion

        #region Public Properties

        /// <summary>
        /// The database context
        /// </summary>
        public static IMongoDatabase Database => mDatabase.Value;

        /// <summary>
        /// The mapper
        /// </summary>
        public static Mapper Mapper => mMapper.Value;

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        public static IServiceProvider? Provider { get; set; }

        /// <summary>
        /// The accounts manager
        /// </summary>
        public static AccountsRepository AccountsRepository => (AccountsRepository)Provider!.CreateScope().ServiceProvider.GetService(typeof(AccountsRepository))!;

        /// <summary>
        /// The users manager
        /// </summary>
        public static UsersRepository UsersRepository => (UsersRepository)Provider!.CreateScope().ServiceProvider.GetService(typeof(UsersRepository))!;

        /// <summary>
        /// The companies manager
        /// </summary>
        public static CompaniesRepository CompaniesRepository=> (CompaniesRepository)Provider!.CreateScope().ServiceProvider.GetService(typeof(CompaniesRepository))!;

        /// <summary>
        /// The staff members manager
        /// </summary>
        public static StaffMembersRepository StaffMembersRepository => (StaffMembersRepository)Provider!.CreateScope().ServiceProvider.GetService(typeof(StaffMembersRepository))!;

        /// <summary>
        /// The customers manager
        /// </summary>
        public static CustomersRepository CustomersRepository => (CustomersRepository)Provider!.CreateScope().ServiceProvider.GetService(typeof(CustomersRepository))!;

        /// <summary>
        /// The services manager
        /// </summary>
        public static ServicesRepository ServicesRepository => (ServicesRepository)Provider!.CreateScope().ServiceProvider.GetService(typeof(ServicesRepository))!;    

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the requested service from the service provider
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// NOTE: Some services require a different approach when retrieving them.
        ///       For example, some services , in order to function properly, require
        ///       some variables that their default parameterless constructors can't set.
        ///       In that case, use the methods located in the DI in their own library!
        /// </typeparam>
        /// <returns></returns>
        public static TService GetRequiredService<TService>(Action<TService>? configuration = null)
        {
            var service = Provider!.GetService<TService>();

            if (service is null)
                throw new InvalidOperationException($"The service of type {typeof(TService)} has not been injected in the DI!");

            configuration?.Invoke(service);

            return service;
        }

        #endregion
    }
}
