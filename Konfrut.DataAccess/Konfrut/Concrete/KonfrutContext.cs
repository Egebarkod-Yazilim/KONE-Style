using KONE.DataAccess.Konfrut.Configuration;
using KONE.Entity.Concrete;
using Konfrut.DataAccess.Konfrut.Configuration;
using Konfrut.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Konfrut.DataAccess.Konfrut.Concrete
{
    public class KonfrutContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        DbSet<Product> Products { get; set; }
        DbSet<Province> Provinces { get; set; }
        DbSet<District> Districts { get; set; }
        DbSet<Coordinates> Coordinates { get; set; }
        public KonfrutContext(DbContextOptions<KonfrutContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguraiton());
            builder.ApplyConfiguration(new ProvincesConfiguraiton());
            builder.ApplyConfiguration(new DistrictsConfiguraiton());
            builder.ApplyConfiguration(new CoordinatesConfiguraiton());

        }
    }
}
