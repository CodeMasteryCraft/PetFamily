using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class VaccinationConfiguration : IEntityTypeConfiguration<Vaccination>
{
    public void Configure(EntityTypeBuilder<Vaccination> builder)
    {
        builder.ToTable("vaccinations");
        builder.HasKey(p => p.Id);

        builder.Property(v => v.Name).IsRequired().HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        builder.Property(v => v.Applied).IsRequired(false).HasDefaultValue(DateTimeOffset.UtcNow);
    }
}