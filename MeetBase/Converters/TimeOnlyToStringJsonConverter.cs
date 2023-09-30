using System;
using System.Data;
using System.Globalization;

namespace MeetBase
{
    /// <summary>
    /// A <see cref="BaseTimeOnlyToStringJsonConverter{TValue}"/> used for converting a <see cref="TimeOnly"/> to a <see cref="string"/>
    /// </summary>
    public class TimeOnlyToStringJsonConverter : BaseTimeOnlyToStringJsonConverter<TimeOnly>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeOnlyToStringJsonConverter() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Converts the <paramref name="value"/> to the respective <see cref="TimeOnly"/>
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns></returns>
        protected override TimeOnly Convert(string value)
            => TimeOnly.ParseExact(value, SerializationFormat, CultureInfo.InvariantCulture);

        /// <summary>
        /// Writes the JSON representation of the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        protected override string Convert(TimeOnly value)
            => value.ToString(SerializationFormat, CultureInfo.InvariantCulture);

        #endregion
    }
}
