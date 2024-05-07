using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MeetBase
{
    /// <summary>
    /// Helper methods related to Newtonsoft
    /// </summary>
    public static class NewtonsoftHelpers
    {
        #region Public Properties

        /// <summary>
        /// A single instance of the <see cref="JsonSerializerSettings"/>
        /// </summary>
        public static JsonSerializerSettings Settings { get; } = CreateSerializerSettings();

        #endregion

        #region Public Methods

        /// <summary>
        /// Configures the specified <paramref name="settings"/>
        /// </summary>
        /// <param name="settings">The settings</param>
        public static void ConfigureSerializer(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true,
                    OverrideSpecifiedNames = false
                }
            };
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Converters.Add(new StringEnumConverter());
            settings.Converters.Add(new TimeOnlyToStringJsonConverter());
            settings.Converters.Add(new DateOnlyToStringJsonConverter());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and returns a pre-configured <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings();

            ConfigureSerializer(settings);

            return settings;
        }

        #endregion
    }
}
