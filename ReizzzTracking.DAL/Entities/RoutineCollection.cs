using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class RoutineCollection
{
    public long Id { get; set; }

    public long? CreatedBy { get; set; }

    public string? Name { get; set; }

    public bool? IsPublic { get; set; }
    public long CopyCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public virtual User? CreatedByNavigation { get; set; }
    public virtual ICollection<Routine>? Routines { get; set; }
}
