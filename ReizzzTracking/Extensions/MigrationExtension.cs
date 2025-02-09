using Microsoft.EntityFrameworkCore;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ReizzzTrackingV1Context context = scope.ServiceProvider.GetRequiredService<ReizzzTrackingV1Context>();
            context.Database.Migrate();
        }
    }
}
