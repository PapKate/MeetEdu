namespace MeetEdu
{
    /// <summary>
    /// Represents a phone number
    /// </summary>
    public class PhoneNumber : IEquatable<PhoneNumber>
    {
        #region Public Properties

        /// <summary>
        /// The country code
        /// </summary>
        public int CountryCode { get; }

        /// <summary>
        /// The phone
        /// </summary>
        public string Phone { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="countryCode">The country code</param>
        /// <param name="phone">The phone number</param>
        public PhoneNumber(int countryCode, string phone)
        {
            CountryCode = countryCode;
            Phone = phone;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether the specified <paramref name="obj"/> is equal to the current object
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is PhoneNumber phoneNumber)
                return Equals(phoneNumber);

            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">The other object</param>
        /// <returns></returns>
        public bool Equals(PhoneNumber? other)
        {
            if (other is null)
                return false;

            return other.Phone == Phone && other.CountryCode == CountryCode;
        }

        /// <summary>
        /// Returns a string that contains both the country code and phone number.
        /// Ex.: 30 6969696969, 1 6969696969
        /// </summary>
        /// <returns></returns>
        public string ToPhoneNumberString() => $"{CountryCode} {Phone}";

        /// <summary>
        /// Returns a string that represents the E.164 phone number.
        /// Ex.: +30 6969696969, +16 969696969
        /// </summary>
        /// <returns></returns>
        public string ToE164PhoneNumberString() => $"+{ToPhoneNumberString()}";

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToPhoneNumberString();

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(CountryCode, Phone);

        #endregion

        #region Operators

        /// <summary>
        /// Determines whether two specified <see cref="PhoneNumber"/>s have the same value
        /// </summary>
        /// <param name="obj1">The first phone number</param>
        /// <param name="obj2">The second phone number</param>
        /// <returns></returns>
        public static bool operator ==(PhoneNumber? obj1, PhoneNumber? obj2) => Equals(obj1, obj2);

        /// <summary>
        /// Determines whether tow specified <see cref="PhoneNumber"/>s have different values
        /// </summary>
        /// <param name="obj1">The first phone number</param>
        /// <param name="obj2">The second phone number</param>
        /// <returns></returns>
        public static bool operator !=(PhoneNumber? obj1, PhoneNumber? obj2) => !(obj1 == obj2);

        /// <summary>
        /// Returns the string representation of the specified <paramref name="phoneNumber"/>
        /// </summary>
        /// <param name="phoneNumber">The phone number</param>
        public static implicit operator string(PhoneNumber? phoneNumber) => phoneNumber?.ToString() ?? string.Empty;

        #endregion
    }
}
