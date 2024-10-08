using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Configurations
{
    public class TimeExchangeConfiguration : IEntityTypeConfiguration<TimeExchange>
    {
        public void Configure(EntityTypeBuilder<TimeExchange> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__time_exc__3214EC07B60632EC");

            builder.ToTable("time_exchanges");

            builder.Property(e => e.Multiplier).HasColumnType("decimal(18, 10)");

            builder.HasOne(d => d.FromUnit).WithMany(p => p.TimeExchangeFromUnits)
                .HasForeignKey(d => d.FromUnitId)
                .HasConstraintName("FK__time_exch__FromU__412EB0B6");

            builder.HasOne(d => d.ToUnit).WithMany(p => p.TimeExchangeToUnits)
                .HasForeignKey(d => d.ToUnitId)
                .HasConstraintName("FK__time_exch__ToUni__4222D4EF");
            builder.HasData(new List<TimeExchange>()
            {
                new TimeExchange { Id = 1, FromUnitId = TimeUnit.Second.Id, ToUnitId = TimeUnit.Minute.Id, Multiplier = (decimal)1/60 },
                new TimeExchange { Id = 2, FromUnitId = TimeUnit.Second.Id, ToUnitId = TimeUnit.Hour.Id, Multiplier = (decimal)1/3600 },
                new TimeExchange { Id = 3, FromUnitId = TimeUnit.Minute.Id, ToUnitId = TimeUnit.Second.Id, Multiplier = (decimal)60 },
                new TimeExchange { Id = 4, FromUnitId = TimeUnit.Minute.Id, ToUnitId = TimeUnit.Hour.Id, Multiplier = (decimal)1/60 },
                new TimeExchange { Id = 5, FromUnitId = TimeUnit.Hour.Id, ToUnitId = TimeUnit.Second.Id, Multiplier = (decimal)3600 },
                new TimeExchange { Id = 6, FromUnitId = TimeUnit.Hour.Id, ToUnitId = TimeUnit.Minute.Id, Multiplier = (decimal)60 },
            });
        }
    }
}
