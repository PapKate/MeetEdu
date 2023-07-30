using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections.Concurrent;
using System.Reflection;

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
        /// <param name="cancellationToken">The cancelation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<IEnumerable<TResponse>>?> GetManyAsync<TResponse, TEntity, TKey>(IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, bool isAscending, Func<TEntity, TKey>? orderSelector, APIArgs? args, Func<TEntity, TResponse> projector, CancellationToken cancellationToken = default)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            // Finds the documents
            var entities = await collection.SelectAsync(filter);

            // If there are no documents...
            if (entities is null)
                // Return
                return null;

            // If there is an order selector...
            if (orderSelector is not null)
            {
                // If it is to be ordered by in ascending order...
                if (isAscending)
                    entities.OrderBy(orderSelector);
                // Else...
                else
                    entities.OrderByDescending(orderSelector);
            }

            // If the args are not null
            if (args is not null)
            {
                // Limit the results
                entities = entities.Skip((args.Page * args.PerPage) + args.Offset)
                                    .Take(args.PerPage).ToList();
            }

            return new OkObjectResult(entities.Select(x => projector(x)));
        }

        /// <summary>
        /// Get one 
        /// </summary>
        /// <typeparam name="TResponse">The type of the response model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="projector">The projector</param>
        /// <param name="filter">The filter</param>
        /// <param name="cancellationToken">The cancelation token</param>
        /// <returns></returns>
        public static async Task<ActionResult<TResponse>?> GetAsync<TResponse, TEntity>(IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, Func<TEntity, TResponse> projector, CancellationToken cancellationToken = default)
            where TResponse : BaseResponseModel
            where TEntity : BaseEntity
        {
            // Finds the documents
            var entity = await collection.FirstOrDefaultAsync(filter);

            // If there are no documents...
            if (entity is null)
                // Return
                return null;

            return new OkObjectResult(projector(entity));
        }

        #endregion
    }
}
