using Microsoft.AspNetCore.Components;

namespace MeetBase.Blazor
{
    public partial class PhoneNumberInput
    {
        #region Private Members

        /// <summary>
        /// A flag indicating whether the search box is visible or not
        /// </summary>
        private bool mIsSearchBoxVisible = false;

        private string mSearchBoxStyle = string.Empty;
        private string mSearchBoxBorderStyle = $"border: solid #E9E3E6 1px; filter: {Personalization.LightFilterDropShadow};";
        private string mSearchBoxOnlyBorderStyle = $"background: transparent;";

        private List<CountryData> mCountryData = new();

        #endregion

        #region Public Properties

        /// <summary>
        /// The value
        /// </summary>
        [Parameter]
        public PhoneNumber? Value { get; set; }

        /// <summary>
        /// The text
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// The phone number pattern
        /// </summary>
        [Parameter]
        public string? Pattern { get; set; } = "000 000 0000";

        /// <summary>
        /// The input placeholder
        /// </summary>
        [Parameter]
        public string? Placeholder { get; set; } = "Phone number";

        /// <summary>
        /// The country value
        /// </summary>
        [Parameter]
        public CountryData? Country { get; set; } 

        /// <summary>
        /// A flag indicating whether it is required or not
        /// </summary>
        [Parameter]
        public bool IsRequired { get; set; }

        /// <summary>
        /// A flag indicating whether it is read only or not
        /// </summary>
        [Parameter]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// A flag indicating whether it is has an underline or not
        /// </summary>
        [Parameter]
        public bool HasLine { get; set; }

        /// <summary>
        /// The css classes
        /// </summary>
        [Parameter]
        public string? CssClasses { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructors
        /// </summary>
        public PhoneNumberInput()
        {
            
        }

        #endregion

        #region Protected Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            mSearchBoxStyle = mSearchBoxBorderStyle;

            mCountryData = GlobalInfo.CountryData;
            var greece = GlobalInfo.CountryData.First(x => x.Country == "Greece");
            var cyprus = GlobalInfo.CountryData.First(x => x.Country == "Cyprus");

            if (Value is not null)
            {
                Country = mCountryData.FirstOrDefault(x => x.CountryCode == Value.CountryCode.ToString());
                Text = Value.Phone;
            }

            if (Country is null)
                Country = greece;

            mCountryData.Remove(cyprus);
            mCountryData.Insert(1, cyprus);

            mCountryData.Remove(greece);
            mCountryData.Insert(0, greece);
        }

        #endregion

        #region Private Methods

        private Task<IEnumerable<CountryData>> Search(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Task.FromResult(mCountryData.Take(5));

            var searchedData = mCountryData.Where(x => x.Country.ToLower().Contains(value.ToLower()) || x.CountryCode.Contains(value)).ToList();

            if(Country is not null)
            {
                searchedData.Remove(Country);
                searchedData.Insert(0, Country);
            }

            var searchedCount = searchedData.Count;

            searchedData.AddRange(mCountryData.Where(x => x != Country).Take(5 - searchedCount));

            IEnumerable<CountryData> result = searchedData;

            return Task.FromResult(result);
        }

        private void Flag_OnClick()
        {
            mIsSearchBoxVisible = !mIsSearchBoxVisible;
            if(mIsSearchBoxVisible)
                mSearchBoxStyle = mSearchBoxBorderStyle;
            else
                mSearchBoxStyle = mSearchBoxOnlyBorderStyle;
        }

        private void SearchInput_IsOpen(bool value)
        {
            if (value)
                mSearchBoxStyle = mSearchBoxOnlyBorderStyle;
            else
            {
                mIsSearchBoxVisible = false;
                mSearchBoxStyle = mSearchBoxBorderStyle;
            }
        }

        private void Search_ValueChanged(CountryData data)
        {
            Country = data;
            mIsSearchBoxVisible = false;
            OnValueChanged();
        }

        private void TextInput_ValueChanged(string value)
        {
            Text = value;
            OnValueChanged();
        }

        private async void OnValueChanged()
        {
            Value = new PhoneNumber(Country!.CountryCode, Text!);
            await ValueChanged.InvokeAsync();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fires when the <see cref="Value"/> is changed
        /// </summary>
        [Parameter]
        public EventCallback<PhoneNumber> ValueChanged { get; set; }

        #endregion
    }
}
