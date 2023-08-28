using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq.Expressions;

namespace AppointMate.Helpers
{
    /// <summary>
    /// Helper methods for <see cref="Controller"/>s
    /// </summary>
    public static class ControllerHelpers
    {
        #region Public Methods

        /// <summary>
        /// Get many 
        /// </summary>
        /// <typeparam name="TResponse">The type of the response model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="projector">The projector</param>
        /// <param name="filter">The filter</param>
        /// <param name="orderCondition">Indicates whether the order is ascending or descending</param>
        /// <param name="orderSelector">The order selector</param>
        /// <param name="args">The args</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<IEnumerable<TResponse>>> GetManyAsync<TResponse, TEntity>(IMongoCollection<TEntity> collection, 
                                                                                                              Func<TEntity, TResponse> projector,
                                                                                                              FilterDefinition<TEntity> filter, APIArgs? args, 
                                                                                                              CancellationToken cancellationToken = default,
                                                                                                              Expression<Func<TEntity, object>>? orderSelector = null,
                                                                                                              OrderCondition orderCondition = OrderCondition.Ascending)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            var query = collection.AsQueryable().Where(x => filter.Inject());

            // If there is an order selector...
            if (orderSelector is not null)
            {
                query = query.OrderBy(orderSelector, orderCondition);
            }

            // If the args are not null
            if (args is not null)
            {
                // Limit the results
                query = query.Skip((args.Page * args.PerPage) + args.Offset)
                             .Take(args.PerPage);
            }

            return (await query.ToListAsync(cancellationToken)).Select(x => projector(x)).ToList();
        }

        /// <summary>
        /// Get one 
        /// </summary>
        /// <typeparam name="TResponse">The type of the response model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="projector">The projector</param>
        /// <param name="filter">The filter</param>
        /// <param name="orderSelector"></param>
        /// <param name="orderCondition">Indicates whether the order is ascending or descending</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<TResponse>?> GetAsync<TResponse, TEntity>(IMongoCollection<TEntity> collection, Func<TEntity, TResponse> projector, 
                                                                                        FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default,
                                                                                        Expression<Func<TEntity, object>>? orderSelector = null,
                                                                                        OrderCondition orderCondition = OrderCondition.Ascending)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            // Finds the documents
            var query = collection.AsQueryable().Where(x => filter.Inject());

            // If there is an order selector...
            if (orderSelector is not null)
            {
                query = query.OrderBy(orderSelector, orderCondition);
            }

            var entity = await query.FirstAsync(cancellationToken);

            // If there are no documents...
            if (entity is null)
                // Return
                return null;

            return projector(entity);
        }


        /// <summary>
        /// Get one 
        /// </summary>
        /// <typeparam name="TResponse">The type of the response model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="projector">The projector</param>
        /// <param name="filter">The filter</param>
        /// <param name="orderSelector"></param>
        /// <param name="orderCondition">Indicates whether the order is ascending or descending</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<TResponse>?> GetAsync<TResponse, TEntity>(IMongoCollection<TEntity> collection, Func<TEntity, TResponse> projector,
                                                                                        Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default,
                                                                                        Expression<Func<TEntity, object>>? orderSelector = null,
                                                                                        OrderCondition orderCondition = OrderCondition.Ascending)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            // Finds the documents
            var query = collection.AsQueryable().Where(filter);

            // If there is an order selector...
            if (orderSelector is not null)
            {
                query = query.OrderBy(orderSelector, orderCondition);
            }

            var entity = await query.FirstAsync(cancellationToken);

            // If there are no documents...
            if (entity is null)
                // Return
                return null;

            return projector(entity);
        }

        #endregion
    }
}
