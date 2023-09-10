using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MeetBase
{
    /// <summary>
    /// Helper methods for <see cref="string"/>s
    /// </summary>
    public static class StringHelpers
    {
        #region Private Methods

        private static readonly Regex mSplitCamelCaseFirstPassRegex = new Regex("(?<before>[^A-Z]|[0-9])(?<after>([A-Z]|[0-9]))", RegexOptions.Compiled);
        private static readonly Regex mSplitCamelCaseSecondPassRegex = new Regex("(?<before>[^ ])(?<after>([A-Z][^A-Zs]))", RegexOptions.Compiled);

        /// <summary>
        /// The regular expression used for search for XML tags
        /// </summary>
        private static readonly Regex mXMLTagsRegex = new Regex("<.*?>", RegexOptions.Compiled);

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes the diacritics from the specified <paramref name="s"/>.
        /// Ex.: Λογικά -> Λογικα
        /// </summary>
        /// <param name="s">The string value</param>
        /// <returns></returns>
        public static string RemoveDiacritics(string s)
        {
            if (s.IsNullOrEmpty())
                return s;

            // Normalize the string
            var normalizedString = s.Normalize(NormalizationForm.FormD);

            // Allocate the span
            Span<char> span = stackalloc char[s.Length];

            // Declare an index for the current character
            var index = 0;

            // For every character...
            foreach (var character in normalizedString)
            {
                // Get the Unicode category
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);

                // If the character is a non spacing mark...
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    // Add it
                    span[index] = character;

                    // Point to the next position
                    index++;
                }
            }

            // Create the string
            s = span.ToString();

            // Set the normalize string
            return s.IsNormalized() ? s : s.Normalize();
        }

        /// <summary>
        /// Generates a unique name that starts with the specified <paramref name="prefix"/>
        /// </summary>
        /// <param name="prefix">The prefix</param>
        /// <returns></returns>
        public static string GenerateUniqueName(string prefix) => prefix + Guid.NewGuid().ToNormalizedString();

        /// <summary>
        /// Returns the specified <paramref name="s"/> with the first char converted to upper case
        /// </summary>
        /// <param name="s">The string</param>
        /// <returns></returns>
        public static string FirstCharToUpper(string s) => s.First().ToString().ToUpper() + s.Substring(1);

        /// <summary>
        /// Returns the specified <paramref name="s"/> with the first char converted to lower case
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToLower(string s) => s.First().ToString().ToLower() + s.Substring(1);

        /// <summary>
        /// Splits the specified <paramref name="s"/> to words based on the upper case letter.
        /// Acronyms are also taken into consideration.
        /// Ex.: ThisIsATestAPI => This, Is, A, Test, API
        /// </summary>
        /// <param name="s">The string</param>
        /// <returns></returns>
        public static string[] SplitCamelCase(string s)
        {
            s = mSplitCamelCaseFirstPassRegex
                .Replace(s, "${before} ${after}")
                .Trim();

            s = mSplitCamelCaseSecondPassRegex
                .Replace(s, "${before} ${after}")
                .Trim();

            return s.Split(' ');
        }

        /// <summary>
        /// Aggregates the specified words to a sentence meaning that
        /// the first letter of the first word is capital. Acronyms
        /// are also kept using capital letters.
        /// </summary>
        /// <param name="words">The words</param>
        /// <param name="shouldFirstCharBeCapital">A flag indicating whether the first character of the first word should be a capital letter</param>
        /// <returns></returns>
        public static string AggregateToSentence(string[] words, bool shouldFirstCharBeCapital = true)
        {
            var isFirst = shouldFirstCharBeCapital;

            return words.AggregateString(x =>
            {
                if (isFirst)
                {
                    isFirst = false;
                    return x;
                }

                if (x.Length != 1 && x.ToUpper() == x)
                    return x;

                return x.ToLower();
            }, " ");
        }

        /// <summary>
        /// Aggregates the specified <paramref name="values"/> into a <see cref="string"/>.
        /// NOTE: This method converts the <paramref name="values"/> to <see cref="string"/>
        ///       using the <see cref="object.ToString()"/> method!
        /// NOTE: This method uses ", " as the items separator!
        /// </summary>
        /// <param name="values">The values</param>
        /// <param name="separator">The separator</param>
        /// <returns></returns>
        public static string AggregateNonNullOrEmptyString(string separator, params string[] values) => AggregateNonNullOrEmptyString(separator, values.ToList());

        /// <summary>
        /// Aggregates the specified <paramref name="values"/> into a <see cref="string"/>.
        /// NOTE: This method converts the <paramref name="values"/> to <see cref="string"/>
        ///       using the <see cref="object.ToString()"/> method!
        /// NOTE: This method uses ", " as the items separator!
        /// </summary>
        /// <param name="values">The values</param>
        /// <param name="separator">The separator</param>
        /// <returns></returns>
        public static string AggregateNonNullOrEmptyString(string separator, IEnumerable<string> values) => values.Where(x => !x.IsNullOrEmpty()).AggregateString(separator);

        /// <summary>
        /// Compares the <paramref name="value"/> against the <paramref name="s"/> using the specified <paramref name="stringEqualityOperator"/>.
        /// Ex.: Operator = <see cref="StringEqualityOperator.Contains"/> -> <paramref name="s"/>.Contains(<paramref name="value"/>).
        /// </summary>
        /// <param name="stringEqualityOperator">The operator</param>
        /// <param name="s">The string value</param>
        /// <param name="value">The string value that will be used by the filter</param>
        /// <returns></returns>
        public static bool StringCompare(string s, StringEqualityOperator stringEqualityOperator, string value)
        {
            if (stringEqualityOperator == StringEqualityOperator.Contains)
                return s.Contains(value);
            else if (stringEqualityOperator == StringEqualityOperator.Equals)
                return s == value;
            else if (stringEqualityOperator == StringEqualityOperator.StartsWith)
                return s.StartsWith(value);
            else if (stringEqualityOperator == StringEqualityOperator.EndsWith)
                return s.EndsWith(value);
            else
            {
                try
                {
                    return Regex.Match(s, value).Success;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Removes the HTML tags from the specified <paramref name="s"/>
        /// </summary>
        /// <param name="s">The string value</param>
        /// <returns></returns>
        public static string RemoveXMLTags(string s)
            => mXMLTagsRegex.Replace(s, string.Empty);

        /// <summary>
        /// Removes the invalid file name characters from the specified <paramref name="s"/>
        /// </summary>
        /// <param name="s">The string value</param>
        /// <returns></returns>
        public static string RemoveInvalidFileNameCharacters(string s)
            => s.Where(x => !Path.GetInvalidPathChars().Contains(x)).AggregateString(string.Empty);

        /// <summary>
        /// Attempts to singularize the specified <paramref name="text"/> according to the rules of the English language.
        /// </summary>
        /// <param name="text">The text to singularize.</param>
        /// <param name="number">If the number is greater than 1 or less than 1, the text is not singularized, otherwise, the text is singularized</param>
        /// <returns>A string that consists of the text in its singularized form.</returns>
        public static string Singularize(string text, int number = 1)
        {
            if (text.IsNullOrEmpty())
                return text;

            if (number > 1 || number < 1)
                return text;
            else
            {
                if (text.EndsWith("ies"))
                    return text.Substring(0, text.Length - 3) + "y";
                else if (text.EndsWith("ves"))
                    return text.Substring(0, text.Length - 3) + "f";
                else if (text.EndsWith("s"))
                    return text.Substring(0, text.Length - 1);
            }

            return text;
        }

        /// <summary>
        /// Attempts to pluralize the specified <paramref name="text"/> according to the rules of the English language.
        /// </summary>
        /// <remarks>
        /// This function attempts to pluralize as many words as practical by following these rules:
        /// <list type="bullet">
        ///		<item><description>Words that end with "y" (but not with a vowel preceding the y) are pluralized by replacing the "y" with "ies".</description></item>
        ///		<item><description>Words that end with "us", "ss", "x", "ch" or "sh" are pluralized by adding "es" to the end of the text.</description></item>
        ///		<item><description>Words that end with "f" or "fe" are pluralized by replacing the "f(e)" with "ves".</description></item>
        ///	</list>
        /// </remarks>
        /// <param name="text">The text to pluralize.</param>
        /// <param name="number">If number is 1, the text is not pluralized, otherwise, the text is pluralized.</param>
        /// <returns>A string that consists of the text in its pluralized form.</returns>
        public static string Pluralize(string text, int number = 2)
        {
            if (text.IsNullOrEmpty())
                return text;

            if (number == 1)
                return text;
            else
            {
                if (text.EndsWith("y", StringComparison.OrdinalIgnoreCase) &&
                    !text.EndsWith("ay", StringComparison.OrdinalIgnoreCase) &&
                    !text.EndsWith("ey", StringComparison.OrdinalIgnoreCase) &&
                    !text.EndsWith("iy", StringComparison.OrdinalIgnoreCase) &&
                    !text.EndsWith("oy", StringComparison.OrdinalIgnoreCase) &&
                    !text.EndsWith("uy", StringComparison.OrdinalIgnoreCase))
                    return text.Substring(0, text.Length - 1) + "ies";
                else if (text.EndsWith("us", StringComparison.InvariantCultureIgnoreCase))
                    // http://en.wikipedia.org/wiki/Plural_form_of_words_ending_in_-us
                    return text + "es";
                else if (text.EndsWith("ss", StringComparison.InvariantCultureIgnoreCase))
                    return text + "es";
                else if (text.EndsWith("s", StringComparison.InvariantCultureIgnoreCase))
                    return text;
                else if (text.EndsWith("x", StringComparison.InvariantCultureIgnoreCase) ||
                         text.EndsWith("ch", StringComparison.InvariantCultureIgnoreCase) ||
                         text.EndsWith("sh", StringComparison.InvariantCultureIgnoreCase))
                    return text + "es";
                else if (text.EndsWith("f", StringComparison.InvariantCultureIgnoreCase) && text.Length > 1)
                    return text.Substring(0, text.Length - 1) + "ves";
                else if (text.EndsWith("fe", StringComparison.InvariantCultureIgnoreCase) && text.Length > 2)
                    return text.Substring(0, text.Length - 2) + "ves";
                else
                    return text + "s";
            }
        }

        #endregion
    }
}
