using System.Collections;
using System.Reflection;

namespace MeetBase
{
    /// <summary>
    /// Extension methods for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="NumericTypes"/>
        /// </summary>
        private static readonly Lazy<Type[]> mSimpleTypes = new Lazy<Type[]>(() => new Type[]
        {
            typeof(Uri),

            typeof(TimeSpan),
            typeof(TimeSpan?),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(DateTime?),
            typeof(DateTimeOffset?),
            typeof(DateOnly),
            typeof(DateOnly?),
            typeof(TimeOnly),
            typeof(TimeOnly?),

            typeof(string),

            typeof(Version),
            typeof(PhoneNumber),

            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),

            typeof(float),
            typeof(double),
            typeof(decimal)
        });

        /// <summary>
        /// The member of the <see cref="NumericTypes"/>
        /// </summary>
        private static readonly Lazy<Type[]> mNumericTypes = new Lazy<Type[]>(() => IntegralNumericTypes.Concat(FloatingPointNumericTypes).ToArray());

        /// <summary>
        /// The member of the <see cref="IntegralNumericTypes"/>
        /// </summary>
        private static readonly Lazy<Type[]> mIntegralNumericTypes = new Lazy<Type[]>(() => new Type[]
        {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong)
        });

        /// <summary>
        /// The member of the <see cref="FloatingPointNumericTypes"/>
        /// </summary>
        private static readonly Lazy<Type[]> mFloatingPointNumericTypes = new Lazy<Type[]>(() => new Type[]
        {
            typeof(float),
            typeof(double),
            typeof(decimal)
        });

        #endregion

        #region Public Properties

        /// <summary>
        /// The numeric types
        /// </summary>
        public static Type[] NumericTypes => mNumericTypes.Value;

        /// <summary>
        /// The integral numeric types
        /// </summary>
        public static Type[] IntegralNumericTypes => mIntegralNumericTypes.Value;

        /// <summary>
        /// The floating point numeric types
        /// </summary>
        public static Type[] FloatingPointNumericTypes => mFloatingPointNumericTypes.Value;

        /// <summary>
        /// The simple class/struct types
        /// </summary>
        public static Type[] SimpleTypes => mSimpleTypes.Value;

        /// <summary>
        /// The available types of the <see cref="Func{TResult}"/>
        /// </summary>
        public static IEnumerable<Type> FuncTypes { get; } = new List<Type>()
        {
            typeof(Func<>),
            typeof(Func<,>),
            typeof(Func<,,>),
            typeof(Func<,,,>),
            typeof(Func<,,,,>),
            typeof(Func<,,,,,>),
            typeof(Func<,,,,,,>),
            typeof(Func<,,,,,,,>),
            typeof(Func<,,,,,,,,>),
            typeof(Func<,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,,>),
        };

        /// <summary>
        /// The available types of the <see cref="Action"/>
        /// </summary>
        public static IEnumerable<Type> ActionTypes { get; } = new List<Type>()
        {
            typeof(Action<>),
            typeof(Action<,>),
            typeof(Action<,,>),
            typeof(Action<,,,>),
            typeof(Action<,,,,>),
            typeof(Action<,,,,,>),
            typeof(Action<,,,,,,>),
            typeof(Action<,,,,,,,>),
            typeof(Action<,,,,,,,,>),
            typeof(Action<,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,,>),
        };

        /// <summary>
        /// Maps the type names to their related CSharp keywords
        /// </summary>
        public static IReadOnlyDictionary<Type, string> TypeNameToCSharpKeywordMapper { get; } = new Dictionary<Type, string>()
        {
            { typeof(SByte), "sbyte" },
            { typeof(Byte), "byte" },
            { typeof(Int16), "short" },
            { typeof(UInt16), "ushort" },
            { typeof(Int32), "int" },
            { typeof(UInt32), "uint" },
            { typeof(Int64), "long" },
            { typeof(UInt64), "ulong" },
            { typeof(IntPtr), "nint" },
            { typeof(UIntPtr), "nuint" },
            { typeof(Single), "float" },
            { typeof(Double), "double" },
            { typeof(Decimal), "decimal" },
            { typeof(Boolean), "bool" },
            { typeof(Char), "char" },
            { typeof(String), "string" },
            { typeof(Object), "object" }
        };

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="type"/> inherits from
        /// the specified <paramref name="typeOrGenericTypeDefinition"/>. If the <paramref name="typeOrGenericTypeDefinition"/>
        /// is the same as the <paramref name="type"/> then <see cref="false"/> is returned!
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="typeOrGenericTypeDefinition">The type or the generic type definition</param>
        /// <returns></returns>
        public static bool InheritsFrom(this Type type, Type typeOrGenericTypeDefinition)
        {
            // If the inserted type is the same as the other type or the generic type definition...
            if (type == typeOrGenericTypeDefinition)
                // Return false
                return false;

            // If the other type isn't a generic type definition...
            if (!typeOrGenericTypeDefinition.IsGenericTypeDefinition)
                // Simply return
                return typeOrGenericTypeDefinition.IsAssignableFrom(type);

            while (type is not null && type != typeof(object))
            {
                var current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (typeOrGenericTypeDefinition == current)
                    return true;
                type = type.BaseType!;
            }
            return false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks whether the <paramref name="type"/> is a complex type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsComplexType(this Type type)
        {
            if (type == typeof(string))
                return false;

            if (type.IsEnumerable())
                return true;

            return type.IsClass && type.GetProperties().Any() && !SimpleTypes.Contains(type);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="type"/> implements the <see cref="IEnumerable"/>
        /// or it's it self the <see cref="IEnumerable"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
                return true;

            return type.GetInterfaces().Any(x => x == typeof(IEnumerable));
        }

        /// <summary>
        /// Checks whether the specified <paramref name="type"/> implements the <see cref="IEnumerable{T}"/>
        /// or it's it self the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsGenericIEnumerable(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return true;

            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        /// <summary>
        /// Checks if the specified <paramref name="type"/> is a type that represents a number
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsNumber(this Type type)
            => type == typeof(sbyte) || type == typeof(byte) || type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(ushort) || type == typeof(long) || type == typeof(ulong) || type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(sbyte?) || type == typeof(byte?) || type == typeof(short?) || type == typeof(ushort?) || type == typeof(int?) || type == typeof(uint?) || type == typeof(ushort?) || type == typeof(long?) || type == typeof(ulong?) || type == typeof(float?) || type == typeof(double?) || type == typeof(decimal?);

        /// <summary>
        /// Checks if the specified <paramref name="type"/> is a type that represents a date
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static bool IsDate(this Type type)
            => type == typeof(DateTime) || type == typeof(DateTimeOffset)
                || type == typeof(DateTime?) || type == typeof(DateTimeOffset?)
                || type == typeof(DateOnly) || type == typeof(DateOnly?);

        #endregion
    }
}
