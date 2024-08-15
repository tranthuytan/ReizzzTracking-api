using Microsoft.Extensions.DependencyInjection;
using ReizzzTracking.BL.Services.AuthServices;
using ReizzzTracking.BL.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
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
        services.AddScoped<IAuthService, AuthService>();
    }
}

