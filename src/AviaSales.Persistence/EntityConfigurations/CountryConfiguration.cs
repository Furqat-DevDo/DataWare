using AviaSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AviaSales.Persistence.EntityConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasMany(c => c.Airports)
            .WithOne(a => a.Country)
            .HasForeignKey(a => a.CountryId);

        builder.Property(c => c.Ccn3).HasMaxLength(3);
        builder.Property(c => c.Cca3).HasMaxLength(3);
        builder.Property(c => c.Cca2).HasMaxLength(2);
        builder.Property(c => c.Cioc).HasMaxLength(3);
    }
}