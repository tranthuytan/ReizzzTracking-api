﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.PermissionService
{
    public interface IUserService
    {
        Task<HashSet<string>> GetPermissionsAsync(long userId);
    }
}
