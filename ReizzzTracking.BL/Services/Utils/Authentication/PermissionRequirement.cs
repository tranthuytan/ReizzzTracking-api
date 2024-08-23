using Microsoft.AspNetCore.Authorization;
using ReizzzTracking.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
