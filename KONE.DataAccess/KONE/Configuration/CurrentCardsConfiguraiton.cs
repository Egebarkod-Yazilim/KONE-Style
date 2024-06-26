﻿using KONE.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KONE.DataAccess.KONE.Configuration
{
    public class CurrentCardsConfiguraiton : IEntityTypeConfiguration<CurrentCard>
    {
        public void Configure(EntityTypeBuilder<CurrentCard> builder)
        {
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.IsActive).HasDefaultValue(true);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasAnnotation("EmailAddress", "Geçersiz email adresi.");

            builder.ToTable(nameof(CurrentCard));
        }
    }
}
