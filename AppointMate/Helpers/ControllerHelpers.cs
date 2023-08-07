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
        /// <typeparam name="TKey">The key type</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="projector">The projector</param>
        /// <param name="filter">The filter</param>
        /// <param name="isAscending">A flag indicating whether the order is ascending or descending</param>
        /// <param name="orderSelector">The order selector</param>
        /// <param name="args">The args</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<IEnumerable<TResponse>>> GetManyAsync<TResponse, TEntity, TKey>(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter, Expression<Func<TEntity, TKey>>? orderSelector, bool isAscending, 
                                                                                                              APIArgs? args, Func<TEntity, TResponse> projector, CancellationToken cancellationToken = default)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            var query = collection.AsQueryable().Where(x => filter.Inject());

            // If there is an order selector...
            if (orderSelector is not null)
            {
                // If it is to be ordered by in ascending order...
                if (isAscending)
                    query = query.OrderBy(orderSelector);
                // Else...
                else
                    query = query.OrderByDescending(orderSelector);
            }

            // If the args are not null
            if (args is not null)
            {
                // Limit the results
                query = query.Skip((args.Page * args.PerPage) + args.Offset)
                             .Take(args.PerPage);
            }

            return (await ManagerHelpers.GetManyAsync(collection, filter, orderSelector, isAscending, args, cancellationToken)).Select(x => projector(x)).ToList();
        }

        /// <summary>
        /// Get one 
        /// </summary>
        /// <typeparam name="TResponse">The type of the response model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="projector">The projector</param>
        /// <param name="filter">The filter</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<TResponse>?> GetAsync<TResponse, TEntity>(IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, Func<TEntity, TResponse> projector, CancellationToken cancellationToken = default)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            // Finds the documents
            var entity = await ManagerHelpers.GetAsync(collection, filter, cancellationToken);

            // If there are no documents...
            if (entity is null)
                // Return
                return null;

            return projector(entity);
        }

        #endregion
    }

    /// <summary>
    /// Helper methods for <see cref="AppointMateManager"/>s
    /// </summary>
    public static class ManagerHelpers
    {
        #region Public Methods

        /// <summary>
        /// Get many 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter</param>
        /// <param name="isAscending">A flag indicating whether the order is ascending or descending</param>
        /// <param name="orderSelector">The order selector</param>
        /// <param name="args">The args</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> GetManyAsync<TEntity, TKey>(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter, Expression<Func<TEntity, TKey>>? orderSelector, 
                                                                                   bool isAscending, APIArgs? args, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var query = collection.AsQueryable().Where(x => filter.Inject());

            // If there is an order selector...
            if (orderSelector is not null)
            {
                // If it is to be ordered by in ascending order...
                if (isAscending)
                    query = query.OrderBy(orderSelector);
                // Else...
                else
                    query = query.OrderByDescending(orderSelector);
            }

            // If the args are not null
            if (args is not null && args.PerPage > 0)
            {
                // Limit the results
                query = query.Skip((args.Page * args.PerPage) + args.Offset)
                             .Take(args.PerPage);
            }

            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get many 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter</param>
        /// <param name="args">The args</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> GetManyAsync<TEntity>(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter, APIArgs? args, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity 
            => await GetManyAsync(collection, filter, x => x.Id, true, args, cancellationToken);

        /// <summary>
        /// Get one 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter</param>
        /// <param name="isAscending">A flag indicating whether the order is ascending or descending</param>
        /// <param name="orderSelector">The order selector</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> GetAsync<TEntity, TKey>(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter, Expression<Func<TEntity, TKey>>? orderSelector, bool isAscending, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            // Finds the documents
            var query = collection.AsQueryable().Where(x => filter.Inject());

            // If there is an order selector...
            if (orderSelector is not null)
            {
                // If it is to be ordered by in ascending order...
                if (isAscending)
                    query = query.OrderBy(orderSelector);
                // Else...
                else
                    query = query.OrderByDescending(orderSelector);
            }

            return await query.FirstAsync(cancellationToken);
        }

        /// <summary>
        /// Get one 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> GetAsync<TEntity>(IMongoCollection<TEntity> collection, FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity => await GetAsync(collection, filter, x => x.Id, true, cancellationToken);

        #endregion
    }
}
