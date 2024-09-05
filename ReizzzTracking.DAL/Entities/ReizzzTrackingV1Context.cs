using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReizzzTracking.DAL.Entities;

public partial class ReizzzTrackingV1Context : DbContext
{
    public ReizzzTrackingV1Context()
    {
    }

    public ReizzzTrackingV1Context(DbContextOptions<ReizzzTrackingV1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryType> CategoryTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Routine> Routines { get; set; }

    public virtual DbSet<RoutineCollection> RoutineCollections { get; set; }

    public virtual DbSet<TimeExchange> TimeExchanges { get; set; }

    public virtual DbSet<TimeUnit> TimeUnits { get; set; }

    public virtual DbSet<TodoSchedule> TodoSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserTask> UserTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=(local);database=reizzz_tracking_v1;uid=sa;pwd=thuytan123;TrustServerCertificate=True;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReizzzTrackingV1Context).Assembly);


        modelBuilder.Entity<Routine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__routines__3214EC07877522F1");

            entity.ToTable("routines");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StartTime).HasMaxLength(50);

            entity.HasOne(d => d.CategoryTypeNavigation).WithMany(p => p.Routines)
                .HasForeignKey(d => d.CategoryType)
                .HasConstraintName("FK__routines__Catego__3D5E1FD2");

            entity.HasOne(r => r.RoutineCollectionNavigation).WithMany(rc => rc.Routines)
                .HasForeignKey(r => r.RoutineCollectionId)
                .HasConstraintName("FK__routines__routinecollections");

            entity.HasOne(e => e.UsedByNavigation).WithMany(u=>u.Routines)
                .HasForeignKey(e => e.UsedBy)
                .HasConstraintName("FK__routines__UsedBy__3C69FB99");
        });

        modelBuilder.Entity<RoutineCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__routine___3214EC071AF97170");

            entity.ToTable("routine_collections");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoutineCollections)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__routine_c__Creat__3A81B327");
        });

        modelBuilder.Entity<TimeExchange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__time_exc__3214EC07B60632EC");

            entity.ToTable("time_exchanges");

            entity.Property(e => e.Multiplier).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.FromUnit).WithMany(p => p.TimeExchangeFromUnits)
                .HasForeignKey(d => d.FromUnitId)
                .HasConstraintName("FK__time_exch__FromU__412EB0B6");

            entity.HasOne(d => d.ToUnit).WithMany(p => p.TimeExchangeToUnits)
                .HasForeignKey(d => d.ToUnitId)
                .HasConstraintName("FK__time_exch__ToUni__4222D4EF");
        });

        modelBuilder.Entity<TimeUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__time_uni__3214EC0723133C3F");

            entity.ToTable("time_units");

            entity.Property(e => e.Name).HasColumnType("datetime");
        });

        modelBuilder.Entity<TodoSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__todo_sch__3214EC07ECF0E426");

            entity.ToTable("todo_schedule");

            entity.Property(e => e.EndAt).HasColumnType("datetime");
            entity.Property(e => e.StartAt).HasColumnType("datetime");

            entity.HasOne(d => d.AppliedForNavigation).WithMany(p => p.TodoSchedules)
                .HasForeignKey(d => d.AppliedFor)
                .HasConstraintName("FK__todo_sche__Appli__3E52440B");

            entity.HasOne(d => d.CategoryTypeNavigation).WithMany(p => p.TodoSchedules)
                .HasForeignKey(d => d.CategoryType)
                .HasConstraintName("FK__todo_sche__Categ__403A8C7D");

            entity.HasOne(d => d.TimeUnit).WithMany(p => p.TodoSchedules)
                .HasForeignKey(d => d.TimeUnitId)
                .HasConstraintName("FK__todo_sche__TimeU__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC07F61BA3CE");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UQ__users__536C85E4AB2DC2C6").IsUnique();

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId, e.RoleId }).HasName("PK__user_rol__F8E69A0D2F69DE35");

            entity.ToTable("user_roles");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_role__RoleI__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_role__UserI__37A5467C");
        });

        modelBuilder.Entity<UserTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_tas__3214EC0768926AE1");

            entity.ToTable("user_tasks");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Remark).HasMaxLength(250);

            entity.HasOne(d => d.AppliedForNavigation).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.AppliedFor)
                .HasConstraintName("FK__user_task__Appli__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
