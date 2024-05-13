using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class VolunteerPhotoConfiguration : IEntityTypeConfiguration<VolunteerPhoto>
{
    public void Configure(EntityTypeBuilder<VolunteerPhoto> builder)
    {
        builder.ToTable("volunteer_photos");
        builder.HasKey(p => p.Id);
    }
}