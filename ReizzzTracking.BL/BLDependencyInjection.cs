using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs;
using ReizzzTracking.BL.MessageBroker;
using ReizzzTracking.BL.MessageBroker.Consumer.ToDoConsumers;
using ReizzzTracking.BL.MessageBroker.Consumers.RoutineConsumers;
using ReizzzTracking.BL.MessageBroker.EventBus;
using ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers;
using ReizzzTracking.BL.MessageBroker.Publishers.ToDoPublisher;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.BL.Services.EmailServices;
using ReizzzTracking.BL.Services.PermissionService;
using ReizzzTracking.BL.Services.RoutineCollectionServices;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.Services.TodoScheduleServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Utils.PasswordHasher;

public static class BLDependencyInjection
{
    public static IServiceCollection AddBl(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices(configuration);

        return services;
    }
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // authentication
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        // services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<IRoutineCollectionService, RoutineCollectionService>();
        services.AddScoped<ITodoScheduleService, TodoScheduleService>();
        services.AddScoped<IEmailService, EmailService>();

        // fluent email
        services
            .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
            .AddSmtpSender(configuration["Email:HostLocal"], configuration.GetValue<int>("Email:PortLocal")
                            // , configuration["Email:SenderEmail"], configuration["Email:SenderPass"]
                            );

        // publisher
        services.AddScoped<RoutinePublisher>();
        services.AddScoped<ToDoPublisher>();

        // HttpContextAccessor
        services.AddHttpContextAccessor();

        //background job
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.ConfigureOptions<BackgroundJobSchedulerSetup>();

        //// rabbitMQ
        services.Configure<MessageBrokerSettings>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        // masstransit
        services.AddMassTransit(busConfiguration =>
        {
            busConfiguration.SetKebabCaseEndpointNameFormatter();

            busConfiguration.AddConsumer<BackgroundRoutineCheckedEventConsumer>();
            busConfiguration.AddConsumer<BackgroundToDoCheckedEventConsumer>();

            busConfiguration.UsingRabbitMq((context, rabbitConfigure) =>
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
                rabbitConfigure.Host(settings.Host, "/", hostCfg =>
                {
                    hostCfg.Username(settings.Username);
                    hostCfg.Password(settings.Password);
                });

                rabbitConfigure.ConfigureEndpoints(context);
            });
        });

        // service for masstransit bus
        services.AddTransient<IEventBus, EventBus>();

    }
}

