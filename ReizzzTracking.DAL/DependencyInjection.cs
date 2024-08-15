using Microsoft.Extensions.DependencyInjection;

namespace ReizzzTracking.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            return services;
        }
    }
}
