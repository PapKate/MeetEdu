using System.ComponentModel;

namespace MeetBase
{
    /// <summary>
    /// Provides enumeration over the company types
    /// </summary>
    public enum DepartmentType
    {
        /// <summary>
        /// Health sciences
        /// </summary>
        [Description("Health Sciences")]
        HealthSciences,

        /// <summary>
        /// Engineering
        /// </summary>
        [Description("Engineering")]
        Engineering,

        /// <summary>
        /// Natural sciences
        /// </summary>
        [Description("Natural Sciences")]
        NaturalSciences,

        /// <summary>
        /// Economics and business
        /// </summary>
        [Description("Economics & Business")]
        EconomicsAndBusiness,

        /// <summary>
        /// Agricultural sciences
        /// </summary>
        [Description("Agricultural Sciences")]
        AgriculturalSciences,

        /// <summary>
        /// Health rehabilitation sciences
        /// </summary>
        [Description("Health Rehabilitation Sciences")]
        HealthRehabilitationSciences
    }
}
