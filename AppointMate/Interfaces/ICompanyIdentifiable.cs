
namespace AppointMate
{
    /// <summary>
    /// Provides abstractions for an object that has a company id
    /// </summary>
    public interface ICompanyIdentifiable
    {
        /// <summary>
        /// The company id
        /// </summary>
        string CompanyId { get; set; }
    }
}
