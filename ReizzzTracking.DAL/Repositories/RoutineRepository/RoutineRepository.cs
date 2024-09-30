using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Repositories.RoutineRepository
{
    internal class RoutineRepository : BaseRepository<Routine>, IRoutineRepository
    {

        public RoutineRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }
    }
}
