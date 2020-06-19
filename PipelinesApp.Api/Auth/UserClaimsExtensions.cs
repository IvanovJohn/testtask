namespace PipelinesApp.Api.Auth
{
    using System.Linq;
    using System.Security.Claims;

    internal static class UserClaimsExtensions
    {
        public const string UserIdClaimName = "userId";

        public static string GetUserIdFromClaims(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(p => p.Type == UserIdClaimName)?.Value;
        }
    }
}