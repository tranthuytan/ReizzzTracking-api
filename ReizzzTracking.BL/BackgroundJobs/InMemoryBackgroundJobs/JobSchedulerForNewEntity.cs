using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class JobSchedulerForNewEntity : IJob
    {
        private readonly ILogger<JobSchedulerForNewEntity> _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        public JobSchedulerForNewEntity(ILogger<JobSchedulerForNewEntity> logger,
                                        ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{nameof(JobSchedulerForNewEntity)} starting ...");

            var scheduler = await _schedulerFactory.GetScheduler();
            var jobType = context.JobDetail.JobDataMap.GetString("jobType");
            if (jobType is null)
            {
                return;
            }

            if (jobType.Equals(typeof(Routine).ToString()))
            {
                var routineJson = context.JobDetail.JobDataMap.GetString("routine");
                if (routineJson is not null)
                {
                    var routine = JsonConvert.DeserializeObject<Routine>(routineJson);
                    if (routine is not null)
                    {
                        JobKey jobKey = JobKey.Create(nameof(RoutineBackgroundJobScheduler) + $"routineId-{routine.Id}", "group1");
                        var job = JobBuilder.Create<RoutineBackgroundJobScheduler>()
                                                    .WithIdentity(jobKey)
                                                    .UsingJobData("routine", JsonConvert.SerializeObject(routine))
                                                    .Build();
                        string[] routineStartTimeSplitted = routine.StartTime!.Split(':');
                        DateTime routineStartTimeUtc = new DateTime(DateTime.UtcNow.Year,
                                                                    DateTime.UtcNow.Month,
                                                                    DateTime.UtcNow.Day,
                                                                    int.Parse(routineStartTimeSplitted[0]) - 7 < 0
                                                                        ? 24 + int.Parse(routineStartTimeSplitted[0]) - 7
                                                                        : int.Parse(routineStartTimeSplitted[0]) - 7,
                                                                    int.Parse(routineStartTimeSplitted[1]),
                                                                    0);
                        DateTime utcNow = DateTime.UtcNow;
                        TimeSpan timeDifference = routineStartTimeUtc - utcNow;
                        int timeDifferenceInSecond = (int)timeDifference.TotalSeconds;

                        var trigger = TriggerBuilder.Create()
                                                    .WithIdentity(jobKey.Name, jobKey.Group)
                                                    .StartAt(DateBuilder.FutureDate(timeDifferenceInSecond, IntervalUnit.Second))
                                                    .Build();
                        await scheduler.ScheduleJob(job, trigger);
                        _logger.LogInformation($"{nameof(JobSchedulerForNewEntity)} completed for routine with routineId = {routine.Id} ...");
                    }
                }
            }

            if (jobType.Equals(typeof(TodoSchedule).ToString()))
            {
                var toDoJson = context.JobDetail.JobDataMap.GetString("toDo");
                if (toDoJson is not null)
                {
                    var toDo = JsonConvert.DeserializeObject<TodoSchedule>(toDoJson);
                    if (toDo is not null)
                    {
                        JobKey jobKey = JobKey.Create(nameof(TodoScheduleBackgroundJob) + $"toDoId-{toDo.Id}", "group1");
                        var job = JobBuilder.Create<TodoScheduleBackgroundJob>()
                                                    .WithIdentity(jobKey)
                                                    .UsingJobData("toDo", JsonConvert.SerializeObject(toDo))
                                                    .Build();
                        DateTime utcNow = DateTime.UtcNow;
                        TimeSpan timeDifference = toDo.StartAtUtc - utcNow;
                        int timeDifferenceInSecond = (int)timeDifference.TotalSeconds;

                        var trigger = TriggerBuilder.Create()
                                                    .WithIdentity(jobKey.Name, jobKey.Group)
                                                    .StartAt(DateBuilder.FutureDate(timeDifferenceInSecond, IntervalUnit.Second))
                                                    .Build();
                        await scheduler.ScheduleJob(job, trigger);
                        _logger.LogInformation($"{nameof(JobSchedulerForNewEntity)} completed for ToDo with toDoId = {toDo.Id} ...");
                    }
                }
            }
        }
    }
}