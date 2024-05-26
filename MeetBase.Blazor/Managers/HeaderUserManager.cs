using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetBase.Blazor
{
    /// <summary>
    /// The header user manager service for displaying the user in the header
    /// </summary>
    public class HeaderUserManager
    {
        #region Public Properties

        /// <summary>
        /// The user color
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// The user username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The user image URL
        /// </summary>
        public Uri? ImageUrl { get; set; }

        /// <summary>
        /// A flag indicating whether a user is logged in or not
        /// </summary>
        public bool IsConnected { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public HeaderUserManager() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the manager values of the connected user
        /// </summary>
        /// <param name="username">The user username</param>
        /// <param name="imageUrl">The user image URL</param>
        /// <param name="color">The user color</param>
        public void SetValues(string username, Uri? imageUrl, string color)
        {
            Username = username;
            ImageUrl = imageUrl;
            Color = color;
            IsConnected = true;
        }

        /// <summary>
        /// Clears the manager values
        /// </summary>
        public void Clear()
        {
            Username = null;
            ImageUrl = null;
            Color = null;
            IsConnected = false;
        }

        #endregion
    }
}
