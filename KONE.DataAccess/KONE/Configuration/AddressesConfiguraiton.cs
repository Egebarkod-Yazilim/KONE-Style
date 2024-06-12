using KONE.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KONE.DataAccess.KONE.Configuration
{
    public class AddressesConfiguraiton : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsActive).HasDefaultValue(true);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();

            // Foreign key configurations
            builder.HasOne(a => a.Country)
                   .WithMany()
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Province)
                   .WithMany()
                   .HasForeignKey(a => a.ProvinceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.District)
                   .WithMany()
                   .HasForeignKey(a => a.DistrictId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Village)
                   .WithMany()
                   .HasForeignKey(a => a.VillageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(Address));
        }
    }
}
