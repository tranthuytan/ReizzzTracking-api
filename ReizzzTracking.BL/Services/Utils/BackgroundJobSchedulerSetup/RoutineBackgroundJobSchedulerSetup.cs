using Microsoft.Extensions.Options;
using Quartz;

namespace ReizzzTracking.BL.Services.Utils.BackgroundJobSchedulerSetup {
    // maybe this class is redundant
    public class RoutineBackgroundJobSchedulerSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            
            Console.WriteLine($"{nameof(RoutineBackgroundJobSchedulerSetup)} starting");
            // var jobKey = JobKey.Create(nameof(DailyJobScheduler));
            // options
            //     .AddJob<DailyJobScheduler>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            //     .AddTrigger(trigger =>
            //         trigger
            //             .ForJob(jobKey)
            //             .StartNow()
            //             .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 0))
            //             );
        }
    }
}