namespace MeetBase
{
    /// <summary>
    /// Represents a department contact message template
    /// </summary>
    public class DepartmentContactMessageTemplate
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="VectorName"/> property
        /// </summary>
        private string? mVectorName;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the vector component
        /// </summary>
        public string VectorName
        {
            get => mVectorName ?? string.Empty;
            set => mVectorName = value;
        }

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
