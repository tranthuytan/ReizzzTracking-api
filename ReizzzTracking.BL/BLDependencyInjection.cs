using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs;
using ReizzzTracking.BL.BackgroundJobs.LoggingBackground;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.BL.Services.PermissionService;
using ReizzzTracking.BL.Services.RoutineCollectionServices;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.Services.TodoScheduleServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Utils.PasswordHasher;

public static class BLDependencyInjection
{
    public static IServiceCollection AddBl(this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider,JwtProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<IRoutineCollectionService, RoutineCollectionService>();
        services.AddScoped<ITodoScheduleService, TodoScheduleService>();
        services.AddHttpContextAccessor();

        //background job
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.ConfigureOptions<BackgroundJobSchedulerSetup>();
    }
}

