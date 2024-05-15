using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerReadConfiguration : IEntityTypeConfiguration<VolunteerReadModel>
{
    public void Configure(EntityTypeBuilder<VolunteerReadModel> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id);
        
        builder.OwnsMany(v => v.SocialMedias, navigationBuilder =>
        {
            navigationBuilder.ToJson();
        });

        builder
            .HasMany(v => v.Photos)
            .WithOne()
            .HasForeignKey(ph => ph.VolunteerId)
            .IsRequired();
    }
}