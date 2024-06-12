using KONE.DataAccess.KONE.Configuration;
using KONE.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KONE.DataAccess.KONE.Concrete
{
    public class KONEContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        DbSet<Product> Products { get; set; }
        DbSet<Province> Provinces { get; set; }
        DbSet<District> Districts { get; set; }
        DbSet<Village> Villages { get; set; }
        DbSet<Coordinates> Coordinates { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<CurrentCard> CurrentCards { get; set; }
        DbSet<CurrentCardAddressMapping> CurrentCardAddressMappings { get; set; }
        DbSet<CurrentCardLandName> CurrentCardLands { get; set; }
        DbSet<Address> Addresses { get; set; }
        public KONEContext(DbContextOptions<KONEContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguraiton());
            builder.ApplyConfiguration(new ProvincesConfiguraiton());
            builder.ApplyConfiguration(new DistrictsConfiguraiton());
            builder.ApplyConfiguration(new VillagesConfiguraiton());
            builder.ApplyConfiguration(new CoordinatesConfiguraiton());
            builder.ApplyConfiguration(new CountryConfiguraiton());
            builder.ApplyConfiguration(new CurrentCardsConfiguraiton());
            builder.ApplyConfiguration(new CurrentCardAddressMappingsConfiguraiton());
            builder.ApplyConfiguration(new CurrentCardLandNameConfiguraiton());
            builder.ApplyConfiguration(new AddressesConfiguraiton());

        }
    }
}
