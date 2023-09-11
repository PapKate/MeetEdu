﻿using Microsoft.AspNetCore.Components;

namespace MeetCore
{
    public partial class Index
    {
        #region Public Properties

        /// <summary>
        /// The username
        /// </summary>
        [Parameter]
        public string? Username { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        [Parameter]
        public string? Password { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Index() : base()
        {

        }

        #endregion

    }
}
