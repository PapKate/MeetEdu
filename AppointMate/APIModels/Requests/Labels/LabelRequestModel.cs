namespace AppointMate
{
    /// <summary>
    /// Request model used for a label
    /// </summary>
    public class LabelRequestModel : StandardRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The description
        /// </summary>
        public string? Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelRequestModel()
        {

        }

        #endregion
    }
}
