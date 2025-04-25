using System;
using System.Collections.Generic;
using ReizzzTracking.DAL.Primitives;

namespace ReizzzTracking.DAL.Entities;

public partial class CategoryType : Enumeration<CategoryType>
{
    public static readonly CategoryType Routine = new(1,"Routine");
    public static readonly CategoryType ToDo = new(2,"ToDo");
    public CategoryType(long id, string name) : base(id, name)
    {
    }

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();

    public virtual ICollection<TodoSchedule> TodoSchedules { get; set; } = new List<TodoSchedule>();
}
