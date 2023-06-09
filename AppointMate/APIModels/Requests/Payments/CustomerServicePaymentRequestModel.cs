using System;

namespace AppointMate
{
    /// <summary>
    /// Represents a customer service payment
    /// </summary>
    public class CustomerServicePaymentRequestModel : BaseRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The amount
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// The payment method
        /// </summary>
        public EmbeddedPaymentMethodRequestModel? PaymentMethod { get; set; }

        /// <summary>
        /// The sum of the flat rate and the percent commission
        /// </summary>
        public decimal? CommissionAmount { get; set; }

        /// <summary>
        /// The amount accumulated by the flat rate commission
        /// </summary>
        public decimal? FlatRateCommissionAmount { get; set; }

        /// <summary>
        /// The amount accumulated by the commission
        /// </summary>
        public decimal? PercentCommissionAmount { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerServicePaymentRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Amount.HasValue ? Amount.Value.ToLocalizedCurrency() : string.Empty;

        #endregion
    }
}
