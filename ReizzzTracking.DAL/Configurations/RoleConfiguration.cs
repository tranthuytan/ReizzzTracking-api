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
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");
            builder.HasKey(e => e.Id).HasName("PK__roles__3214EC07C57C2FD8");
            builder.HasMany(x => x.UserRoles).WithOne();
            builder.HasMany(x => x.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasIndex(e => e.Name, "UQ__roles__737584F61C4A63D0").IsUnique();

            builder.Property(e => e.Name).HasMaxLength(50);
            builder.HasData(Role.GetValues());
        }
    }
}
