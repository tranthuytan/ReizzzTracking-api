using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.RoutineRepository
{
    internal class RoutineRepository : BaseRepository<Routine>, IRoutineRepository
    {

        public RoutineRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }
    }
}
