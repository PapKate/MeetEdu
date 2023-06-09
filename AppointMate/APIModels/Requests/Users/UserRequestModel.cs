﻿
using AppointMate;

namespace AppointMate
{
    /// <summary>
    /// The user response model
    /// </summary>
    public abstract class UserRequestModel : BaseRequestModel, IImageable, IPhoneable
    {
        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// The email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// A flag indicating whether the email is confirmed or not
        /// </summary>
        public bool? IsEmailConfirmed { get; set; }

        /// <summary>
        /// The phone number
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        /// <summary>
        /// The birthday
        /// </summary>
        public DateOnly? DateOfBirth { get; set; }

        /// <summary>
        /// The shipping information
        /// </summary>
        public ShippingRequestModel? Shipping { get; set; }

        /// <summary>
        /// The billing information
        /// </summary>
        public BillingRequestModel? Billing { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserRequestModel() : base()
        {

        }

        #endregion
    }

    public class EmbeddedUserRequestModel : EmbeddedStandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// The small image URL
        /// </summary>
        public Uri? SmallImageUrl { get; set; }

        /// <summary>
        /// The image URL
        /// </summary>
        public Uri? NormalImageUrl { get; set; }

        /// <summary>
        /// The large image URL
        /// </summary>
        public Uri? LargeImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedUserRequestModel() : base()
        {

        }

        #endregion
    }
}
