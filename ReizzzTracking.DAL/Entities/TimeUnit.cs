using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class TimeUnit
{
    public long Id { get; set; }

    public DateTime? Name { get; set; }

    public virtual ICollection<TimeExchange> TimeExchangeFromUnits { get; set; } = new List<TimeExchange>();

    public virtual ICollection<TimeExchange> TimeExchangeToUnits { get; set; } = new List<TimeExchange>();

    public virtual ICollection<TodoSchedule> TodoSchedules { get; set; } = new List<TodoSchedule>();
}
