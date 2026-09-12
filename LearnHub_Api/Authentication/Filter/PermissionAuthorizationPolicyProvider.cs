using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace LearnHub_Api.Authentication.Filter
{
    public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
    {
        // Permission policies are built dynamically the first time each permission
        // is requested. AuthorizationOptions.AddPolicy is not thread-safe, so we
        // cache them ourselves in a ConcurrentDictionary to avoid a race between
        // concurrent requests resolving the same permission for the first time.
        private readonly ConcurrentDictionary<string, AuthorizationPolicy> _permissionPolicies = new();

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
                return policy;

            return _permissionPolicies.GetOrAdd(policyName, name =>
                new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(name))
                    .Build());
        }
    }
}
