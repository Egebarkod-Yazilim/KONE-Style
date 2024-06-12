using KONE.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KONE.DataAccess.KONE.Configuration
{
    public class CurrentCardAddressMappingsConfiguraiton : IEntityTypeConfiguration<CurrentCardAddressMapping>
    {
        public void Configure(EntityTypeBuilder<CurrentCardAddressMapping> builder)
        {
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsActive).HasDefaultValue(true);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();

            builder.HasOne(m => m.CurrentCard)
                .WithMany(c => c.CurrentCardAddressMappings)
                .HasForeignKey(m => m.CurrentCardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Address)
                .WithMany(a => a.CurrentCardAddressMappings)
                .HasForeignKey(m => m.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(m => m.CurrentCardLandName)
            //    .WithOne()
            //    .HasForeignKey<CurrentCardAddressMapping>(m => m.CurrentCardLandNameId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(CurrentCardAddressMapping));
        }
    }
}
