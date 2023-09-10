namespace MeetBase
{
    /// <summary>
    /// Helper methods for <see cref="Type"/>
    /// </summary>
    public static class TypeHelpers
    {
        #region Public Methods

        /// <summary>
        /// Gets the T from the <see cref="IEnumerable{T}"/> of the specified <paramref name="type"/>
        /// when it implements the <see cref="IEnumerable{T}"/> interface
        /// </summary>
        /// <param name="type">The type that implements the <see cref="IEnumerable{T}"/> interface</param>
        /// <returns></returns>3
        public static Type GetNonEnumerableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            foreach (var iEnumerable in type.GetInterfaces())
                if (iEnumerable.IsGenericType && iEnumerable.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return iEnumerable.GetGenericArguments()[0];

            return type;
        }

        /// <summary>
        /// Gets the T from the <see cref="Nullable{T}"/> of the specified <paramref name="type"/>
        /// when the <paramref name="type"/> is nullable, otherwise it returns the type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static Type GetNonNullableType(Type type)
            => Nullable.GetUnderlyingType(type) ?? type;

        /// <summary>
        /// Uses the specified <paramref name="type"/> to create the <see cref="Nullable{type}"/>
        /// when the <paramref name="type"/> is a value type, otherwise it returns the <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type whose nullable equivalent to get</param>
        /// <returns></returns>
        public static Type GetNullableType(Type type)
        {
            // Use Nullable.GetUnderlyingType() to remove the Nullable<T> wrapper if type is already nullable.
            type = Nullable.GetUnderlyingType(type) ?? type; // avoid type becoming null
            if (type.IsValueType)
                return typeof(Nullable<>).MakeGenericType(type);
            else
                return type;
        }

        /// <summary>
        /// Gets the default value of the specified <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public static object? GetDefaultValue(Type type)
            => type.IsValueType ? Activator.CreateInstance(type)! : null;

        #region Numbers

        /// <summary>
        /// Converts the specified <paramref name="number"/> from its current type to
        /// the specified <paramref name="type"/>
        /// </summary>
        /// <param name="number">The number</param>
        /// <param name="type">The numeric type to convert the number</param>
        /// <returns></returns>
        public static object? ConvertNumber(object? number, Type type)
        {
            // If the number is null...
            if (number is null)
                // Return the default value of the to type...
                return GetDefaultValue(type);

            return Convert.ChangeType(number, GetNonNullableType(type));
        }

        /// <summary>
        /// Adds the specified <paramref name="number1"/> to the specified <paramref name="number2"/>
        /// and returns a number of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="number1">The first number</param>
        /// <param name="number2">The second number</param>
        /// <returns></returns>
        public static object? AddNumbers(Type type, object? number1, object? number2)
        {
            if (type == typeof(sbyte))
                return (sbyte)number1! + (sbyte)number2!;
            if (type == typeof(sbyte?))
                return (sbyte?)number1 + (sbyte?)number2;
            else if (type == typeof(byte))
                return (byte)number1! + (byte)number2!;
            else if (type == typeof(byte?))
                return (byte?)number1 + (byte?)number2;
            else if (type == typeof(short))
                return (short)number1! + (short)number2!;
            else if (type == typeof(short?))
                return (short?)number1 + (short?)number2;
            else if (type == typeof(ushort))
                return (ushort)number1! + (ushort)number2!;
            else if (type == typeof(ushort?))
                return (ushort?)number1 + (ushort?)number2;
            else if (type == typeof(int))
                return (int)number1! + (int)number2!;
            else if (type == typeof(int?))
                return (int?)number1 + (int?)number2;
            else if (type == typeof(uint))
                return (uint)number1! + (uint)number2!;
            else if (type == typeof(uint?))
                return (uint?)number1 + (uint?)number2;
            else if (type == typeof(long))
                return (long)number1! + (long)number2!;
            else if (type == typeof(long?))
                return (long?)number1 + (long?)number2;
            else if (type == typeof(ulong))
                return (ulong)number1! + (ulong)number2!;
            else if (type == typeof(ulong?))
                return (ulong?)number1 + (ulong?)number2;
            else if (type == typeof(float))
                return (float)number1! + (float)number2!;
            else if (type == typeof(float?))
                return (float?)number1 + (float?)number2;
            else if (type == typeof(double))
                return (double)number1! + (double)number2!;
            else if (type == typeof(double?))
                return (double?)number1 + (double?)number2;
            else if (type == typeof(decimal))
                return (decimal)number1! + (decimal)number2!;
            else if (type == typeof(decimal?))
                return (decimal?)number1 + (decimal?)number2;

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Sums the specified <paramref name="numbers"/> and returns a number of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="numbers">The numbers</param>
        /// <returns></returns>
        public static object? AddNumbers(Type type, IEnumerable<object?> numbers)
        {
            if (type == typeof(sbyte))
                return numbers.Cast<sbyte>().Sum(x => x);
            if (type == typeof(sbyte?))
                return numbers.Cast<sbyte?>().Sum(x => x);
            else if (type == typeof(byte))
                return numbers.Cast<byte>().Sum(x => x);
            else if (type == typeof(byte?))
                return numbers.Cast<byte?>().Sum(x => x);
            else if (type == typeof(short))
                return numbers.Cast<short>().Sum(x => x);
            else if (type == typeof(short?))
                return numbers.Cast<short?>().Sum(x => x);
            else if (type == typeof(ushort))
                return numbers.Cast<ushort>().Sum(x => x);
            else if (type == typeof(ushort?))
                return numbers.Cast<ushort?>().Sum(x => x);
            else if (type == typeof(int))
                return numbers.Cast<int>().Sum(x => x);
            else if (type == typeof(int?))
                return numbers.Cast<int?>().Sum(x => x);
            else if (type == typeof(uint))
                return numbers.Cast<uint>().Sum(x => x);
            else if (type == typeof(uint?))
                return numbers.Cast<uint?>().Sum(x => x);
            else if (type == typeof(long))
                return numbers.Cast<long>().Sum(x => x);
            else if (type == typeof(long?))
                return numbers.Cast<long?>().Sum(x => x);
            else if (type == typeof(ulong))
                return numbers.Cast<long>().Sum(x => x);
            else if (type == typeof(ulong?))
                return numbers.Cast<long?>().Sum(x => x);
            else if (type == typeof(float))
                return numbers.Cast<float>().Sum(x => x);
            else if (type == typeof(float?))
                return numbers.Cast<float?>().Sum(x => x);
            else if (type == typeof(double))
                return numbers.Cast<double>().Sum(x => x);
            else if (type == typeof(double?))
                return numbers.Cast<double?>().Sum(x => x);
            else if (type == typeof(decimal))
                return numbers.Cast<decimal>().Sum(x => x);
            else if (type == typeof(decimal?))
                return numbers.Cast<decimal?>().Sum(x => x);

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Subtracts the specified <paramref name="subtracter"/> from the specified <paramref name="subtrahend"/>
        /// and returns a number of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="subtrahend">The subtrahend</param>
        /// <param name="subtracter">The subtracter</param>
        /// <returns></returns>
        public static object? SubtractNumbers(Type type, object? subtrahend, object? subtracter)
        {
            if (type == typeof(sbyte))
                return (sbyte)subtrahend! - (sbyte)subtracter!;
            if (type == typeof(sbyte?))
                return (sbyte?)subtrahend - (sbyte?)subtracter;
            else if (type == typeof(byte))
                return (byte)subtrahend! - (byte)subtracter!;
            else if (type == typeof(byte?))
                return (byte?)subtrahend - (byte?)subtracter;
            else if (type == typeof(short))
                return (short)subtrahend! - (short)subtracter!;
            else if (type == typeof(short?))
                return (short?)subtrahend - (short?)subtracter;
            else if (type == typeof(ushort))
                return (ushort)subtrahend! - (ushort)subtracter!;
            else if (type == typeof(ushort?))
                return (ushort?)subtrahend - (ushort?)subtracter;
            else if (type == typeof(int))
                return (int)subtrahend! - (int)subtracter!;
            else if (type == typeof(int?))
                return (int?)subtrahend - (int?)subtracter;
            else if (type == typeof(uint))
                return (uint)subtrahend! - (uint)subtracter!;
            else if (type == typeof(uint?))
                return (uint?)subtrahend - (uint?)subtracter;
            else if (type == typeof(long))
                return (long)subtrahend! - (long)subtracter!;
            else if (type == typeof(long?))
                return (long?)subtrahend - (long?)subtracter;
            else if (type == typeof(ulong))
                return (ulong)subtrahend! - (ulong)subtracter!;
            else if (type == typeof(ulong?))
                return (ulong?)subtrahend - (ulong?)subtracter;
            else if (type == typeof(float))
                return (float)subtrahend! - (float)subtracter!;
            else if (type == typeof(float?))
                return (float?)subtrahend - (float?)subtracter;
            else if (type == typeof(double))
                return (double)subtrahend! - (double)subtracter!;
            else if (type == typeof(double?))
                return (double?)subtrahend - (double?)subtracter;
            else if (type == typeof(decimal))
                return (decimal)subtrahend! - (decimal)subtracter!;
            else if (type == typeof(decimal?))
                return (decimal?)subtrahend - (decimal?)subtracter;

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Multiplies the specified <paramref name="number1"/> with the specified <paramref name="number2"/>
        /// and returns a number of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="number1">The first number</param>
        /// <param name="number2">The second number</param>
        /// <returns></returns>
        public static object? MultiplyNumbers(Type type, object? number1, object? number2)
        {
            if (type == typeof(sbyte))
                return (sbyte)number1! * (sbyte)number2!;
            if (type == typeof(sbyte?))
                return (sbyte?)number1 * (sbyte?)number2;
            else if (type == typeof(byte))
                return (byte)number1! * (byte)number2!;
            else if (type == typeof(byte?))
                return (byte?)number1 * (byte?)number2;
            else if (type == typeof(short))
                return (short)number1! * (short)number2!;
            else if (type == typeof(short?))
                return (short?)number1 * (short?)number2;
            else if (type == typeof(ushort))
                return (ushort)number1! * (ushort)number2!;
            else if (type == typeof(ushort?))
                return (ushort?)number1 * (ushort?)number2;
            else if (type == typeof(int))
                return (int)number1! * (int)number2!;
            else if (type == typeof(int?))
                return (int?)number1 * (int?)number2;
            else if (type == typeof(uint))
                return (uint)number1! * (uint)number2!;
            else if (type == typeof(uint?))
                return (uint?)number1 * (uint?)number2;
            else if (type == typeof(long))
                return (long)number1! * (long)number2!;
            else if (type == typeof(long?))
                return (long?)number1 * (long?)number2;
            else if (type == typeof(ulong))
                return (ulong)number1! * (ulong)number2!;
            else if (type == typeof(ulong?))
                return (ulong?)number1 * (ulong?)number2;
            else if (type == typeof(float))
                return (float)number1! * (float)number2!;
            else if (type == typeof(float?))
                return (float?)number1 * (float?)number2;
            else if (type == typeof(double))
                return (double)number1! * (double)number2!;
            else if (type == typeof(double?))
                return (double?)number1 * (double?)number2;
            else if (type == typeof(decimal))
                return (decimal)number1! * (decimal)number2!;
            else if (type == typeof(decimal?))
                return (decimal?)number1 * (decimal?)number2;

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Divides the specified <paramref name="numerator"/> with the specified <paramref name="denominator"/>
        /// and returns a number of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="numerator">The numerator</param>
        /// <param name="denominator">The denominator</param>
        /// <returns></returns>
        public static object? DivideNumbers(Type type, object? numerator, object? denominator)
        {
            if (type == typeof(sbyte))
                return (sbyte)numerator! / (sbyte)denominator!;
            if (type == typeof(sbyte?))
                return (sbyte?)numerator / (sbyte?)denominator;
            else if (type == typeof(byte))
                return (byte)numerator! / (byte)denominator!;
            else if (type == typeof(byte?))
                return (byte?)numerator / (byte?)denominator;
            else if (type == typeof(short))
                return (short)numerator! / (short)denominator!;
            else if (type == typeof(short?))
                return (short?)numerator / (short?)denominator;
            else if (type == typeof(ushort))
                return (ushort)numerator! / (ushort)denominator!;
            else if (type == typeof(ushort?))
                return (ushort?)numerator / (ushort?)denominator;
            else if (type == typeof(int))
                return (int)numerator! / (int)denominator!;
            else if (type == typeof(int?))
                return (int?)numerator / (int?)denominator;
            else if (type == typeof(uint))
                return (uint)numerator! / (uint)denominator!;
            else if (type == typeof(uint?))
                return (uint?)numerator / (uint?)denominator;
            else if (type == typeof(long))
                return (long)numerator! / (long)denominator!;
            else if (type == typeof(long?))
                return (long?)numerator / (long?)denominator;
            else if (type == typeof(ulong))
                return (ulong)numerator! / (ulong)denominator!;
            else if (type == typeof(ulong?))
                return (ulong?)numerator / (ulong?)denominator;
            else if (type == typeof(float))
                return (float)numerator! / (float)denominator!;
            else if (type == typeof(float?))
                return (float?)numerator / (float?)denominator;
            else if (type == typeof(double))
                return (double)numerator! / (double)denominator!;
            else if (type == typeof(double?))
                return (double?)numerator / (double?)denominator;
            else if (type == typeof(decimal))
                return (decimal)numerator! / (decimal)denominator!;
            else if (type == typeof(decimal?))
                return (decimal?)numerator / (decimal?)denominator;

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Gets the absolute representation of the specified <paramref name="number"/>
        /// and returns a number of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="number">The number</param>
        /// <returns></returns>
        public static object? AbsoluteNumber(Type type, object? number)
        {
            if (type == typeof(sbyte))
                return Math.Abs((sbyte)number!);
            if (type == typeof(sbyte?))
                return (sbyte?)number is null ? (sbyte?)null : Math.Abs((sbyte)number);
            else if (type == typeof(byte))
                return (byte)number!;
            else if (type == typeof(byte?))
                return (byte?)number;
            else if (type == typeof(short))
                return Math.Abs((short)number!);
            else if (type == typeof(short?))
                return (short?)number is null ? (short?)null : Math.Abs((short)number);
            else if (type == typeof(ushort))
                return (ushort)number!;
            else if (type == typeof(ushort?))
                return (ushort)number!;
            else if (type == typeof(int))
                return Math.Abs((int)number!);
            else if (type == typeof(int?))
                return (int?)number is null ? (int?)null : Math.Abs((int)number);
            else if (type == typeof(uint))
                return (uint)number!;
            else if (type == typeof(uint?))
                return (uint?)number;
            else if (type == typeof(long))
                return Math.Abs((long)number!);
            else if (type == typeof(long?))
                return (long?)number is null ? (long?)null : Math.Abs((long)number);
            else if (type == typeof(ulong))
                return (ulong)number!;
            else if (type == typeof(ulong?))
                return (ulong?)number;
            else if (type == typeof(float))
                return Math.Abs((float)number!);
            else if (type == typeof(float?))
                return (float?)number is null ? (float?)null : Math.Abs((float)number);
            else if (type == typeof(double))
                return Math.Abs((double)number!);
            else if (type == typeof(double?))
                return (double?)number is null ? (double?)null : Math.Abs((double)number);
            else if (type == typeof(decimal))
                return Math.Abs((decimal)number!);
            else if (type == typeof(decimal?))
                return (decimal?)number is null ? (decimal?)null : Math.Abs((decimal)number); ;

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Gets the minimum value of the specified numbers and returns a number 
        /// of type <paramref name="type"/>.
        /// NOTE: When both numbers are null then null is returned, otherwise the
        ///       null number is casted to a 0!
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="number1">The first number</param>
        /// <param name="number2">The second number</param>
        /// <returns></returns>
        public static object? MinNumber(Type type, object? number1, object? number2)
        {
            if (type == typeof(sbyte))
                return Math.Min((sbyte)number1!, (sbyte)number2!);
            if (type == typeof(sbyte?))
                return (sbyte?)number1 is null && (sbyte?)number2 is null ? (sbyte?)null : Math.Min((sbyte?)number1 ?? 0, (sbyte?)number2 ?? 0);
            else if (type == typeof(byte))
                return Math.Min((byte)number1!, (byte)number2!);
            else if (type == typeof(byte?))
                return (byte?)number1 is null && (byte?)number2 is null ? (byte?)null : Math.Min((byte?)number1 ?? 0, (byte?)number2 ?? 0);
            else if (type == typeof(short))
                return Math.Min((short)number1!, (short)number2!);
            else if (type == typeof(short?))
                return (short?)number1 is null && (short?)number2 is null ? (short?)null : Math.Min((short?)number1 ?? 0, (short?)number2 ?? 0);
            else if (type == typeof(ushort))
                return Math.Min((ushort)number1!, (ushort)number2!);
            else if (type == typeof(ushort?))
                return (ushort?)number1 is null && (ushort?)number2 is null ? (ushort?)null : Math.Min((ushort?)number1 ?? 0, (ushort?)number2 ?? 0);
            else if (type == typeof(int))
                return Math.Min((int)number1!, (int)number2!);
            else if (type == typeof(int?))
                return (int?)number1 is null && (int?)number2 is null ? (int?)null : Math.Min((int?)number1 ?? 0, (int?)number2 ?? 0);
            else if (type == typeof(uint))
                return Math.Min((uint)number1!, (uint)number2!);
            else if (type == typeof(uint?))
                return (uint?)number1 is null && (uint?)number2 is null ? (uint?)null : Math.Min((uint?)number1 ?? 0, (uint?)number2 ?? 0);
            else if (type == typeof(long))
                return Math.Min((long)number1!, (long)number2!);
            else if (type == typeof(long?))
                return (long?)number1 is null && (long?)number2 is null ? (long?)null : Math.Min((long?)number1 ?? 0, (long?)number2 ?? 0);
            else if (type == typeof(ulong))
                return Math.Min((ulong)number1!, (ulong)number2!);
            else if (type == typeof(ulong?))
                return (ulong?)number1 is null && (ulong?)number2 is null ? (ulong?)null : Math.Min((ulong?)number1 ?? 0, (ulong?)number2 ?? 0);
            else if (type == typeof(float))
                return Math.Min((float)number1!, (float)number2!);
            else if (type == typeof(float?))
                return (float?)number1 is null && (float?)number2 is null ? (float?)null : Math.Min((float?)number1 ?? 0, (float?)number2 ?? 0);
            else if (type == typeof(double))
                return Math.Min((double)number1!, (double)number2!);
            else if (type == typeof(double?))
                return (double?)number1 is null && (double?)number2 is null ? (double?)null : Math.Min((double?)number1 ?? 0, (double?)number2 ?? 0);
            else if (type == typeof(decimal))
                return Math.Min((decimal)number1!, (decimal)number2!);
            else if (type == typeof(decimal?))
                return (decimal?)number1 is null && (decimal?)number2 is null ? (decimal?)null : Math.Min((decimal?)number1 ?? 0, (decimal?)number2 ?? 0);

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        /// <summary>
        /// Gets the maximum value of the specified numbers and returns a number 
        /// of type <paramref name="type"/>.
        /// NOTE: When both numbers are null then null is returned, otherwise the
        ///       null number is casted to a 0!
        /// </summary>
        /// <param name="type">The type of the numbers</param>
        /// <param name="number1">The first number</param>
        /// <param name="number2">The second number</param>
        /// <returns></returns>
        public static object? MaxNumber(Type type, object? number1, object? number2)
        {
            if (type == typeof(sbyte))
                return Math.Max((sbyte)number1!, (sbyte)number2!);
            if (type == typeof(sbyte?))
                return (sbyte?)number1 is null && (sbyte?)number2 is null ? (sbyte?)null : Math.Max((sbyte?)number1 ?? 0, (sbyte?)number2 ?? 0);
            else if (type == typeof(byte))
                return Math.Max((byte)number1!, (byte)number2!);
            else if (type == typeof(byte?))
                return (byte?)number1 is null && (byte?)number2 is null ? (byte?)null : Math.Max((byte?)number1 ?? 0, (byte?)number2 ?? 0);
            else if (type == typeof(short))
                return Math.Max((short)number1!, (short)number2!);
            else if (type == typeof(short?))
                return (short?)number1 is null && (short?)number2 is null ? (short?)null : Math.Max((short?)number1 ?? 0, (short?)number2 ?? 0);
            else if (type == typeof(ushort))
                return Math.Max((ushort)number1!, (ushort)number2!);
            else if (type == typeof(ushort?))
                return (ushort?)number1 is null && (ushort?)number2 is null ? (ushort?)null : Math.Max((ushort?)number1 ?? 0, (ushort?)number2 ?? 0);
            else if (type == typeof(int))
                return Math.Max((int)number1!, (int)number2!);
            else if (type == typeof(int?))
                return (int?)number1 is null && (int?)number2 is null ? (int?)null : Math.Max((int?)number1 ?? 0, (int?)number2 ?? 0);
            else if (type == typeof(uint))
                return Math.Max((uint)number1!, (uint)number2!);
            else if (type == typeof(uint?))
                return (uint?)number1 is null && (uint?)number2 is null ? (uint?)null : Math.Max((uint?)number1 ?? 0, (uint?)number2 ?? 0);
            else if (type == typeof(long))
                return Math.Max((long)number1!, (long)number2!);
            else if (type == typeof(long?))
                return (long?)number1 is null && (long?)number2 is null ? (long?)null : Math.Max((long?)number1 ?? 0, (long?)number2 ?? 0);
            else if (type == typeof(ulong))
                return Math.Max((ulong)number1!, (ulong)number2!);
            else if (type == typeof(ulong?))
                return (ulong?)number1 is null && (ulong?)number2 is null ? (ulong?)null : Math.Max((ulong?)number1 ?? 0, (ulong?)number2 ?? 0);
            else if (type == typeof(float))
                return Math.Max((float)number1!, (float)number2!);
            else if (type == typeof(float?))
                return (float?)number1 is null && (float?)number2 is null ? (float?)null : Math.Max((float?)number1 ?? 0, (float?)number2 ?? 0);
            else if (type == typeof(double))
                return Math.Max((double)number1!, (double)number2!);
            else if (type == typeof(double?))
                return (double?)number1 is null && (double?)number2 is null ? (double?)null : Math.Max((double?)number1 ?? 0, (double?)number2 ?? 0);
            else if (type == typeof(decimal))
                return Math.Max((decimal)number1!, (decimal)number2!);
            else if (type == typeof(decimal?))
                return (decimal?)number1 is null && (decimal?)number2 is null ? (decimal?)null : Math.Max((decimal?)number1 ?? 0, (decimal?)number2 ?? 0);

            throw new InvalidOperationException($"The type {type.Name} is not a number!");
        }

        #endregion

        #endregion
    }
}
