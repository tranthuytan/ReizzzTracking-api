using System;
using System.Collections.Generic;

namespace ReizzzTracking.Entities;

public partial class TodoSchedule
{
    public long Id { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public long? ToDoId { get; set; }

    public long? AppliedFor { get; set; }

    public bool? IsDone { get; set; }

    public int? EstimatedTime { get; set; }

    public int? ActualTime { get; set; }

    public long? TimeUnitId { get; set; }

    public long? CategoryType { get; set; }

    public virtual User? AppliedForNavigation { get; set; }

    public virtual CategoryType? CategoryTypeNavigation { get; set; }

    public virtual TimeUnit? TimeUnit { get; set; }
}
