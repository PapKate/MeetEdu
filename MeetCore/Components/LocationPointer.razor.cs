using MeetBase;
using MeetBase.Blazor;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using Newtonsoft.Json;

using System.Reflection;

namespace MeetCore
{
    /// <summary>
    /// A component for setting a location
    /// </summary>
    public partial class LocationPointer
    {
        #region Private Members

        /// <summary>
        /// The longitude 
        /// </summary>
        private double mLongitude;

        /// <summary>
        /// The latitude 
        /// </summary>
        private double mLatitude;

        /// <summary>
        /// The member of the <see cref="Color"/> property
        /// </summary>
        private string? mColor;

        /// <summary>
        /// The icon button
        /// </summary>
        private IconButton? mIconButton;

        #endregion

        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The map id
        /// </summary>
        [Parameter]
        public string? MapId { get; set; }

        /// <summary>
        /// The map height
        /// </summary>
        [Parameter]
        public string MapHeight { get; set; } = "280px";

        /// <summary>
        /// The additional CSS classes
        /// </summary>
        [Parameter]
        public string? CssClasses { get; set; }

        /// <summary>
        /// A flag indicating whether the location is read only or not
        /// </summary>
        [Parameter]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        [Parameter]
        public Location? Location { get; set; }

        /// <summary>
        /// The color
        /// </summary>
        [Parameter]
        public string? Color 
        {
            get => mColor;
            set
            {
                // If the color is the same...
                if (mColor == value)
                    // Return...
                    return;

                // Sets the value
                mColor = value;

                if (mIconButton != null)
                {
                    mIconButton.BackColor = value;
                    mIconButton.ForeColor = value.DarkOrWhite();
                    mIconButton.InvokeStateHasChanged();
                }
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// The JS runtime service
        /// </summary>
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LocationPointer() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the location search result
        /// </summary>
        /// <param name="result"></param>
        [JSInvokable]
        public async void GetSearchResult(string result)
        {
            Console.Write(result);

            var geoLocation = JsonConvert.DeserializeObject<GeoLocation>(result);

            if (geoLocation is null)
                return;

            var locationData = geoLocation.Label.Split(',');
            if (Location is null)
                Location = new();

            Location.Address = locationData[0];
            Location.City = locationData[1];
            Location.Postcode = locationData[locationData.Length - 2];
            Location.Country = CountryCode.GR;
            Location.Latitude = geoLocation.Latitude;
            Location.Longitude = geoLocation.Longitude;

            mLatitude = geoLocation.Latitude;
            mLongitude = geoLocation.Longitude;

            await LocationChanged.InvokeAsync(Location);
        }

        /// <summary>
        /// Gets the location search result
        /// </summary>
        /// <param name="result"></param>
        [JSInvokable]
        public async void GetAddress(string result)
        {
            Console.Write(result);

            SetLocationFromLabel(result);
            Location!.Longitude = mLongitude;
            Location.Latitude = mLatitude;

            await LocationChanged.InvokeAsync(Location);
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // If the location does not exist or has default values...
            if(Location is null || (Location.Latitude == 0 && Location.Longitude == 0))
            {
                // Creates a new location
                Location = new();

                // Sets default coordinates to Patras
                mLatitude = 38.24614639482425;
                mLongitude = 21.73514322015635;
                
                // Returns
                return;
            }

            // Sets the coordinates
            mLatitude = Location.Latitude;
            mLongitude = Location.Longitude; 
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Task.Delay(100);
                
                // If the map is read only...
                if(IsReadOnly)
                {
                    // Shows the read only map
                    await JSRuntime.InvokeVoidAsync("ShowReadOnlyLeafletMap", MapId, mLatitude, mLongitude);

                    // Returns
                    return;
                }
                // Shows the map with the search bar
                await JSRuntime.InvokeVoidAsync("ShowLeafletSearchMap", MapId, mLatitude, mLongitude, DotNetObjectReference.Create(this));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the <see cref="Location"/> address according to the <paramref name="label"/>
        /// </summary>
        /// <param name="label">The label</param>
        private void SetLocationFromLabel(string label)
        {
            // Splits the data
            var locationData = label.Split(',');

            Location!.Address = locationData[0];
            Location.City = locationData[4];
            Location.Postcode = locationData[locationData.Length - 2];
            Location.Country = CountryCode.GR;
        }

        /// <summary>
        /// Sets the coordinates of the <see cref="Location"/>
        /// </summary>
        private async void SetCoordinates()
        {
            await JSRuntime.InvokeAsync<string>("GetAddressFromCoordinates", mLatitude, mLongitude, DotNetObjectReference.Create(this));
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the <see cref="Location"/> is changed
        /// </summary>
        [Parameter]
        public EventCallback<Location> LocationChanged { get; set; }

        #endregion
    }
}
