using Atom.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace MeetEdu
{
    /// <summary>
    /// The header component
    /// </summary>
    public partial class Header 
    {
        #region Public Properties

        /// <summary>
        /// The app name
        /// </summary>
        [Parameter]
        public string? AppName { get; set; }

        /// <summary>
        /// The back color
        /// </summary>
        [Parameter]
        public string? BackColor { get; set; }

        /// <summary>
        /// The fore color
        /// </summary>
        [Parameter]
        public string? ForeColor { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        [Parameter]
        public string? Username { get; set; }

        /// <summary>
        /// The user image URL
        /// </summary>
        [Parameter]
        public Uri? UserImageUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Header() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The route model
    /// </summary>
    public class RouteModel : INameable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="Route"/> property
        /// </summary>
        private string? mRoute;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name
        /// </summary>
        public string Name 
        { 
            get => mName ?? string.Empty;
            set => mName = value;
        }

        /// <summary>
        /// The route
        /// </summary>
        public string Route
        { 
            get => mRoute ?? string.Empty;
            set => mRoute = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RouteModel() : base()
        {

        }

        #endregion
    }
}
