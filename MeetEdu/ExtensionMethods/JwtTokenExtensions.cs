using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MeetEdu
{
    /// <summary>
    /// Extension methods for the JWT token
    /// </summary>
    public static class JwtTokenExtensions
    {
        /// <summary>
        /// Searches the specified <paramref name="claims"/> collection for a claim with type <see cref="ClaimTypes.NameIdentifier"/>
        /// and extracts its value
        /// </summary>
        /// <param name="claims">The claims collection</param>
        /// <returns></returns>
        public static string GetUserId(this IEnumerable<Claim> claims) => claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        /// <summary>
        /// Gets the user id from the specified <paramref name="context"/>
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns></returns>
        public static string GetUserId(this HttpContext context) => context.User.Claims.GetUserId();

        /// <summary>
        /// Searches the specified <paramref name="context"/> claims with type <see cref="JwtTokenConstants.SecretaryIdClaimType"/>
        /// and extracts its value
        /// </summary>
        /// <param name="context">The HTTP context</param>
        /// <param name="secretaryId">The unique secretary id</param>
        /// <returns></returns>
        public static bool TryGetSecretaryId(this HttpContext context, [NotNullWhen(true)] out string? secretaryId)
        {
            // Set the id
            secretaryId = context.User.Claims.FirstOrDefault(x => x.Type == JwtTokenConstants.SecretaryIdClaimType)?.Value;
            
            // Return whether there operation was successful
            return secretaryId != null;
        }

        /// <summary>
        /// Searches the specified <paramref name="context"/> claims with type <see cref="JwtTokenConstants.ProfessorIdClaimType"/>
        /// and extracts its value
        /// </summary>
        /// <param name="context">The HTTP context</param>
        /// <param name="professorId">The unique professor id</param>
        /// <returns></returns>
        public static bool TryGetProfessorId(this HttpContext context, [NotNullWhen(true)] out string? professorId)
        {
            // Set the id
            professorId = context.User.Claims.FirstOrDefault(x => x.Type == JwtTokenConstants.ProfessorIdClaimType)?.Value;

            // Return whether there operation was successful
            return professorId != null;
        }
    }
}
