using Hangfire.Dashboard;
using LearnHub_Api.Abstractions.Consts;

namespace LearnHub_Api.Authentication.Filter
{
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
