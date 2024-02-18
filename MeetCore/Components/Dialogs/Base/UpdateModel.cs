using MeetBase;
using MeetBase.Web;
using Microsoft.AspNetCore.Http;

namespace MeetCore
{
    /// <summary>
    /// The base update dialog model
    /// </summary>
    public class UpdateModel<T>
        where T : IImageable, new()
    {
        #region Public Properties

        /// <summary>
        /// The model
        /// </summary>
        public T? Model { get; set; }

        /// <summary>
        /// The image
        /// </summary>
        public IFormFile? File { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public UpdateModel() : base()
        {

        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateModel(T model) : base()
        {
            Model = model;
        }

        #endregion
    }
}
