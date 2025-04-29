using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReizzzTracking.DAL.Common.Enums;
using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Permission = ReizzzTracking.DAL.Common.Enums.Permission;

namespace ReizzzTracking.DAL.Configurations
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permissions");
            builder.HasKey(x => new { x.RoleId, x.PermissionId });
            builder.HasData(
                Create(Role.Registered, Permission.AddToDo),
                Create(Role.Registered, Permission.ReadToDo),
                Create(Role.Registered, Permission.UpdateToDo),
                Create(Role.Registered, Permission.DeleteToDo),
                Create(Role.Registered, Permission.AddRoutine),
                Create(Role.Registered, Permission.ReadRoutine),
                Create(Role.Registered, Permission.UpdateRoutine),
                Create(Role.Registered, Permission.DeleteRoutine)
            );
        }
        private static RolePermission Create(Role role, Permission permission)
        {
            return new RolePermission
            {
                RoleId = role.Id,
                PermissionId = (long)permission
            };
        }
    }
}
