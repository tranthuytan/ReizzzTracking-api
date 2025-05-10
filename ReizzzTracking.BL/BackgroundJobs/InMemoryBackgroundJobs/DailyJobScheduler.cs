using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    [DisallowConcurrentExecution]
    public class DailyJobScheduler : IJob
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly ITodoScheduleRepository _toDoScheduleRepostiroy;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<DailyJobScheduler> _logger;

        public DailyJobScheduler(IRoutineRepository routineRepository,
                                        ISchedulerFactory schedulerFactory,
                                        ILogger<DailyJobScheduler> logger,
                                        ITodoScheduleRepository todoScheduleRepository
            )
        {
            _routineRepository = routineRepository;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
            _toDoScheduleRepostiroy = todoScheduleRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start of {nameof(DailyJobScheduler)}");
            await GetAllRoutineAndSetupBackgroundJob(context);
            await GetAllToDoAndSetupBackgroundJob(context);
            _logger.LogInformation($"Complete {nameof(DailyJobScheduler)}");
        }
        private async Task GetAllRoutineAndSetupBackgroundJob(IJobExecutionContext context)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var nowTimeString = DateTime.UtcNow.AddHours(7).ToString("HH:mm");
            // get all routine (with utc +7) that have  start time > current time
            var routines = await _routineRepository.GetAll(x => string.Compare(x.StartTime, nowTimeString) == 1);
            foreach (var routine in routines)
            {
                if (routine is not null)
                {
                    JobKey jobKey = JobKey.Create(nameof(RoutineBackgroundJobScheduler) + $"routineId-{routine.Id}", "group1");
                    IJobDetail routineJob = JobBuilder.Create<RoutineBackgroundJobScheduler>()
                                                .WithIdentity(jobKey)
                                                .UsingJobData("routine", JsonConvert.SerializeObject(routine))
                                                .Build();
                    string[] routineStartTimeSplitted = routine!.StartTime!.Split(':');
                    DateTime routineStartTimeUtc7 = new DateTime(
                                                            DateTime.Now.Year,
                                                            DateTime.Now.Month,
                                                            DateTime.Now.Day,
                                                            int.Parse(routineStartTimeSplitted[0]),
                                                            int.Parse(routineStartTimeSplitted[1]),
                                                            0);
                    DateTime routineStartTimeUtc = routineStartTimeUtc7.AddHours(-7);
                    DateTime utcNow = DateTime.UtcNow;
                    TimeSpan timeDifference = routineStartTimeUtc - utcNow;
                    int timeDifferenceInSecond = (int)timeDifference.TotalSeconds;

                    ITrigger trigger = TriggerBuilder.Create()
                                            .WithIdentity(jobKey.Name, jobKey.Group)
                                            .StartAt(DateBuilder.FutureDate(timeDifferenceInSecond, IntervalUnit.Second))
                                            .Build();
                    await scheduler.ScheduleJob(routineJob, trigger);
                    _logger.LogInformation($"Completed {nameof(DailyJobScheduler)} for routine with id = {routine.Id} at Utc = {routineStartTimeUtc}");
                }
            }
        }
        private async Task GetAllToDoAndSetupBackgroundJob(IJobExecutionContext context)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            // get all routine (with utc) that have  start time > current time
            var toDos = await _toDoScheduleRepostiroy.GetAll(x => DateTime.Compare(x.StartAtUtc, DateTime.UtcNow) == 1);
            foreach (var toDo in toDos)
            {
                if (toDo is not null)
                {
                    JobKey jobKey = JobKey.Create(nameof(TodoScheduleBackgroundJob) + $"routineId-{toDo.Id}", "group1");
                    IJobDetail toDoJob = JobBuilder.Create<TodoScheduleBackgroundJob>()
                                                .WithIdentity(jobKey)
                                                .UsingJobData("toDo", JsonConvert.SerializeObject(toDo))
                                                .Build();
                    TimeSpan timeDifference = toDo.StartAtUtc - DateTime.UtcNow;
                    var a = timeDifference.TotalMinutes;
                    int timeDifferenceInSecond = (int)timeDifference.TotalSeconds;

                    ITrigger trigger = TriggerBuilder.Create()
                                            .WithIdentity(jobKey.Name, jobKey.Group)
                                            .StartAt(DateBuilder.FutureDate(timeDifferenceInSecond, IntervalUnit.Second))
                                            .Build();
                    await scheduler.ScheduleJob(toDoJob, trigger);
                    _logger.LogInformation($"Completed {nameof(DailyJobScheduler)} for ToDoSchedule with id = {toDo.Id} at Utc = {toDo.StartAtUtc}");
                }
            }
        }
    }
}
