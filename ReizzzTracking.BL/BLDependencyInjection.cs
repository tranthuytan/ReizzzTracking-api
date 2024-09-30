using Microsoft.Extensions.DependencyInjection;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.BL.Services.PermissionService;
using ReizzzTracking.BL.Services.RoutineCollectionService;
using ReizzzTracking.BL.Services.RoutineServices;
using ReizzzTracking.BL.Services.UserTaskServices;
using ReizzzTracking.BL.Services.Utils.Authentication;
using ReizzzTracking.BL.Utils.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
        services.AddScoped<IUserTaskService, UserTaskService>();
        services.AddHttpContextAccessor();

    }
}

