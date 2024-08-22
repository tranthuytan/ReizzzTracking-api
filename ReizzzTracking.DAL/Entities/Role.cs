using ReizzzTracking.DAL.Primitives;
using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public Role(long id, string name) : base(id, name)
    {
    }


    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<Permission> Permissions { get; set; }
}
