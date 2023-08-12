using Atom.Blazor.Controls;

using Microsoft.AspNetCore.Components;

namespace AppointMate
{
    /// <summary>
    /// The header component
    /// </summary>
    public partial class Header 
    {
        #region Private Members

        /// <summary>
        /// The drop down menu for the account
        /// </summary>
        private DropDownMenu<RouteModel>? mAccountMenu;

        #endregion

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

        #region Protected Methods

        /// <summary>
        /// Method invoked after each time the component has been rendered
        /// </summary>
        /// <param name="firstRender">Set to true if this is the first time the method has been invoked on this component instance; otherwise false</param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            // If the drop down menu has no items...
            if (mAccountMenu is not null && mAccountMenu.ItemsCount == 0)   
            {
                var routes = new List<RouteModel>();
                // If it is not a connected user...
                if (Username.IsNullOrEmpty())
                {
                    routes.Add(new()
                    {
                        Name = AppointMateConstants.LogIn,
                        Route = "log in route"
                    });
                }
                // Else...
                else
                {
                    routes.AddRange(new List<RouteModel>()
                    {
                        new()
                        {
                            Name = AppointMateConstants.MyAccount,
                            Route = "MyAccount route"
                        },
                        new()
                        {
                            Name = AppointMateConstants.Appointments,
                            Route = "Appointments route"
                        },
                        new()
                        {
                            Name = AppointMateConstants.Favorites,
                            Route = "Favorites route"
                        },
                        new()
                        {
                            Name = AppointMateConstants.Settings,
                            Route = "Settings route"
                        },
                        new()
                        {
                            Name = AppointMateConstants.LogOut,
                            Route = "log out route"
                        }
                    });
                }

                mAccountMenu.AddRange(routes);
                mAccountMenu.MenuButton.HasAnimation = false;
                mAccountMenu.MenuButton.InvokeStateHasChanged();
            }

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
