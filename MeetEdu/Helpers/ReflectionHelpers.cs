using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;

namespace MeetEdu
{
    /// <summary>
    /// Reflection helper methods
    /// </summary>
    public class ReflectionHelpers
    {
        /// <summary>
        /// Creates and returns a <see cref="List{T}"/> that can contains items of the specified <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the items the list contains</param>
        /// <returns></returns>
        public static IList CreateList(Type type)
        {
            var generic = typeof(List<>);
            var constructedClass = generic.MakeGenericType(type);
            return (IList)Activator.CreateInstance(constructedClass)!;
        }
    }

    /// <summary>
    /// Used for converting comma separated <see cref="Enum"/>s to <see cref="IEnumerable{Enum}"/>s
    /// </summary>
    public class EnumCommaSeparatedModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="ModelBindingContext"/>.</param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            if (bindingContext.ModelType.GetInterface(typeof(IEnumerable<string>).Name) != null)
                return Task.CompletedTask;

            var modelName = string.IsNullOrEmpty(bindingContext.BinderModelName)
                ? bindingContext.ModelName
                : bindingContext.BinderModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var valueToBind = valueProviderResult.FirstValue;

            if (valueToBind == null)
                return Task.CompletedTask;

            var enumType = bindingContext.ModelType.GetGenericArguments()[0];

            var enumNames = valueToBind.Split(',');

            var enumValues = Enum.GetValues(enumType);

            var value = ReflectionHelpers.CreateList(enumType);

            foreach (var enumValue in enumValues)
            {
                if (enumNames.Contains(enumValue.ToString()))
                    value.Add(enumValue);
            }

            bindingContext.Result = ModelBindingResult.Success(value);

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Provides the <see cref="EnumCommaSeparatedModelBinder"/> when necessary
    /// </summary>
    public class CommaSeparatedModelBinderProvider : IModelBinderProvider
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Instance"/> property
        /// </summary>
        private static readonly Lazy<CommaSeparatedModelBinderProvider> mInstace = new(() => new());

        #endregion

        #region Public Properties

        /// <summary>
        /// The single instance of the <see cref="CommaSeparatedModelBinderProvider"/>
        /// </summary>
        public static CommaSeparatedModelBinderProvider Instance => mInstace.Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected CommaSeparatedModelBinderProvider() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            //if (context.Metadata.ModelType == typeof(IEnumerable<string>))
            //    return new BinderTypeModelBinder(typeof(EnumCommaSeparatedModelBinder));

            if (context.Metadata.ModelType != typeof(string) && context.Metadata.ModelType.IsGenericIEnumerable())
            {
                // Get the generic type
                var genericType = context.Metadata.ModelType.GetGenericArguments()[0];

                // If it's an enum...
                if (genericType.BaseType == typeof(Enum))
                    return new BinderTypeModelBinder(typeof(EnumCommaSeparatedModelBinder));
            }

            return null;
        }

        #endregion
    }
}
