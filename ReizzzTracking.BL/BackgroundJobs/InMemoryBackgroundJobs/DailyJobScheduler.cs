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
    [DisallowConcurrentExecution]
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
            await GetAllRoutineAndSetupBackgroundJob(context);
        }
        private async Task GetAllRoutineAndSetupBackgroundJob(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start of {nameof(DailyJobScheduler)}");
            var scheduler = await _schedulerFactory.GetScheduler();
            var nowTimeString = DateTime.UtcNow.AddHours(7).ToString("HH:mm");
            // get all routine (with utc +7) that have  start time > current time
            var routines = await _routineRepository.GetAll(x => string.Compare(x.StartTime, nowTimeString) == 1);
            foreach (var routine in routines)
            {
                IJobDetail routineJob = JobBuilder.Create<RoutineBackgroundJobScheduler>()
                                            .WithIdentity(nameof(RoutineBackgroundJobScheduler) + $"routineId-{routine.Id}")
                                            .UsingJobData("routine", JsonConvert.SerializeObject(routine))
                                            .Build();
                string[] routineStartTimeSplitted = routine.StartTime.Split(':');
                DateTime routineStartTimeUtc7 = new DateTime(
                                                        DateTime.Now.Year,
                                                        DateTime.Now.Month,
                                                        DateTime.Now.Day,
                                                        int.Parse(routineStartTimeSplitted[0]),
                                                        int.Parse(routineStartTimeSplitted[1]),
                                                        DateTime.Now.Second);
                DateTime routineStartTimeUtc = routineStartTimeUtc7.AddHours(-7);
                DateTime utcNow = DateTime.UtcNow;
                TimeSpan timeDifference = routineStartTimeUtc - utcNow;
                var a = timeDifference.TotalMinutes;
                int timeDifferenceInMinute = (int)timeDifference.TotalMinutes;

                ITrigger trigger = TriggerBuilder.Create()
                                        .WithIdentity(nameof(RoutineBackgroundJobScheduler) + $"routineId-{routine.Id}")
                                        .StartAt(DateBuilder.FutureDate(timeDifferenceInMinute, IntervalUnit.Minute))
                                        .Build();
                await scheduler.ScheduleJob(routineJob, trigger);
                _logger.LogInformation($"Completed {nameof(DailyJobScheduler)} for routine with id = {routine.Id} at Utc = {routineStartTimeUtc}");
            }
        }
    }
}
