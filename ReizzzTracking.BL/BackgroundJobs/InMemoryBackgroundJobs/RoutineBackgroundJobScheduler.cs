using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers;
using ReizzzTracking.DAL.Common.DateTimeToUtc;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.UserRepository;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    [DisallowConcurrentExecution]
    public class RoutineBackgroundJobScheduler : IJob
    {
        private readonly ILogger<RoutineBackgroundJobScheduler> _logger;
        private readonly RoutinePublisher _routinePublisher;
        private readonly IUserRepository _userRepository;

        private readonly ISchedulerFactory _schedulerFactory;
        public RoutineBackgroundJobScheduler(ILogger<RoutineBackgroundJobScheduler> logger,
                                                ISchedulerFactory schedulerFactory,
                                                RoutinePublisher routinePublisher,
                                                IUserRepository userRepository)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
            _routinePublisher = routinePublisher;
            _userRepository = userRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start of {nameof(RoutineBackgroundJobScheduler)}");
            var dataMap = context.JobDetail.JobDataMap;
            var routineJson = dataMap.GetString("routine");
            if (routineJson is not null)
            {
                var routine = JsonConvert.DeserializeObject<Routine>(routineJson);
                if (routine is not null)
                {

                    var routineUser = await _userRepository.Find(routine.CreatedBy);
                    if (routine is not null && routine.IsActive == true)
                    {
                        BackgroundRoutineCheckedEvent backgroundRoutineCheckedEvent = new BackgroundRoutineCheckedEvent
                        {
                            Id = routine.Id,
                            UserName = routineUser!.Name!,
                            UserEmail = routineUser!.Email!,
                            StartTime = routine.StartTime,
                            Name = routine.Name,
                            IsPublic = routine.IsPublic,
                            IsActive = routine.IsActive,
                            CategoryType = routine.CategoryType,
                            RoutineCollectionId = routine.RoutineCollectionId
                        };
                        await _routinePublisher.PublishRoutineIsEnabledCheck(backgroundRoutineCheckedEvent);

                    }

                    _logger.LogInformation($"RoutineBackgroundJobScheduler completed of routineId = {routine!.Id}, startTime = {routine.StartTime}");
                }
            }
        }
    }
}
