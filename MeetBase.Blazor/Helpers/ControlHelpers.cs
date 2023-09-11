namespace MeetBase.Blazor
{
    /// <summary>
    /// Helper methods for the controls
    /// </summary>
    public static class ControlHelpers
    {
        #region Placement

        /// <summary>
        /// Sets the correct flex direction according to the <see cref="ContentPlacement"/> of the image
        /// </summary>
        /// <returns></returns>
        public static string SetFlexDirectionForContainer(ContentPlacement contentPlacement)
        {
            if (contentPlacement == ContentPlacement.Left)
                return "row";
            else if (contentPlacement == ContentPlacement.Right)
                return "row-reverse";
            else if (contentPlacement == ContentPlacement.Top)
                return "column";
            else
                return "column-reverse";
        }

        #endregion
    }
}
