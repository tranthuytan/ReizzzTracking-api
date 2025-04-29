using Quartz;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class TodoScheduleReminderBackgroundJob : IJob
    {
        private readonly TodoScheduleRepository _todoScheduleRepository;

        public TodoScheduleReminderBackgroundJob(TodoScheduleRepository todoScheduleRepository)
        {
            _todoScheduleRepository = todoScheduleRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
