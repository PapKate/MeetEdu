namespace MeetCore
{
    /// <summary>
    /// The profile page
    /// </summary>
    public partial class Professor_ProfilePage : StateManagablePage
    {
        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="firstRender"><inheritdoc/></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                var test = 0;
                if (StateManager is not null)
                    test = StateManager.Value;
            }
        }

        #endregion
    }
}
