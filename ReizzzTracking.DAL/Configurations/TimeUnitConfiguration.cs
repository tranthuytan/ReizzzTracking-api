using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Configurations
{
    public class TimeUnitConfiguration : IEntityTypeConfiguration<TimeUnit>
    {
        public void Configure(EntityTypeBuilder<TimeUnit> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__time_uni__3214EC0723133C3F");
            builder.ToTable("time_units");
            builder.Property(e => e.Name).HasColumnType("nvarchar(20)");
            builder.HasData(TimeUnit.GetValues());
        }
    }
}
