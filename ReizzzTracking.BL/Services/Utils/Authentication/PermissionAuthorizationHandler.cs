using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ReizzzTracking.BL.Services.PermissionService;
using System.Security.Claims;

namespace ReizzzTracking.BL.Services.Utils.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userId, out var parsedUserId))
            {
                return;
            }
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IUserService _permissionService = scope.ServiceProvider.GetRequiredService<IUserService>();
                HashSet<string> permissions = await _permissionService.GetPermissionsAsync(parsedUserId);
                if (permissions.Contains(requirement.Permission))
                {
                    context.Succeed(requirement);
                }
            }
            return;
        }
    }
}
