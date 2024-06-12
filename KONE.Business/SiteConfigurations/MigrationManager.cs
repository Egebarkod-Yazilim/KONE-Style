using KONE.DataAccess.KONE.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KONE.Business.SiteConfigurations
{
    public static class MigrationManager
    {
        public static IHost ApplyMigrations(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<KONEContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
