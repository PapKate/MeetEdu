using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections;

namespace MeetEdu
{
    /// <summary>
    /// Contains the <see cref="UserEntity"/> and depending on the user role...
    /// <list type="bullet">
    /// <item>If it is a secretary, the <see cref="SecretaryEntity"/></item>
    /// <item>If it is a professor, the <see cref="ProfessorEntity"/></item>
    /// <item>If it is a member, the <see cref="MemberEntity"/></item>
    /// </list>
    /// </summary>
    public class LoginResult
    {
        #region Public Properties

        /// <summary>
        /// The user
        /// </summary>
        public UserEntity? User { get; }

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryEntity? Secretary { get; }

        /// <summary>
        /// The professor
        /// </summary>
        public ProfessorEntity? Professor { get; }

        /// <summary>
        /// The member
        /// </summary>
        public MemberEntity? Member { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for a secretary
        /// </summary>
        public LoginResult(UserEntity user, SecretaryEntity secretary) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Secretary = secretary ?? throw new ArgumentNullException(nameof(secretary));
        }

        /// <summary>
        /// Constructor for a professor
        /// </summary>
        public LoginResult(UserEntity user, ProfessorEntity professor) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Professor = professor ?? throw new ArgumentNullException(nameof(professor));
        }

        /// <summary>
        /// Constructor for a member
        /// </summary>
        public LoginResult(UserEntity user, MemberEntity member) : base()
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Member = member ?? throw new ArgumentNullException(nameof(member));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="UserResponseModel"/> from the current <see cref="UserEntity"/>
        /// </summary>
        /// <returns></returns>
        public LoginResponse ToResponseModel()
        {
            var response = new LoginResponse();

            // If there is a user...
            if (User is not null)
                // Adds the user to the response
                response.User = EntityHelpers.ToResponseModel<UserResponseModel>(User);

            // If there is a secretary...
            if (Secretary is not null)
                // Adds the secretary to the response
                response.Secretary = EntityHelpers.ToResponseModel<SecretaryResponseModel>(Secretary);

            // If there is a professor...
            if (Professor is not null)
                // Adds the professor to the response
                response.Professor = EntityHelpers.ToResponseModel<ProfessorResponseModel>(Professor);

            // If there is a member...
            if (Member is not null)
                // Adds the user to the response
                response.Member = EntityHelpers.ToResponseModel<MemberResponseModel>(Member);

            return response;
        }

        #endregion
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
