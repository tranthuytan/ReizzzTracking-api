using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class TimeExchange
{
    public long Id { get; set; }

    public long? FromUnitId { get; set; }

    public long? ToUnitId { get; set; }

    public decimal? Multiplier { get; set; }

    public virtual TimeUnit? FromUnit { get; set; }

    public virtual TimeUnit? ToUnit { get; set; }
}
