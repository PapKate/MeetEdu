using AutoMapper;

using MongoDB.Bson;
using MongoDB.Driver;

using System.Collections.Concurrent;
using System.Reflection;

namespace AppointMate
{
    /// <summary>
    /// Helper methods for entities
    /// </summary>
    public static class EntityHelpers
    {
        #region Private Members

        /// <summary>
        /// Maps a <see cref="Type.FullName"/> that implements the <see cref="IMongoCompanyIdentifiable"/> to information related to its <see cref="IMongoCompanyIdentifiable"/> sub properties
        /// </summary>
        private static readonly ConcurrentDictionary<string, CompanyEntityCompanyIdentifiablePropertiesInformationDataModel> mTypeToIdentifiablePropertiesMapper = new();

        #endregion

        #region Public Methods

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
            where TEntity : BaseEntity, ICompanyIdentifiable, new()
        {
            var entity = new TEntity()
            {
                CompanyId = companyId
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
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not <see cref="null"/>!
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
        /// NOTE: This method only affects the properties that can't be mapped by the <see cref="Mapper"/> and are not <see cref="null"/>!
        /// </param>
        /// <returns></returns>
        public static async Task<TEntity> FromRequestModelAsync<TEntity, TRequestModel>(TRequestModel model, ObjectId companyId, Func<TRequestModel, TEntity, ObjectId, Task> updateNonAutoMapperValues)
            where TEntity : BaseEntity, ICompanyIdentifiable, new()
        {
            var entity = new TEntity()
            {
                CompanyId = companyId
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
            where TEmbeddedEntity : BaseEntity, new()
        {
            var embeddedEntity = new TEmbeddedEntity();

            DI.Mapper.Map(entity, embeddedEntity);

            return embeddedEntity;
        }

        /// <summary>
        /// Initializes the specified <paramref name="entity"/> by setting all of the values of its sub properties
        /// that implement the <see cref="IMongoCompanyIdentifiable"/> interface the <see cref="IMongoCompanyIdentifiable.CompanyId"/>
        /// as its current company id value
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity that implements the <see cref="IMongoCompanyIdentifiable"/></typeparam>
        /// <param name="entity">The entity</param>
        public static void InitializeCompanyEntity<TEntity>(TEntity entity)
          where TEntity : ICompanyIdentifiable
        {
            // Get the type of the entity
            // NOTE: We are getting the type of the instance because the supplied entity could be
            //       a class that inherits from the TEntity!
            var entityType = entity.GetType();

            // Try to get the properties information and if the properties information isn't created for the specified type...
            if (!mTypeToIdentifiablePropertiesMapper.TryGetValue(entityType.FullName!, out var propertiesInformation))
            {
                var companyIdentifiableProperties = new List<PropertyInfo>();
                var enumerableCompanyIdentifiableProperties = new List<PropertyInfo>();

                // For every property...
                foreach (var property in entityType.GetProperties())
                {
                    // Check if the property is an enumerable
                    var isEnumerable = property.PropertyType.IsGenericIEnumerable();
                    // Get the non-enumerableType
                    var nonEnumerableType = TypeHelpers.GetNonEnumerableType(property.PropertyType);

                    // If the non-enumerable type implements the ICompanyIdentifiable interface...
                    if (nonEnumerableType.GetInterfaces().Any(x => x == typeof(ICompanyIdentifiable)))
                    {
                        // If the property was an enumerable...
                        if (isEnumerable)
                            // Add it to the enumerable properties
                            enumerableCompanyIdentifiableProperties.Add(property);
                        // Else...
                        else
                            // Add it to the standard properties
                            companyIdentifiableProperties.Add(property);
                    }
                }

                // Create the information model
                propertiesInformation = new CompanyEntityCompanyIdentifiablePropertiesInformationDataModel(entityType, companyIdentifiableProperties, enumerableCompanyIdentifiableProperties);

                // Map it
                mTypeToIdentifiablePropertiesMapper.TryAdd(entityType.FullName!, propertiesInformation);
            }

            // For every company identifiable property...
            foreach (var companyIdentifiableProperty in propertiesInformation.CompanyIdentifiableProperties)
            {
                // Get the value
                var value = (ICompanyIdentifiable?)companyIdentifiableProperty.GetValue(entity);

                // If there isn't a value...
                if (value == null)
                    // Continue
                    continue;

                // Set its company id
                value.CompanyId = entity.CompanyId;
                // Set the company id to its children
                //value.EndInit();
            }

            // For every enumerable company identifiable property...
            foreach (var enumerableCompanyIdentifiableProperty in propertiesInformation.EnumerableCompanyIdentifiableProperties)
            {
                // Get the value
                var value = (IEnumerable<ICompanyIdentifiable>?)enumerableCompanyIdentifiableProperty.GetValue(entity);

                // If there isn't a value...
                if (value == null)
                    // Continue
                    continue;

                // For every value...
                foreach (var v in value)
                {
                    // Sets its company id
                    v.CompanyId = entity.CompanyId;
                    // Set the company id to its children
                    //v.EndInit();
                }
            }
        }

        #endregion

        #region Private Classes

        private sealed class CompanyEntityCompanyIdentifiablePropertiesInformationDataModel
        {
            #region Public Properties

            /// <summary>
            /// The parent type that implements the <see cref="IMongoCompanyIdentifiable"/>
            /// </summary>
            public Type Type { get; }

            /// <summary>
            /// The properties of the <see cref="Type"/> that implement the <see cref="IMongoCompanyIdentifiable"/>
            /// </summary>
            public IEnumerable<PropertyInfo> CompanyIdentifiableProperties { get; }

            /// <summary>
            /// The enumerable properties of the <see cref="Type"/> whose generic type implement the <see cref="IMongoCompanyIdentifiable"/>
            /// </summary>
            public IEnumerable<PropertyInfo> EnumerableCompanyIdentifiableProperties { get; }

            #endregion

            #region Constructors

            /// <summary>
            /// Default constructor
            /// </summary>
            /// <param name="type">The parent type that implements the <see cref="IMongoCompanyIdentifiable"/></param>
            /// <param name="companyIdentifiableProperties">The properties of the <see cref="Type"/> that implement the <see cref="IMongoCompanyIdentifiable"/></param>
            /// <param name="enumerableCompanyIdentifiableProperties">The enumerable properties of the <see cref="Type"/> whose generic type implement the <see cref="IMongoCompanyIdentifiable"/></param>
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
