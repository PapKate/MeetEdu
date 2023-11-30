using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The state management service
    /// </summary>
    public class StateManager
    {
        #region Public Properties
        
        /// <summary>
        /// The State property with an initial value
        /// </summary>
        public int Value { get; set; } = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StateManager() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The method that will be accessed by the sender component to update the state
        /// </summary>
        public void SetValue(int value)
        {
            Value = value;
            NotifyStateChanged();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Notifies that the state has changed
        /// </summary>
        protected void NotifyStateChanged() => OnStateChange?.Invoke();

        #endregion

        #region Public Events

        /// <summary>
        /// The event that will be raised for state changed
        /// </summary>
        public event Action? OnStateChange;

        #endregion
    }
}
