using MudBlazor;

namespace MeetCore.Shared
{
    public partial class MainLayout
    {
        #region Private Members

        /// <summary>
        /// The theme provider
        /// </summary>
        private MudTheme? mMeetCoreTheme;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainLayout() : base()
        {
            mMeetCoreTheme = new MudTheme()
            {
                Typography = new Typography()
                {
                    Default = new Default()
                    {
                        FontFamily = new[] { "Objektiv VF Trial", "sans-serif" }
                    }
                },
                Palette = new()
                {
                    
                }
            };
        }

        #endregion
    }
}
