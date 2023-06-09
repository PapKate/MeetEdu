using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;

namespace AppointMate
{
    /// <summary>
    /// Global access points for the AppointMate application database 
    /// </summary>
    public static class AppointMateDbMapper
    {
        #region Categories

        /// <summary>
        /// The categories collection
        /// </summary>
        public static IMongoCollection<CategoryEntity> Categories => DI.Database.GetCollection<CategoryEntity>(AppointMateMongoDbContext.CategoriesCollectionName);

        #endregion

        #region Companies

        /// <summary>
        /// The categories collection
        /// </summary>
        public static IMongoCollection<CategoryEntity> CompanyCategories => DI.Database.GetCollection<CategoryEntity>(AppointMateMongoDbContext.CategoriesCollectionName);

        #endregion

        #region Labels

        #endregion

        #region Notes

        #endregion

        #region Payments

        #endregion

        #region Points

        #endregion

        #region Reviews

        #endregion

        #region Services

        #endregion

        #region Users

        #endregion

        #region Customers


        #endregion

        #region Staff Members

        #endregion
    }
}
