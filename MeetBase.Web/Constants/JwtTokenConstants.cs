using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBase.Web
{
    /// <summary>
    /// Constants related to JWT tokens
    /// </summary>
    public static class JwtTokenConstants
    {
        /// <summary>
        /// The name of the claim related to the university id
        /// </summary>
        public const string UniversityIdClaimType = "UniversityId";

        /// <summary>
        /// The name of the claim related to the department id
        /// </summary>
        public const string DepartmentIdClaimType = "DepartmentId";

        /// <summary>
        /// The name of the claim related to the secretary id
        /// </summary>
        public const string SecretaryIdClaimType = "SecretaryId";

        /// <summary>
        /// The name of the claim related to the professor id
        /// </summary>
        public const string ProfessorIdClaimType = "ProfessorId";

        /// <summary>
        /// The name of the claim related to the username
        /// </summary>
        public const string UsernameClaimType = "Username";
    }
}
