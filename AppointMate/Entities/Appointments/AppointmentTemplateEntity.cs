using AutoMapper;

using MongoDB.Bson;

namespace AppointMate
{
    /// <summary>
    /// Represents an appointment template document in the MongoDB
    /// </summary>
    public class AppointmentTemplateEntity: StandardEntity, IDescriptable, INoteable, IProfessorIdentifiable<ObjectId>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Description"/> property
        /// </summary>
        private string? mDescription;

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool HasRemoteOption { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description
        {
            get => mDescription ?? string.Empty;
            set => mDescription = value;
        }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentTemplateEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="AppointmentTemplateEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static AppointmentTemplateEntity FromRequestModel(ServiceRequestModel model)
        {
            var entity = new AppointmentTemplateEntity();

            DI.Mapper.Map(model, entity);

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="ServiceResponseModel"/> from the current <see cref="AppointmentTemplateEntity"/>
        /// </summary>
        /// <returns></returns>
        public ServiceResponseModel ToResponseModel() 
            => EntityHelpers.ToResponseModel<ServiceResponseModel>(this);

        #endregion
    }

    /// <summary>
    /// A minimal version of the <see cref="AppointmentTemplateEntity"/> that contains the fields that are 
    /// more frequently used when embedding documents in the MongoDB
    /// </summary>
    public class EmbeddedAppointmentTemplateEntity : EmbeddedStandardEntity, IProfessorIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The professor id
        /// </summary>
        public ObjectId ProfessorId { get; set; }

        /// <summary>
        /// A flag indicating whether it has a remote option or not
        /// </summary>
        public bool HasRemoteOption { get; set; }

        /// <summary>
        /// The duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedAppointmentTemplateEntity() : base()
        {

        }

        #endregion
    }
}
