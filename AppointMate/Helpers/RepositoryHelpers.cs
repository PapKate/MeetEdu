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
        /// Returns the first document that matches the requirements and <see cref="null"/> if none is found
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
        /// Returns the first document that matches the requirements and <see cref="null"/> if none is found
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
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
        /// Returns the first document that matches the requirements and <see cref="null"/> if none is found
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
        /// Returns the documents that match the requirements and <see cref="null"/> if none is found
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
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

            return await collection.FirstAsync(entity.Id);
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
        /// Deletes the specified <paramref name="entity"/> from the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The new updated document</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<TEntity> DeleteAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity,bool>> filter, CancellationToken cancellationToken = default)
            where TEntity : BaseEntity
            => await collection.FindOneAndDeleteAsync(filter, cancellationToken: cancellationToken);

        /// <summary>
        /// Deletes the specified <paramref name="entity"/> from the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="entity">The new updated document</param>
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

        /// <summary>
        /// Updates the property of the entity selected by the specified <paramref name="entityDocumentPropertySelector"/>
        /// with a value retrieved using the specified <paramref name="queryable"/> along with the id provided by the value
        /// of the property selected by the specified <paramref name="modelIdPropertySelector"/>. If no value was retrieved 
        /// by the queryable <see cref="null"/> is set, otherwise the <see cref="ObjectId"/> that was used for retrieving the
        /// value
        /// </summary>
        /// <typeparam name="TRequestModel">The type of the request model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TSourceEntity">The type of the entities that will be searched</typeparam>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <param name="modelIdPropertySelector">Selects the id property of the <typeparamref name="TRequestModel"/></param>
        /// <param name="entityDocumentPropertySelector">Selects the document property of the <typeparamref name="TEntity"/></param>
        /// <param name="queryable">The queryable</param>
        /// <param name="updateAction">Further updates the entity</param>
        /// <returns></returns>
        public static async Task UpdateNonAutoMapperValueAsync<TRequestModel, TEntity, TSourceEntity>(
            TRequestModel model,
            TEntity entity,
            Expression<Func<TRequestModel, string>> modelIdPropertySelector,
            Expression<Func<TEntity, ObjectId?>> entityDocumentPropertySelector,
            IMongoQueryable<TSourceEntity> queryable,
            Action<TEntity, TSourceEntity>? updateAction = null)
            where TEntity : BaseEntity
            where TSourceEntity : BaseEntity
        {
            // Get model property
            var modelProperty = modelIdPropertySelector.GetPropertyInfo();

            // Get the id
            var id = (string?)modelProperty.GetValue(model);

            if (id != null)
            {
                // Get the entity document property
                var entityDocumentProperty = entityDocumentPropertySelector.GetPropertyInfo();

                // If the value is an empty string...
                if (id.IsNullOrEmpty())
                    // Clear the value of the entity
                    entityDocumentProperty.SetValue(entity, null);
                // Else...
                else
                {
                    // Create the object id
                    var objectId = id.ToObjectId();
                    // Check if an entity with this id exists
                    var targetEntity = await queryable.FirstOrDefaultAsync(x => x.Id == objectId);

                    if (targetEntity != null)
                    {
                        // Update the entity
                        entityDocumentProperty.SetValue(entity, objectId);

                        // Further update the entity
                        updateAction?.Invoke(entity, targetEntity);
                    }
                    else
                        // Update the entity
                        entityDocumentProperty.SetValue(entity, null);
                }
            }
        }

        /// <summary>
        /// Updates the property of the entity selected by the specified <paramref name="entityDocumentPropertySelector"/>
        /// with a value retrieved using the specified <paramref name="queryable"/> along with the id provided by the value
        /// of the property selected by the specified <paramref name="modelIdPropertySelector"/>
        /// </summary>
        /// <typeparam name="TRequestModel">The type of the request model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TSourceEntity">The type of the entities that will be searched</typeparam>
        /// <typeparam name="TEmbeddedEntity">
        /// The type of the entity that should be created by the <typeparamref name="TSourceEntity"/> and
        /// placed as the value of the property of the <typeparamref name="TEntity"/>
        /// </typeparam>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <param name="modelIdPropertySelector">Selects the id property of the <typeparamref name="TRequestModel"/></param>
        /// <param name="entityDocumentPropertySelector">Selects the document property of the <typeparamref name="TEntity"/></param>
        /// <param name="queryable">The queryable</param>
        /// <param name="projection">Creates and returns the embedded entity from the source entity</param>
        /// <param name="updateAction">Further updates the entity</param>
        /// <returns></returns>
        public static async Task UpdateNonAutoMapperValueAsync<TRequestModel, TEntity, TSourceEntity, TEmbeddedEntity>(
            TRequestModel model,
            TEntity entity,
            Expression<Func<TRequestModel, string>> modelIdPropertySelector,
            Expression<Func<TEntity, TEmbeddedEntity>> entityDocumentPropertySelector,
            IMongoQueryable<TSourceEntity> queryable,
            Func<TSourceEntity, TEmbeddedEntity> projection,
            Action<TEntity, TSourceEntity>? updateAction = null)
            where TEntity : BaseEntity
            where TSourceEntity : BaseEntity
            where TEmbeddedEntity : BaseEntity
        {
            // Get model property
            var modelProperty = modelIdPropertySelector.GetPropertyInfo();

            // Get the id
            var id = (string?)modelProperty.GetValue(model);

            // If the entity should get updated...
            if (id != null)
            {
                // Get the entity document property
                var entityDocumentProperty = entityDocumentPropertySelector.GetPropertyInfo();

                // If the value is an empty string...
                if (id.IsNullOrEmpty())
                    // Clear the value of the entity
                    entityDocumentProperty.SetValue(entity, null);
                // Else...
                else
                {
                    // Get the target entity
                    var targetEntity = await queryable.FirstAsync(x => x.Id == id.ToObjectId());

                    // Update the entity
                    entityDocumentProperty.SetValue(entity, projection(targetEntity));

                    // Further update the entity
                    updateAction?.Invoke(entity, targetEntity);
                }
            }
        }

        /// <summary>
        /// Updates the property of the entity selected by the specified <paramref name="entityDocumentsPropertySelector"/>
        /// with a value retrieved using the specified <paramref name="queryable"/> along with the ids provided by the value
        /// of the property selected by the specified <paramref name="modelIdsPropertySelector"/>
        /// </summary>
        /// <typeparam name="TRequestModel">The type of the request model</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TSourceEntity">The type of the entities that will be searched</typeparam>
        /// <typeparam name="TEmbeddedEntity">
        /// The type of the entity that should be created by the <typeparamref name="TSourceEntity"/> and
        /// placed as the value of the property of the <typeparamref name="TEntity"/>
        /// </typeparam>
        /// <param name="model">The model</param>
        /// <param name="entity">The entity</param>
        /// <param name="modelIdsPropertySelector">Selects the ids property of the <typeparamref name="TRequestModel"/></param>
        /// <param name="entityDocumentsPropertySelector">Selects the documents property of the <typeparamref name="TEntity"/></param>
        /// <param name="queryable">The queryable</param>
        /// <param name="projection">Creates and returns the embedded entity from the source entity</param>
        /// <returns></returns>
        public static async Task UpdateNonAutoMapperEnumerableValueAsync<TRequestModel, TEntity, TSourceEntity, TEmbeddedEntity>(
            TRequestModel model,
            TEntity entity,
            Expression<Func<TRequestModel, IEnumerable<string>>> modelIdsPropertySelector,
            Expression<Func<TEntity, IEnumerable<TEmbeddedEntity>>> entityDocumentsPropertySelector,
            IMongoQueryable<TSourceEntity> queryable,
            Func<TSourceEntity, TEmbeddedEntity> projection)
            where TEntity : BaseEntity
            where TSourceEntity : BaseEntity
            where TEmbeddedEntity : BaseEntity
        {
            // Get model property
            var modelProperty = modelIdsPropertySelector.GetPropertyInfo();

            // Get the ids
            var ids = (IEnumerable<string>?)modelProperty.GetValue(model);

            // If entity should get updated...
            if (ids != null)
            {
                // Get the entity documents property
                var entityDocumentsProperty = entityDocumentsPropertySelector.GetPropertyInfo();

                // If the value is an empty enumerable...
                if (ids.IsNullOrEmpty())
                    // Clear the value of the entity
                    entityDocumentsProperty.SetValue(entity, Enumerable.Empty<TEmbeddedEntity>());
                // Else...
                else
                {
                    // Get the object ids
                    var objectIds = ids.Select(x => x.ToObjectId()).ToList();

                    // Get the target entities
                    var targetEntities = await queryable.Where(x => objectIds.Contains(x.Id)).ToListAsync();

                    // Update the entity
                    entityDocumentsProperty.SetValue(entity, targetEntities.Select(x => projection(x)).ToList());
                }
            }
        }

        #endregion
    }
}
