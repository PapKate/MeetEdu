using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using MongoDB.Driver.Linq;

using SharpCompress.Common;

using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace MeetEdu
{
    /// <summary>
    /// Helper methods for entities
    /// </summary>
    public static class EntityHelpers
    {
        #region Private Members

        /// <summary>
        /// Maps a <see cref="Type.FullName"/> that implements the <see cref="IDepartmentIdentifiable"/> to information related to its <see cref="IDepartmentIdentifiable"/> sub properties
        /// </summary>
        private static readonly ConcurrentDictionary<string, CompanyEntityCompanyIdentifiablePropertiesInformationDataModel> mTypeToIdentifiablePropertiesMapper = new();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the user that has the specified <paramref name="userId"/> and return the <see cref="EmbeddedUserEntity"/>
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns></returns>
        public static async Task<EmbeddedUserEntity> GetUserAsync(string userId)
        {
            var user = await MeetEduDbMapper.Users.FirstOrDefaultAsync(x => x.Id == userId.ToObjectId());
            return user.ToEmbeddedEntity();
        }

        /// <summary>
        /// Creates and returns an entity of the specified type using the specified
        /// <paramref name="model"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static TEntity FromRequestModel<TEntity>(object model)
            where TEntity : BaseEntity, new()
        {
            var entity = new TEntity();

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns an entity of the specified type using the specified
        /// <paramref name="model"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="model">The model</param>
        /// <param name="action">The action</param>
        /// <returns></returns>
        public static TEntity FromRequestModel<TEntity>(object model, Action<TEntity>? action = null)
            where TEntity : BaseEntity, new()
        {
            var entity = new TEntity();

            DI.Mapper.Map(model, entity);
            // Calls the action
            action?.Invoke(entity);
            
            return entity;
        }

        /// <summary>
        /// Creates and returns an entity of the specified type using the specified
        /// <paramref name="model"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="model">The model</param>
        /// <param name="companyId">The company id</param>
        /// <returns></returns>
        public static TEntity FromRequestModel<TEntity>(object model, ObjectId companyId)
            where TEntity : BaseEntity, IDepartmentIdentifiable, new()
        {
            var entity = new TEntity()
            {
                DepartmentId = companyId
            };

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns an entity of the specified type using the specified
        /// <paramref name="model"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TRequestModel">The type of the request model</typeparam>
        /// <param name="model">The type of the model</param>
        /// <param name="updateNonAutoMapperValues">
        /// Updates the values of the entity with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </param>
        /// <returns></returns>
        public static async Task<TEntity> FromRequestModelAsync<TEntity, TRequestModel>(TRequestModel model, Func<TRequestModel, TEntity, Task> updateNonAutoMapperValues)
            where TEntity : BaseEntity, new()
        {
            var entity = new TEntity();

            DI.Mapper.Map(model, entity);

            await updateNonAutoMapperValues(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns an entity of the specified type using the specified
        /// <paramref name="model"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TRequestModel">The type of the request model</typeparam>
        /// <param name="model">The type of the model</param>
        /// <param name="companyId">The company id</param>
        /// <param name="updateNonAutoMapperValues">
        /// Updates the values of the entity with the values of the specified <paramref name="model"/>.
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not null!
        /// </param>
        /// <returns></returns>
        public static async Task<TEntity> FromRequestModelAsync<TEntity, TRequestModel>(TRequestModel model, ObjectId companyId, Func<TRequestModel, TEntity, ObjectId, Task> updateNonAutoMapperValues)
            where TEntity : BaseEntity, IDepartmentIdentifiable, new()
        {
            var entity = new TEntity()
            {
                DepartmentId = companyId
            };

            DI.Mapper.Map(model, entity);

            await updateNonAutoMapperValues(model, entity, companyId);

            return entity;
        }

        /// <summary>
        /// Creates and returns a response model of a specified type using the specified <paramref name="entity"/>
        /// </summary>
        /// <typeparam name="TResponseModel">The type of the response model</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static TResponseModel ToResponseModel<TResponseModel>(BaseEntity entity)
            where TResponseModel : new()
        {
            return DI.Mapper.Map(entity, new TResponseModel());
        }

        /// <summary>
        /// Creates and returns an embedded entity from the specified <paramref name="entity"/>
        /// </summary>
        /// <typeparam name="TEmbeddedEntity">The type of the embedded entity</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public static TEmbeddedEntity ToEmbeddedEntity<TEmbeddedEntity>(BaseEntity entity)
            where TEmbeddedEntity : EmbeddedBaseEntity, new()
        {
            var embeddedEntity = new TEmbeddedEntity();

            DI.Mapper.Map(entity, embeddedEntity);

            return embeddedEntity;
        }


        /// <summary>
        /// Updates the property of the entity selected by the specified <paramref name="entityDocumentPropertySelector"/>
        /// with a value retrieved using the specified <paramref name="queryable"/> along with the id provided by the value
        /// of the property selected by the specified <paramref name="modelIdPropertySelector"/>. If no value was retrieved 
        /// by the queryable null is set, otherwise the <see cref="ObjectId"/> that was used for retrieving the
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
            where TEmbeddedEntity : EmbeddedBaseEntity?
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
            where TEmbeddedEntity : EmbeddedBaseEntity?
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

        #region Private Classes

        private sealed class CompanyEntityCompanyIdentifiablePropertiesInformationDataModel
        {
            #region Public Properties

            /// <summary>
            /// The parent type that implements the <see cref="IDepartmentIdentifiable"/>
            /// </summary>
            public Type Type { get; }

            /// <summary>
            /// The properties of the <see cref="Type"/> that implement the <see cref="IDepartmentIdentifiable"/>
            /// </summary>
            public IEnumerable<PropertyInfo> CompanyIdentifiableProperties { get; }

            /// <summary>
            /// The enumerable properties of the <see cref="Type"/> whose generic type implement the <see cref="IDepartmentIdentifiable"/>
            /// </summary>
            public IEnumerable<PropertyInfo> EnumerableCompanyIdentifiableProperties { get; }

            #endregion

            #region Constructors

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="type">The parent type that implements the <see cref="IDepartmentIdentifiable"/></param>
            /// <param name="companyIdentifiableProperties">The properties of the <see cref="Type"/> that implement the <see cref="IDepartmentIdentifiable"/></param>
            /// <param name="enumerableCompanyIdentifiableProperties">The enumerable properties of the <see cref="Type"/> whose generic type implement the <see cref="IDepartmentIdentifiable"/></param>
            public CompanyEntityCompanyIdentifiablePropertiesInformationDataModel(Type type, IEnumerable<PropertyInfo> companyIdentifiableProperties, IEnumerable<PropertyInfo> enumerableCompanyIdentifiableProperties) : base()
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                CompanyIdentifiableProperties = companyIdentifiableProperties ?? new List<PropertyInfo>();
                EnumerableCompanyIdentifiableProperties = enumerableCompanyIdentifiableProperties ?? new List<PropertyInfo>();
            }

            #endregion
        }

        #endregion
    }

}
