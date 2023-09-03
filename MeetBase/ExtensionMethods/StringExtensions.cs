using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace MeetBase
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks whether the string is null or empty
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Returns true if the string is null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
            => string.IsNullOrEmpty(value);

        /// <summary>
        /// Returns the specified <paramref name="value"/> if it's not null or empty,
        /// otherwise it throws an <see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string NotNullOrEmpty(this string? value)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(value));

            return value;
        }

        /// <summary>
        /// Converts the given <paramref name="value"/> to an <see cref="int"/>
        /// </summary>
        /// <remarks>
        ///     An Int32 can only be up to 9 digits long!
        /// </remarks>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static int ToInt(this string? value)
        {
            if (value.IsNullOrEmpty() || value == "-" || value == "+")
                return 0;

            return int.Parse(value, LocalizationConstants.Culture);
        }

        /// <summary>
        /// Checks if the inserted string is a valid email
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static bool IsEmail(this string? value)
        {
            if (value.IsNullOrEmpty())
                return false;

            return RegexConstants.EmailRegex.IsMatch(value);
        }

        /// <summary>
        /// Encrypts the specified <paramref name="value"/> with the <see cref="SHA256"/> hash method
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static string EncryptPassword(this string value)
        {
            using var sha256Hash = SHA256.Create();
            // Convert the password string to a byte array.
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            // Convert the byte array back to a string representation.
            var builder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")); // "x2" means lowercase hexadecimal format.
            }

            return builder.ToString();
        }

        #endregion
    }
}
