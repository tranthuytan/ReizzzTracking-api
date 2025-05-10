using Microsoft.AspNetCore.Authorization;
using ReizzzTracking.DAL.Common.Enums;

namespace ReizzzTracking.BL.Services.Utils.Authentication
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
            :base(policy: permission.ToString())
        {
            
        }
    }
}
