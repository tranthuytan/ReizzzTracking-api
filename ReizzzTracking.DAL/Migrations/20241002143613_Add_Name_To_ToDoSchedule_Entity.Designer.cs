﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReizzzTracking.DAL.Entities;

#nullable disable

namespace ReizzzTracking.DAL.Migrations
{
    [DbContext(typeof(ReizzzTrackingV1Context))]
    [Migration("20241002143613_Add_Name_To_ToDoSchedule_Entity")]
    partial class Add_Name_To_ToDoSchedule_Entity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.CategoryType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__category__3214EC07ADA88A03");

                    b.ToTable("category_types", (string)null);
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "AddToDo"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "ReadToDo"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "UpdateToDo"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "DeleteToDo"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "AddRoutine"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "ReadRoutine"
                        },
                        new
                        {
                            Id = 7L,
                            Name = "UpdateRoutine"
                        },
                        new
                        {
                            Id = 8L,
                            Name = "DeleteRoutine"
                        });
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__roles__3214EC07C57C2FD8");

                    b.HasIndex(new[] { "Name" }, "UQ__roles__737584F61C4A63D0")
                        .IsUnique();

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Registered"
                        });
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.RolePermission", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("role_permissions", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 1L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 2L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 3L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 4L
                        });
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.Routine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CategoryType")
                        .HasColumnType("bigint");

                    b.Property<bool?>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("RoutineCollectionId")
                        .HasColumnType("bigint");

                    b.Property<string>("StartTime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("UsedBy")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK__routines__3214EC07877522F1");

                    b.HasIndex("CategoryType");

                    b.HasIndex("RoutineCollectionId");

                    b.HasIndex("UsedBy");

                    b.ToTable("routines", (string)null);
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.RoutineCollection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CopyCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<bool?>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id")
                        .HasName("PK__routine___3214EC071AF97170");

                    b.HasIndex("CreatedBy");

                    b.ToTable("routine_collections", (string)null);
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.TimeExchange", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("FromUnitId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Multiplier")
                        .HasColumnType("decimal(18, 10)");

                    b.Property<long?>("ToUnitId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK__time_exc__3214EC07B60632EC");

                    b.HasIndex("FromUnitId");

                    b.HasIndex("ToUnitId");

                    b.ToTable("time_exchanges", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FromUnitId = 1L,
                            Multiplier = 0.0166666666666666666666666667m,
                            ToUnitId = 2L
                        },
                        new
                        {
                            Id = 2L,
                            FromUnitId = 1L,
                            Multiplier = 0.0002777777777777777777777778m,
                            ToUnitId = 3L
                        },
                        new
                        {
                            Id = 3L,
                            FromUnitId = 2L,
                            Multiplier = 60m,
                            ToUnitId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            FromUnitId = 2L,
                            Multiplier = 0.0166666666666666666666666667m,
                            ToUnitId = 3L
                        },
                        new
                        {
                            Id = 5L,
                            FromUnitId = 3L,
                            Multiplier = 3600m,
                            ToUnitId = 1L
                        },
                        new
                        {
                            Id = 6L,
                            FromUnitId = 3L,
                            Multiplier = 60m,
                            ToUnitId = 2L
                        });
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.TimeUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id")
                        .HasName("PK__time_uni__3214EC0723133C3F");

                    b.ToTable("time_units", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Second"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Minute"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Hour"
                        });
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.TodoSchedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int?>("ActualTime")
                        .HasColumnType("int");

                    b.Property<long?>("AppliedFor")
                        .HasColumnType("bigint");

                    b.Property<long?>("CategoryType")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("EstimatedTime")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDone")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("datetime");

                    b.Property<long?>("TimeUnitId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK__todo_sch__3214EC07ECF0E426");

                    b.HasIndex("AppliedFor");

                    b.HasIndex("CategoryType");

                    b.HasIndex("TimeUnitId");

                    b.ToTable("todo_schedule", (string)null);
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__users__3214EC07F61BA3CE");

                    b.HasIndex(new[] { "Username" }, "UQ__users__536C85E4AB2DC2C6")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id", "UserId", "RoleId")
                        .HasName("PK__user_rol__F8E69A0D2F69DE35");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.RolePermission", b =>
                {
                    b.HasOne("ReizzzTracking.DAL.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReizzzTracking.DAL.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.Routine", b =>
                {
                    b.HasOne("ReizzzTracking.DAL.Entities.CategoryType", "CategoryTypeNavigation")
                        .WithMany("Routines")
                        .HasForeignKey("CategoryType")
                        .HasConstraintName("FK__routines__Catego__3D5E1FD2");

                    b.HasOne("ReizzzTracking.DAL.Entities.RoutineCollection", "RoutineCollectionNavigation")
                        .WithMany("Routines")
                        .HasForeignKey("RoutineCollectionId")
                        .HasConstraintName("FK__routines__routinecollections");

                    b.HasOne("ReizzzTracking.DAL.Entities.User", "UsedByNavigation")
                        .WithMany("Routines")
                        .HasForeignKey("UsedBy")
                        .HasConstraintName("FK__routines__UsedBy__3C69FB99");

                    b.Navigation("CategoryTypeNavigation");

                    b.Navigation("RoutineCollectionNavigation");

                    b.Navigation("UsedByNavigation");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.RoutineCollection", b =>
                {
                    b.HasOne("ReizzzTracking.DAL.Entities.User", "CreatedByNavigation")
                        .WithMany("RoutineCollections")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK__routine_c__Creat__3A81B327");

                    b.Navigation("CreatedByNavigation");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.TimeExchange", b =>
                {
                    b.HasOne("ReizzzTracking.DAL.Entities.TimeUnit", "FromUnit")
                        .WithMany("TimeExchangeFromUnits")
                        .HasForeignKey("FromUnitId")
                        .HasConstraintName("FK__time_exch__FromU__412EB0B6");

                    b.HasOne("ReizzzTracking.DAL.Entities.TimeUnit", "ToUnit")
                        .WithMany("TimeExchangeToUnits")
                        .HasForeignKey("ToUnitId")
                        .HasConstraintName("FK__time_exch__ToUni__4222D4EF");

                    b.Navigation("FromUnit");

                    b.Navigation("ToUnit");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.TodoSchedule", b =>
                {
                    b.HasOne("ReizzzTracking.DAL.Entities.User", "AppliedForNavigation")
                        .WithMany("TodoSchedules")
                        .HasForeignKey("AppliedFor")
                        .HasConstraintName("FK__todo_sche__Appli__3E52440B");

                    b.HasOne("ReizzzTracking.DAL.Entities.CategoryType", "CategoryTypeNavigation")
                        .WithMany("TodoSchedules")
                        .HasForeignKey("CategoryType")
                        .HasConstraintName("FK__todo_sche__Categ__403A8C7D");

                    b.HasOne("ReizzzTracking.DAL.Entities.TimeUnit", "TimeUnit")
                        .WithMany("TodoSchedules")
                        .HasForeignKey("TimeUnitId")
                        .HasConstraintName("FK__todo_sche__TimeU__3F466844");

                    b.Navigation("AppliedForNavigation");

                    b.Navigation("CategoryTypeNavigation");

                    b.Navigation("TimeUnit");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.UserRole", b =>
                {
                    b.HasOne("ReizzzTracking.DAL.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__user_role__RoleI__38996AB5");

                    b.HasOne("ReizzzTracking.DAL.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__user_role__UserI__37A5467C");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.CategoryType", b =>
                {
                    b.Navigation("Routines");

                    b.Navigation("TodoSchedules");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.RoutineCollection", b =>
                {
                    b.Navigation("Routines");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.TimeUnit", b =>
                {
                    b.Navigation("TimeExchangeFromUnits");

                    b.Navigation("TimeExchangeToUnits");

                    b.Navigation("TodoSchedules");
                });

            modelBuilder.Entity("ReizzzTracking.DAL.Entities.User", b =>
                {
                    b.Navigation("RoutineCollections");

                    b.Navigation("Routines");

                    b.Navigation("TodoSchedules");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
