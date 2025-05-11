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
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");
            builder.HasKey(x => x.Id);
            List<Permission> permissions = Enum.GetValues<Common.Enums.Permission>().Select(x => new Permission
            {
                Id = (int)x,
                Name = x.ToString()
            }).ToList();
            builder.HasData(permissions);
        }
    }
}
