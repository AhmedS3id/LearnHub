using Hangfire.Dashboard;
using LearnHub_Api.Abstractions.Consts;

namespace LearnHub_Api.Authentication.Filter
{
    /// <summary>
    /// Restricts access to the Hangfire dashboard ("/jobs") to authenticated users
    /// in the existing Admin role, reusing the same Identity/JWT principal that
    /// secures the rest of the API instead of introducing a separate auth scheme.
    /// </summary>
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.Identity is { IsAuthenticated: true }
                && httpContext.User.IsInRole(DefaultRoles.Admin);
        }
    }
}
