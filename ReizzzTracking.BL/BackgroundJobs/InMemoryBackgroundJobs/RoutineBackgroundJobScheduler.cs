using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.DAL.Common.DateTimeToUtc;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class RoutineBackgroundJobScheduler : IJob
    {
        //private readonly ILogger _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("RoutineBackgroundJobScheduler starting");
            //_logger.LogInformation($"Start of {nameof(RoutineBackgroundJobScheduler)}");
            var dataMap = context.JobDetail.JobDataMap;
            var routineJson = dataMap.GetString("routine");
            var routine = JsonConvert.DeserializeObject<Routine>(routineJson);
            var routineStartAt = TimeMapper.FromTimeStringUtc7ToUtc(routine.StartTime);
            var timer = TimeMapper.GetDateTimeOffSetFromTimeOnly(routineStartAt);
            if (timer < DateTimeOffset.UtcNow)
            {
                var scheduler = await _schedulerFactory.GetScheduler();

                IJobDetail routineJob = JobBuilder.Create<RoutineIsUsedCheckBackgroundJob>()
                                            .WithIdentity(nameof(RoutineIsUsedCheckBackgroundJob))
                                            .UsingJobData("routine", JsonConvert.SerializeObject(routine))
                                            .Build();
                ITrigger trigger = TriggerBuilder.Create()
                                        .WithIdentity(nameof(RoutineIsUsedCheckBackgroundJob))
                                        .StartAt(timer)
                                        .Build();
                await scheduler.ScheduleJob(routineJob, trigger);

                //_logger.LogInformation($"Completed {nameof(RoutineBackgroundJobScheduler)}");
            }
            Console.WriteLine("RoutineBackgroundJobScheduler completed");
        }
    }
}
