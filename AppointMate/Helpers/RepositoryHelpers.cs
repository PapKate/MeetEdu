using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq.Expressions;
using System.Threading;

namespace AppointMate
{
    /// <summary>
    /// Helper Methods for the repositories
    /// </summary>
    public static class RepositoryHelpers
    {
        #region Public Methods

        /// <summary>
        /// Adds the specified <paramref name="entity"/> to the specified <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> AddAsync<TEntity>(this IMongoCollection<TEntity> collection, TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            await collection.InsertOneAsync(entity, null, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Adds the specified <paramref name="entities"/> to the specified <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entities">The entity</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(this IMongoCollection<TEntity> collection, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            await collection.InsertManyAsync(entities, null, cancellationToken);
            return entities;
        }

        /// <summary>
        /// Returns the first document that matches the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter expression</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.FindAsync<TEntity>(filter);

            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the first document that matches the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id of the document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static Task<TEntity> FirstOrDefaultAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => collection.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        /// <summary>
        /// Returns the first document that matches the requirements
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter expression</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> FirstAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.FindAsync<TEntity>(filter);

            return await result.FirstAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the first document that matches the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id of the document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static Task<TEntity> FirstAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => collection.FirstAsync(x => x.Id == id, cancellationToken);

        /// <summary>
        /// Returns the documents that match the requirements and null if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> SelectAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var items = await collection.FindAsync(filter, cancellationToken: cancellationToken);

            return await items.ToListAsync();
        }

        /// <summary>
        /// Returns the first document that matches the requirements and throws a <see cref="InvalidOperationException"/> if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="filter">The filter expression</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<bool> AnyAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var result = await collection.FindAsync<TEntity>(filter);

            return await result.AnyAsync(cancellationToken);
        }

        /// <summary>
        /// Returns the first document that matches the requirements and throws a <see cref="InvalidOperationException"/> if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id of the document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static Task<bool> AnyAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => collection.AnyAsync(x => x.Id == id, cancellationToken);

        /// <summary>
        /// Updates the specified <paramref name="entity"/> while keeping its id the same.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The new updated document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> UpdateAsync<TEntity>(this IMongoCollection<TEntity> collection, TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions() { IsUpsert = false }, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static async Task<TEntity?> UpdateAsync<TEntity, TRequestModel>(this IMongoCollection<TEntity> collection, ObjectId id, TRequestModel model)
            where TEntity : BaseEntity
            where TRequestModel : BaseRequestModel
        {
            var entity = await collection.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return null;

            DI.Mapper.Map(model, entity);
            return await collection.UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the specified <paramref name="entities"/> while keeping their id the same
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entities">The entities</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(this IMongoCollection<TEntity> collection, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
        {
            var updates = new List<WriteModel<TEntity>>();
            foreach (var entity in entities)
                updates.Add(new ReplaceOneModel<TEntity>(Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id), entity) { IsUpsert = true });

            await collection.BulkWriteAsync(updates, null, cancellationToken);

            return entities;
        }

        /// <summary>
        /// Deletes the entity with the specified <paramref name="id"/> from the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> DeleteAsync<TEntity>(this IMongoCollection<TEntity> collection, ObjectId id, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => await collection.FindOneAndDeleteAsync(x => x.Id == id, cancellationToken: cancellationToken);

        /// <summary>
        /// Deletes the specified <paramref name="entity"/> from the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The new updated document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> DeleteAsync<TEntity>(this IMongoCollection<TEntity> collection, TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => await collection.DeleteAsync(entity, cancellationToken);

        /// <summary>
        /// Deletes the specified <paramref name="entities"/> from the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entities">The new updated document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<DeleteResult> DeleteRangeAsync<TEntity>(this IMongoCollection<TEntity> collection, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => await collection.DeleteManyAsync(x => entities.Contains(x), cancellationToken);

        #endregion
    }
}
