
namespace AppointMate
{
    /// <summary>
    /// The transaction response model
    /// </summary>
    public class TransactionResponseModel : StandardResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The type
        /// </summary>
        public TransactionType Type { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TransactionResponseModel() : base()
        {

        }

        #endregion
    }
}
