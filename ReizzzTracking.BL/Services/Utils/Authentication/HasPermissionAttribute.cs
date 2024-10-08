using Microsoft.AspNetCore.Authorization;
using ReizzzTracking.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
