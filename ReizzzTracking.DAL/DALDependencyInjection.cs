using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReizzzTracking.DAL.Common.DbFactory;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.AuthRepository;

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
    }
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReizzzTrackingV1Context>(options => options.UseSqlServer(configuration.GetConnectionString("ReizzzTrackingLocal")));
    }
}