﻿using ReizzzTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.ViewModels.RoutineViewModel
{
    public class RoutineAddViewModel
    {
        public string Name { get; set; } = "";
        public string StartTimeString { get; set; } = "";
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public long CategoryTypeId { get; set; }
        public long? RoutineCollectionId { get; set; }
        public Routine ToRoutine(RoutineAddViewModel routineVM)
        {
            //format to HH:MM format
            if (routineVM.StartTimeString.Trim()[1].Equals(":"))
            {
                routineVM.StartTimeString=string.Concat("0",routineVM.StartTimeString);
            }
            return new Routine
            {
                Name = routineVM.Name,
                StartTime = routineVM.StartTimeString,
                IsPublic = routineVM.IsPublic,
                IsActive = routineVM.IsActive,
                CategoryType = routineVM.CategoryTypeId,
                RoutineCollectionId = routineVM.RoutineCollectionId,
            };
        }
    }
}
