using LearnHub_Api.Abstractions.Consts;
using System.Security.Claims;

namespace LearnHub_Api.Extensions
{
    public static class UserExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal User)
        {
            return User.IsInRole(DefaultRoles.Admin);
        }
    }
}
