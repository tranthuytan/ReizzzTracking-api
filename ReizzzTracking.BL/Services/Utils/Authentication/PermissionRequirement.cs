using Microsoft.AspNetCore.Authorization;

namespace ReizzzTracking.BL.Services.Utils.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissions)
        {
            Permission = permissions;
        }

        public string Permission { get; set; }
    }
}
