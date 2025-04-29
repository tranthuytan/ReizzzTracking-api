using Microsoft.Extensions.Logging;
using Quartz;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class JobSchedulerForNewEntity : IJob
    {
        private readonly ILogger<JobSchedulerForNewEntity> _logger;

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{nameof(JobSchedulerForNewEntity)} starting ...");
            _logger.LogInformation($"{nameof(JobSchedulerForNewEntity)} completed ...");
            return Task.CompletedTask;
        }
    }
}