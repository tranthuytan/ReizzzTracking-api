using System;
using System.Collections.Generic;

namespace ReizzzTracking.Entities;

public partial class UserTask
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? AppliedFor { get; set; }

    public string? Description { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? AppliedForNavigation { get; set; }
}
