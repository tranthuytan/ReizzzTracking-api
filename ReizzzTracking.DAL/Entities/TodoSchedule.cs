using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class TodoSchedule
{
    public long Id { get; set; }
    public string Name { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public long? AppliedFor { get; set; }

    public bool? IsDone { get; set; }

    public int? EstimatedTime { get; set; }
    //TODO: find ways to convert ActualTime to HH:MM:SS
    public decimal? ActualTime { get; set; }

    public long? TimeUnitId { get; set; }

    public long? CategoryType { get; set; }

    public virtual User? AppliedForNavigation { get; set; }

    public virtual CategoryType? CategoryTypeNavigation { get; set; }

    public virtual TimeUnit? TimeUnit { get; set; }
}
