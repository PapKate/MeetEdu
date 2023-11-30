namespace MeetBase
{
    /// <summary>
    /// Represents a department contact message template
    /// </summary>
    public class DepartmentContactMessageTemplate
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The image
        /// </summary>
        public Uri? Image { get; set; }

        /// <summary>
        /// The mean of contact
        /// </summary>
        public ContactMean ContactMean { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note 
        { 
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        #endregion
    }
}
