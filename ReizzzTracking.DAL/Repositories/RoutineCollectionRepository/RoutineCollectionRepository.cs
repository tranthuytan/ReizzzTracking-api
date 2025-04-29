using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.RoutineCollectionRepository
{
    public class RoutineCollectionRepository : BaseRepository<RoutineCollection>, IRoutineCollectionRepository
    {
        public RoutineCollectionRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }
    }
}
