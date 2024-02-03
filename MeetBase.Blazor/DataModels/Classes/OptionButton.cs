using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public class OptionButton
    {
        #region Public Properties

        /// <summary>
        /// The foreground
        /// </summary>
        public string? Foreground { get; set; }

        /// <summary>
        /// The background
        /// </summary>
        public string? Background { get; set; }

        /// <summary>
        /// The vector source
        /// </summary>
        public VectorSource? VectorSource { get; set; }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public OptionButton(string? background, string? foreground, VectorSource? vectorSource) : base()
        {
            Background = background;
            Foreground = foreground;
            VectorSource = vectorSource;
        }

        /// <summary>
        /// Constructor with event
        /// </summary>
        public OptionButton(string? background, string? foreground, VectorSource? vectorSource, EventCallback onClick) : this(background, foreground, vectorSource)
        {
            OnClick = onClick;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the button is clicked
        /// </summary>
        public EventCallback OnClick { get; set; }

        #endregion
    }
}
