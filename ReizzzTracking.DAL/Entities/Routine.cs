using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class Routine
{
    public long Id { get; set; }

    public string? StartTime { get; set; }

    public string? Name { get; set; }

    public bool? IsPublic { get; set; }

    public long? UsedBy { get; set; }

    public long? CategoryType { get; set; }
    public long? RoutineCollectionId { get; set; }

    public virtual CategoryType? CategoryTypeNavigation { get; set; }

    public virtual User? UsedByNavigation { get; set; }
    public virtual RoutineCollection? RoutineCollectionNavigation { get; set; }
}
