using MeetBase.Web;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace MeetCore
{
    /// <summary>
    /// The inbox page
    /// </summary>
    public partial class Secretary_InboxPage : BasePage
    {
        #region Private Members

        /// <summary>
        /// The <see cref="MudDialog"/> options
        /// </summary>
        private DialogOptions mDialogOptions = new() { FullWidth = true };

        private IEnumerable<DepartmentContactMessageResponseModel> mDepartmentMessages = new List<DepartmentContactMessageResponseModel>();

        #endregion

        #region Public Properties

        /// <summary>
        /// The secretary
        /// </summary>
        public SecretaryResponseModel Secretary => StateManager.Secretary!;

        /// <summary>
        /// The department
        /// </summary>
        public DepartmentResponseModel Department => StateManager.Department!;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The client
        /// </summary>
        [Inject]
        protected MeetCoreClient Client { get; set; } = default!;

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
        public Secretary_InboxPage() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            //var response = await Client.GetDepartmentContactMessagesAsync(new() { IncludeDepartments = new List<string>() { StateManager.Department!.Id } });

            //// If there was an error...
            //if (!response.IsSuccessful)
            //{
            //    // Show the error
            //    Snackbar.Add(response.ErrorMessage, Severity.Info);
            //    // Return
            //    return;
            //}
            //mContactMessages = response.Result;

            mDepartmentMessages = new List<DepartmentContactMessageResponseModel>()
            {
                new()
                {
                    FirstName = "Katherine",
                    LastName = "Papadopoulou",
                    Email = "mail@mail.com",
                    Message = "Το ΤΜΗΥ&Π ιδρύθηκε το 1979 και λειτουργεί από το 1980. Είναι το πρωτοπόρο τμήμΤο ΤΜΗΥ&Π ιδρύθηκε το 1979 και λειτουργεί από το 1980. Είναι το πρωτοπόρο τμήμα στον \r\nχώρο της Τεχνολογίας Υπολογιστών, Πληροφορικής και Επικοινωνιών στην Ελλάδα. \r\nΒρίσκεται ανάμεσα στα καλύτερα Πανεπιστημιακά Τμήματα στην Ελλάδα και με μεγάλες \r\nδιακρίσεις διεθνώς. Ο σκοπός και η αποστολή του είναι η διδασκαλία και η έρευνα στην \r\nεπιστήμη και τεχνολογία των υπολογιστών και η μελέτη των εφαρμογών τους. Οι σπουδές στο \r\nΤμήμα απαιτούν την ενεργή, συνεπή και δημιουργική συμμετοχή των φοιτητών στις \r\nεκπαιδευτικές δραστηριότητες του Προγράμματος Σπουδών, στοιχεία απαραίτητα για την \r\nεπιτυχή ολοκλήρωση των σπουδών, αλλά το τελικό αποτέλεσμα δικαιώνει τις προσδοκίες των \r\nφοιτητών. Το τμήμα είναι οργανωμένο σε τρεις τομείς:Το ΤΜΗΥ&Π ιδρύθηκε το 1979 και λειτουργεί από το 1980. Είναι το πρωτοπόρο τμήμα στον \r\nχώρο της Τεχνολογίας Υπολογιστών, Πληροφορικής και Επικοινωνιών στην Ελλάδα. \r\nΒρίσκεται ανάμεσα στα καλύτερα Πανεπιστημιακά Τμήματα στην Ελλάδα και με μεγάλες \r\nδιακρίσεις διεθνώς. Ο σκοπός και η αποστολή του είναι η διδασκαλία και η έρευνα στην \r\nεπιστήμη και τεχνολογία των υπολογιστών και η μελέτη των εφαρμογών τους. Οι σπουδές στο \r\nΤμήμα απαιτούν την ενεργή, συνεπή και δημιουργική συμμετοχή των φοιτητών στις \r\nεκπαιδευτικές δραστηριότητες του Προγράμματος Σπουδών, στοιχεία απαραίτητα για την \r\nεπιτυχή ολοκλήρωση των σπουδών, αλλά το τελικό αποτέλεσμα δικαιώνει τις προσδοκίες των \r\nφοιτητών. Το τμήμα είναι οργανωμένο σε τρεις τομείς:α στον \r\nχώρο της Τεχνολογίας Υπολογιστών, Πληροφορικής και Επικοινωνιών στην Ελλάδα. \r\nΒρίσκεται ανάμεσα στα καλύτερα Πανεπιστημιακά Τμήματα στην Ελλάδα και με μεγάλες \r\nδιακρίσεις διεθνώς. Ο σκοπός και η αποστολή του είναι η διδασκαλία και η έρευνα στην \r\nεπιστήμη και τεχνολογία των υπολογιστών και η μελέτη των εφαρμογών τους. Οι σπουδές στο \r\nΤμήμα απαιτούν την ενεργή, συνεπή και δημιουργική συμμετοχή των φοιτητών στις \r\nεκπαιδευτικές δραστηριότητες του Προγράμματος Σπουδών, στοιχεία απαραίτητα για την \r\nεπιτυχή ολοκλήρωση των σπουδών, αλλά το τελικό αποτέλεσμα δικαιώνει τις προσδοκίες των \r\nφοιτητών. Το τμήμα είναι οργανωμένο σε τρεις τομείς:",
                    DateCreated = DateTime.Now,
                }
            };

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private async void ViewMessage(DepartmentContactMessageResponseModel message)
        {
            var model = new DepartmentMesageModel()
            {
                Color = Department.Color,
                Model = message,
            };

            var parameters = new DialogParameters<DepartmentMessageDialog> { { x => x.Model, model } };

            // Creates and opens a dialog with the specified type
            var dialog = await DialogService.ShowAsync<DepartmentMessageDialog>(null, parameters, mDialogOptions);

            // Once the dialog is closed...
            // Gets the result
            var result = await dialog.Result;

            // If there is no result or the dialog was closed by canceling the inner actions...
            if (result is null || result.Canceled)
            {
                // Return
                return;
            }
        }

        #endregion
    }
}
