using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class CategoryType
{
    public long Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();

    public virtual ICollection<TodoSchedule> TodoSchedules { get; set; } = new List<TodoSchedule>();
}
