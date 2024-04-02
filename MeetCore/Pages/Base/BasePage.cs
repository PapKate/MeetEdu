using MeetBase.Blazor;
using MeetBase.Web;
using MeetBase;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Reflection;

namespace MeetCore
{
    /// <summary>
    /// The base page template
    /// </summary>
    public class BasePage : StateManagablePage
    {
        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The base form page
    /// </summary>
    public class FormPage : BasePage
    {
        #region Protected Members

        /// <summary>
        /// The list containing the vector components
        /// </summary>
        protected readonly List<Type> mImageVectors = new() { typeof(CoffeeContactVector), typeof(EmailContactVector), typeof(CellPhoneCalendarVector) };

        /// <summary>
        /// The descriptive text showing above the form
        /// </summary>
        protected string? mDescription = string.Empty;

        /// <summary>
        /// The descriptive text showing under the form icon
        /// </summary>
        protected string? mNote = string.Empty;

        /// <summary>
        /// The index of the selected vector component
        /// </summary>
        protected int mSelectedIndex = 0;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The dialog service
        /// </summary>
        [Inject]
        protected IDialogService DialogService { get; set; } = default!;

        /// <summary>
        /// The <see cref="MudBlazor"/> snack bar manager
        /// </summary>
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FormPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Saves the form description
        /// </summary>
        /// <param name="value"></param>
        protected void SaveFormDescription(string? value)
        {
            mDescription = value;
        }

        /// <summary>
        /// Saves the form note
        /// </summary>
        /// <param name="value"></param>
        protected void SaveFormNote(string? value)
        {
            mNote = value;
        }

        /// <summary>
        /// Sets the index of the selected vector in the carousel
        /// </summary>
        /// <param name="index"></param>
        protected void SetSelectedIndex(int index)
        {
            mSelectedIndex = index;
        }

        /// <summary>
        /// Refreshes the form to the default values
        /// </summary>
        protected void CancelButton_OnClickCore()
        {
            mDescription = string.Empty;
            mSelectedIndex = 0;

            CancelButton_OnClick();

            StateHasChanged();
        }

        /// <summary>
        /// Saves the form changes 
        /// </summary>
        protected void SaveButton_OnClickCore()
        {
            var vectorName = mImageVectors.ElementAt(mSelectedIndex).GetTypeInfo().FullName;
            SaveButton_OnClick(vectorName!);
        }

        /// <summary>
        /// Additional implementations for when the cancel button is clicked
        /// </summary>
        protected virtual void CancelButton_OnClick()
        {

        }

        /// <summary>
        /// Additional implementations for when the save button is clicked
        /// </summary>
        protected virtual void SaveButton_OnClick(string vectorName)
        {

        }

        #endregion
    }
}
