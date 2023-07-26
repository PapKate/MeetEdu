namespace AppointMate
{
    /// <summary>
    /// Query arguments that provide ordering options
    /// </summary>
    public class OrderableAPIArgs : APIArgs, IOrderable
    {
        #region Public Properties

        /// <summary>
        /// The order condition.
        /// NOTE: The default is <see cref="OrderCondition.Descending"/>!
        /// </summary>
        public virtual OrderCondition Order { get; set; } = OrderCondition.Descending;

        /// <summary>
        /// The fields in order that should be used for ordering the results.
        /// </summary>
        public virtual IEnumerable<string>? OrderBy { get; set; }

        /// <summary>
        /// The rules
        /// </summary>
        IEnumerable<OrderRule>? IOrderable.Rules
        {
            get => OrderBy?.Where(x => !x.IsNullOrEmpty()).Distinct().Select(x => new OrderRule(Order, x)).ToList() ?? Enumerable.Empty<OrderRule>();

            set
            {
                if (value.IsNullOrEmpty())
                {
                    OrderBy = null;
                    return;
                }

                Order = value.First().OrderCondition;
                OrderBy = value.Select(x => x.OrderBy).ToList();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public OrderableAPIArgs() : base()
        {

        }

        #endregion
    }
}
