using Microsoft.Extensions.Options;
using Quartz;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    [DisallowConcurrentExecution]
    public class BackgroundJobSchedulerSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            Console.WriteLine("BackgroundJobScheduler starting");
            var jobKey = JobKey.Create(nameof(DailyJobScheduler));
            var jobKeyWhenAppStart = JobKey.Create($"{nameof(DailyJobScheduler)} when app start");
            options
                .AddJob<DailyJobScheduler>(jobBuilder => jobBuilder.WithIdentity(jobKeyWhenAppStart))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKeyWhenAppStart)
                        .StartNow()
                        );

            options
                .AddJob<DailyJobScheduler>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 0))
                        );

        }
    }
}
