using Quartz;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
