using System.Text.RegularExpressions;

namespace MeetBase
{
    /// <summary>
    /// Constants related to <see cref="Regex"/>
    /// </summary>
    public static class RegexConstants
    {
        /// <summary>
        /// The pattern that is used by the <see cref="EmptyOrNumericOnlyStringRegex"/>
        /// </summary>
        public const string EmptyOrNumericOnlyStringRegexPattern = @"(^$)|(^[0-9]+$)";

        /// <summary>
        /// Regex used for validating an empty string or a string that contains only numbers
        /// </summary>
        public static readonly Regex EmptyOrNumericOnlyStringRegex = new Regex(EmptyOrNumericOnlyStringRegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The pattern that is used by the <see cref="EmailRegex"/>
        /// </summary>
        public const string EmailRegexPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        /// <summary>
        /// The regular expression for validating an email
        /// </summary>
        public static readonly Regex EmailRegex = new(EmailRegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The pattern that is used by the <see cref="E164PhoneNumberRegex"/>
        /// </summary>
        public const string E164PhoneNumberRegexPattern = @"^\s*\+\s*[1-9]\d{0,2}\s*\d{1,12}\s*$";

        /// <summary>
        /// The regular expression for validating a E164 phone number
        /// </summary>
        public static readonly Regex E164PhoneNumberRegex = new(E164PhoneNumberRegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The pattern that is used by the <see cref="PhoneNumberRegex"/>
        /// </summary>
        public const string PhoneNumberRegexPattern = @"^\s*[1-9]\d{0,2}\s*\d{1,12}\s*$";

        /// <summary>
        /// The regular expression for validating a phone number
        /// </summary>
        public static readonly Regex PhoneNumberRegex = new(PhoneNumberRegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The pattern that is used by the <see cref="NonFloatingNumberRegex"/>
        /// </summary>
        public const string NonFloatingNumberRegexPattern = @"^(\+|-)?\d+$";

        /// <summary>
        /// The regular expression for identifying a non floating point number
        /// </summary>
        public static readonly Regex NonFloatingNumberRegex = new(NonFloatingNumberRegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The pattern that is used by the <see cref="LatinRegex"/>
        /// </summary>
        public const string LatinRegexPattern = @"[A-Za-z]";

        /// <summary>
        /// The regular expression for identifying words with latin characters only
        /// </summary>
        public static readonly Regex LatinRegex = new Regex(LatinRegexPattern, RegexOptions.Compiled);

        /// <summary>
        /// The pattern that is used by the <see cref="HexRegex"/>
        /// </summary>
        public const string HexRegexPattern = @"\A\b[0-9a-fA-F]+\b\Z";

        /// <summary>
        /// The regular expression used for validating a hex value
        /// </summary>
        public static readonly Regex HexRegex = new Regex(HexRegexPattern, RegexOptions.Compiled);
    }
}
