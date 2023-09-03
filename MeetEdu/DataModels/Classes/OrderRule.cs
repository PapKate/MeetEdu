using MeetBase;

using System.Diagnostics.CodeAnalysis;

namespace MeetEdu
{
    /// <summary>
    /// Provides information related to an order rule
    /// </summary>
    public record OrderRule : IFormattable, IParsable<OrderRule>
    {
        #region Constants

        private const string Separator = "|!|";

        #endregion

        #region Public Properties

        /// <summary>
        /// The order condition
        /// </summary>
        public OrderCondition OrderCondition { get; }

        /// <summary>
        /// The order by target
        /// </summary>
        public string OrderBy { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="orderCondition">The order condition</param>
        /// <param name="orderBy">The order by target</param>
        public OrderRule(OrderCondition orderCondition, string orderBy) : base()
        {
            OrderCondition = orderCondition;
            OrderBy = orderBy.NotNullOrEmpty();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{OrderCondition} - {OrderBy}";

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="format">
        /// The format to use. -or- A null reference (Nothing in Visual Basic) to use the
        /// default format defined for the type of the <see cref="IFormattable"/> implementation.
        /// </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value. -or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.
        /// </param>
        /// <returns></returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return $"{(int)OrderCondition}{Separator}{OrderBy}";
        }

        /// <summary>
        /// Parses a string into a value.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="provider">An object that provides culture-specific formatting information about s.</param>
        /// <returns></returns>
        public static OrderRule Parse(string s, IFormatProvider? provider)
        {
            var values = s.Split(Separator);

            return new OrderRule((OrderCondition)values[0].ToInt(), values[1]);
        }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s"/>.</param>
        /// <param name="result">
        /// When this method returns, contains the result of successfully parsing <paramref name="s"/> or an
        /// undefined value on failure.
        /// </param>
        /// <returns></returns>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out OrderRule result)
        {
            if (s.IsNullOrEmpty())
            {
                result = default;

                return false;
            }

            try
            {
                result = Parse(s, provider);

                return true;
            }
            catch
            {
                result = default;

                return false;
            }
        }

        #endregion
    }
}
