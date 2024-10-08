using ReizzzTracking.DAL.Primitives;
using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class TimeUnit : Enumeration<TimeUnit>
{
    public static readonly TimeUnit Second = new(1, "Second");
    public static readonly TimeUnit Minute = new(2, "Minute");
    public static readonly TimeUnit Hour = new(3, "Hour");
    public TimeUnit(long id, string name) : base(id, name)
    {
    }

    public virtual ICollection<TimeExchange> TimeExchangeFromUnits { get; set; } = new List<TimeExchange>();

    public virtual ICollection<TimeExchange> TimeExchangeToUnits { get; set; } = new List<TimeExchange>();

    public virtual ICollection<TodoSchedule> TodoSchedules { get; set; } = new List<TodoSchedule>();
}
