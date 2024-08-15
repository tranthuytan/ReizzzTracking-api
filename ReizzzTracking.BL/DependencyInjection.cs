using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ReizzzTracking.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBl(this IServiceCollection services) 
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            //services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
