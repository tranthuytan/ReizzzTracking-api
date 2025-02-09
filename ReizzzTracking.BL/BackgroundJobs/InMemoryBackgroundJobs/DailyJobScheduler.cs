using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class DailyJobScheduler : IJob
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<DailyJobScheduler> _logger;

        public DailyJobScheduler(IRoutineRepository routineRepository,
                                        ISchedulerFactory schedulerFactory,
                                        ILogger<DailyJobScheduler> logger
            )
        {
            _routineRepository = routineRepository;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("DailyJobScheduler starting");
            await GetAllRoutineAndSetupBackgroundJob(context);
            _logger.LogInformation("DailyJobScheduler completed");
        }
        private async Task GetAllRoutineAndSetupBackgroundJob(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start of {nameof(DailyJobScheduler)}");
            var scheduler = await _schedulerFactory.GetScheduler();
            var nowTimeString = DateTime.UtcNow.AddHours(7).ToString("HH:mm");
            var routines = await _routineRepository.GetAll(x => string.Compare(x.StartTime, nowTimeString) == 1);
            foreach (var routine in routines)
            {
                IJobDetail routineJob = JobBuilder.Create<RoutineBackgroundJobScheduler>()
                                            .WithIdentity(nameof(RoutineBackgroundJobScheduler))
                                            .UsingJobData("routine", JsonConvert.SerializeObject(routine))
                                            .Build();
                ITrigger trigger = TriggerBuilder.Create()
                                        .WithIdentity(nameof(RoutineBackgroundJobScheduler))
                                        .StartNow()
                                        .Build();
                await scheduler.ScheduleJob(routineJob, trigger);
                _logger.LogInformation($"Completed {nameof(DailyJobScheduler)} for routine with id = {routine.Id}");
            }
        }
    }
}
