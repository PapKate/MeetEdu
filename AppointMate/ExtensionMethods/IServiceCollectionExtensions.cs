using AutoMapper;
using AutoMapper.Internal;

using System.Reflection;

namespace AppointMate
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        #region Constants

        /// <summary>
        /// The maps of the namespaces between the entities and the DTOs
        /// </summary>
        public static readonly IEnumerable<EntityToDTONamespaceMap> EntityToDTONamespaceMaps = new List<EntityToDTONamespaceMap>()
        {
            // Atom.Web.Server.Argon -> Argon.Web
            new(typeof(CustomerEntity).Namespace!, typeof(CustomerResponseModel).Namespace!)
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the pre-configured <see cref="Mapper"/> in the <paramref name="services"/>
        /// </summary>
        /// <param name="services">The services</param>
        /// <returns></returns>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            // The request model types
            var requestModelTypes = new List<Type>();
            // The response model types
            var responseModelTypes = new List<Type>();
            // The entity types
            var entityTypes = new List<Type>();
            // The embedded entity types
            var embeddedEntityTypes = new List<Type>();
            // The API args types
            var apiArgTypes = new List<Type>();

            // Create the configuration
            var configuration = new MapperConfiguration((cfg) =>
            {
                cfg.Internal().AllowAdditiveTypeMapCreation = true;
                cfg.Internal().MethodMappingEnabled = false;

                var assemblies = new List<Assembly>
                {
                    // For the request models
                    Assembly.GetAssembly(typeof(UserRequestModel))!,
                    // For the entities
                    Assembly.GetAssembly(typeof(UserEntity))!,
                    // For the response models
                    Assembly.GetAssembly(typeof(UserResponseModel))!,
                };

                // For every Atom assembly...
                foreach (var assembly in assemblies)
                {
                    // For every type
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.Name.EndsWith(FrameworkConstructionExtensions.RequestModelSuffix))
                            requestModelTypes.Add(type);

                        if (type.Name.EndsWith(FrameworkConstructionExtensions.EntitySuffix))
                        {
                            entityTypes.Add(type);

                            if (type.Name.Contains(FrameworkConstructionExtensions.EmbeddedPrefix))
                                embeddedEntityTypes.Add(type);
                        }

                        if (type.Name.EndsWith(FrameworkConstructionExtensions.ResponseModelSuffix))
                            responseModelTypes.Add(type);

                        if (type.Name.EndsWith(FrameworkConstructionExtensions.APIArgsSuffix))
                            apiArgTypes.Add(type);
                    }
                }

                // For every request model type...(RequestModel / CreateRequestModel / UpdateRequestModel -> Entity)
                foreach (var requestModelType in requestModelTypes)
                {
                    // Get the namespace map for the request DTO if any
                    var map = EntityToDTONamespaceMaps.FirstOrDefault(x => x.DTONamespace == requestModelType.Namespace);

                    // Get the prefix of the request model
                    var requestModelNamePrefix = requestModelType.Name.Replace(FrameworkConstructionExtensions.RequestModelSuffix, string.Empty);

                    // Get the entity type is any with the same prefix
                    var entityType = entityTypes.FirstOrDefault(x => (map is null ? true : map.EntityNamespace == x.Namespace) && x.Name.Replace(FrameworkConstructionExtensions.EntitySuffix, string.Empty) == requestModelNamePrefix);

                    // If there is an entity type...
                    if (entityType != null)
                        // Create a map
                        cfg.CreateMap(requestModelType, entityType);
                }

                // For every entity...(Entity -> Entity), (Entity -> EmbeddedEntity), (Entity -> ResponseModel)
                foreach (var entityType in entityTypes)
                {
                    // Get the namespace map for the entity if any
                    var map = EntityToDTONamespaceMaps.FirstOrDefault(x => x.EntityNamespace == entityType.Namespace);

                    // Create the map
                    cfg.CreateMap(entityType, entityType);

                    // Get the prefix of the model
                    var entityNamePrefix = entityType.Name.Replace(FrameworkConstructionExtensions.EntitySuffix, string.Empty);

                    // Get the response model type
                    var responseModelType = responseModelTypes.FirstOrDefault(x => (map is null ? true : x.Namespace == map.DTONamespace) && x.Name.Replace(FrameworkConstructionExtensions.ResponseModelSuffix, string.Empty) == entityNamePrefix);

                    // If there is a response model type...
                    if (responseModelType != null)
                        // Create a map
                        cfg.CreateMap(entityType, responseModelType);

                    // Get the embedded related entity type if any
                    var embeddedEntityType = entityTypes.FirstOrDefault(x => (map is null ? true : x.Namespace == map.EntityNamespace) && x.Name == FrameworkConstructionExtensions.EmbeddedPrefix + entityType.Name);

                    // If there is an embedded entity type...
                    if (embeddedEntityType != null)
                        // Create a map
                        cfg.CreateMap(entityType, embeddedEntityType);
                }
                
                // Custom Maps
            });

            // Create the mapper
            var mapper = new Mapper(configuration);

            // Add the mapper to the services
            services.AddSingleton(mapper);

            // Return for chaining
            return services;
        }

        #endregion
    }

    /// <summary>
    /// The map from entities to data transfer objects
    /// </summary>
    public record EntityToDTONamespaceMap
    {
        #region Public Properties

        /// <summary>
        /// The namespace of the entity
        /// </summary>
        public string EntityNamespace { get; set; }

        /// <summary>
        /// The namespace of the DTO
        /// </summary>
        public string DTONamespace { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="entityNamespace">The namespace of the entity</param>
        /// <param name="dtoNamespace">The namespace of the DTO</param>
        public EntityToDTONamespaceMap(string entityNamespace, string dtoNamespace) : base()
        {
            EntityNamespace = entityNamespace.NotNullOrEmpty();
            DTONamespace = dtoNamespace.NotNullOrEmpty();
        }

        #endregion
    }
}
