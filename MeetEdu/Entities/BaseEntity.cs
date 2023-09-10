﻿using MeetBase;

using MongoDB.Bson;

namespace MeetEdu
{
    /// <summary>
    /// The base for a document in the MongoDB
    /// </summary>
    public abstract class BaseEntity : IIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        public ObjectId Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseEntity() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The base for all the embedded entities
    /// </summary>
    public class EmbeddedBaseEntity : BaseEntity, IEmbeddableIdentifiable<ObjectId>
    {
        #region Public Properties

        /// <summary>
        /// The id of the entity that was used for creating the current 
        /// </summary>
        public ObjectId Source { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmbeddedBaseEntity() : base()
        {

        }

        #endregion
    }
}
