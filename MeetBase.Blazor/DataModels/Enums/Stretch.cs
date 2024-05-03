namespace MeetBase.Blazor
{
    /// <summary>
    /// The object-fit property is used to specify how an <img/> or <video/> should be resized to fit its container.
    /// </summary>
    public enum Stretch
    {
        /// <summary>
        /// The replaced content is sized to fill the element's content box. If necessary, the object will be stretched or squished to fit
        /// </summary>
        Fill,

        /// <summary>
        /// The replaced content is scaled to maintain its aspect ratio while fitting within the element's content box
        /// </summary>
        Container,

        /// <summary>
        /// The replaced content is sized to maintain its aspect ratio while filling the element's entire content box. The object will be clipped to fit
        /// </summary>
        Cover,

        /// <summary>
        /// The replaced content is not resized
        /// </summary>
        None,

        /// <summary>
        /// The content is sized as if none or contain were specified (would result in a smaller concrete object size)	
        /// </summary>
        ScaleDown,

        /// <summary>
        /// Sets this property to its default value.
        /// </summary>
        Initial,

        /// <summary>
        /// Inherits this property from its parent element. 
        /// </summary>
        Inherit
    }
}
