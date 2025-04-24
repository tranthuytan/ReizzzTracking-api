using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers;
using ReizzzTracking.DAL.Common.DateTimeToUtc;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    [DisallowConcurrentExecution]
    public class RoutineBackgroundJobScheduler : IJob
    {
        private readonly ILogger<RoutineBackgroundJobScheduler> _logger;
        private readonly RoutinePublisher _routinePublisher;

        private readonly ISchedulerFactory _schedulerFactory;
        public RoutineBackgroundJobScheduler(ILogger<RoutineBackgroundJobScheduler> logger,
                                                ISchedulerFactory schedulerFactory,
                                                RoutinePublisher routinePublisher)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
            _routinePublisher = routinePublisher;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start of {nameof(RoutineBackgroundJobScheduler)}");
            var dataMap = context.JobDetail.JobDataMap;
            var routineJson = dataMap.GetString("routine");
            var routine = JsonConvert.DeserializeObject<Routine>(routineJson);
            var routineStartAt = TimeMapper.FromTimeStringUtc7ToUtc(routine.StartTime);
            var timer = TimeMapper.GetDateTimeOffSetFromTimeOnly(routineStartAt);

            await _routinePublisher.PublishRoutineIsEnabledCheck(routine);

            _logger.LogInformation($"RoutineBackgroundJobScheduler completed of routineId = {routine.Id}, startTime = {routine.StartTime}");
        }
    }
}
