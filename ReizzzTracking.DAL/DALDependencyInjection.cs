using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReizzzTracking.DAL.Common.DbFactory;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.AuthRepository;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using ReizzzTracking.DAL.Repositories.UserRepository;

public static class DALDependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        return services;
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDbFactory, DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoutineCollectionRepository, RoutineCollectionRepository>();
        services.AddScoped<IRoutineRepository, RoutineRepository>();
    }
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReizzzTrackingV1Context>(options => options.UseSqlServer(configuration.GetConnectionString("ReizzzTrackingLocal")));
    }
}