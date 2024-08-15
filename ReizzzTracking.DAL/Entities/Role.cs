using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class Role
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
