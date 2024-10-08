using System;
using System.Collections.Generic;

namespace ReizzzTracking.DAL.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public byte Gender { get; set; }

    public DateOnly Birthday { get; set; }

    public string? Bio { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<RoutineCollection> RoutineCollections { get; set; } = new List<RoutineCollection>();

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();

    public virtual ICollection<TodoSchedule> TodoSchedules { get; set; } = new List<TodoSchedule>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}
