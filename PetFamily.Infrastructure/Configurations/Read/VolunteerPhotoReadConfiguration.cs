using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerPhotoReadConfiguration : IEntityTypeConfiguration<VolunteerPhotoReadModel>
{
    public void Configure(EntityTypeBuilder<VolunteerPhotoReadModel> builder)
    {
        builder.ToTable("volunteer_photos");
        builder.HasKey(p => p.Id);
    }
}