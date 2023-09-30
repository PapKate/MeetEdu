using System.Globalization;

namespace MeetBase
{
    /// <summary>
    /// A <see cref="BaseDateOnlyToStringJsonConverter{TValue}"/> used for converting a <see cref="DateOnly"/> to a <see cref="string"/>
    /// </summary>
    public class DateOnlyToStringJsonConverter : BaseDateOnlyToStringJsonConverter<DateOnly>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DateOnlyToStringJsonConverter() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Converts the <paramref name="value"/> to the respective <see cref="DateOnly"/>
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns></returns>
        protected override DateOnly Convert(string value)
            => DateOnly.ParseExact(value, SerializationFormat, CultureInfo.InvariantCulture);

        /// <summary>
        /// Writes the JSON representation of the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        protected override string Convert(DateOnly value)
            => value.ToString(SerializationFormat, CultureInfo.InvariantCulture);

        #endregion
    }
}
