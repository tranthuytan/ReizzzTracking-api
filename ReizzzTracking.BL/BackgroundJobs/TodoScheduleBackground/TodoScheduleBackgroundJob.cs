using Quartz;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.BackgroundJobs.TodoScheduleBackground
{
    public class TodoScheduleBackgroundJob : IJob
    {
        private readonly ITodoScheduleRepository _todoScheduleRepository;
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
        private Task SendNotification(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
