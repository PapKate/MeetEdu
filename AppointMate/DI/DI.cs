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


        #endregion

        #region Public Properties

        /// <summary>
        /// The database context
        /// </summary>
        public static IMongoDatabase Database => mDatabase.Value;

        #endregion
    }
}
