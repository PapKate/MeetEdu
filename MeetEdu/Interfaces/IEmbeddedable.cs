namespace MeetEdu
{
    /// <summary>
    /// Provides abstractions for an entity that has an embedded version
    /// </summary>
    /// <typeparam name="TEmbeddedEntity">The embedded version of the entity</typeparam>
    public interface IEmbeddedable<TEmbeddedEntity>
        where TEmbeddedEntity : BaseEntity
    {
        #region Methods

        /// <summary>
        /// Creates and returns a <typeparamref name="TEmbeddedEntity"/> from the current entity
        /// </summary>
        /// <returns></returns>
        TEmbeddedEntity ToEmbeddedEntity();

        #endregion
    }
}
