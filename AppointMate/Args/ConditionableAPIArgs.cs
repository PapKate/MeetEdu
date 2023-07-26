using System;
using System.Collections.Generic;
using System.Linq;

namespace AppointMate
{
    /// <summary>
    /// Query arguments that provide conditional based filtering options.
    /// </summary>
    public class ConditionableAPIArgs : OrderableAPIArgs, IConditionable
    {
        #region Public Properties

        /// <summary>
        /// The condition.
        /// </summary>
        public Condition Condition { get; set; } = Condition.AND;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConditionableAPIArgs() : base()
        {

        }

        #endregion
    }
}
