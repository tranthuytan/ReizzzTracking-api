﻿using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class UserRole
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
