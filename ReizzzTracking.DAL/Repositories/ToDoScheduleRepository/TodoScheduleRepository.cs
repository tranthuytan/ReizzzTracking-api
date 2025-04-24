using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.BaseRepository;

namespace ReizzzTracking.DAL.Repositories.TodoScheduleRepository
{
    public class TodoScheduleRepository : BaseRepository<TodoSchedule>, ITodoScheduleRepository
    {
        public TodoScheduleRepository(ReizzzTrackingV1Context context) : base(context)
        {
        }
    }
}
