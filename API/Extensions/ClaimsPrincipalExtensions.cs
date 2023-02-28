using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public static int GetGymId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.GivenName)?.Value);
        }
    }
}