using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerConfiguration : IEntityTypeConfiguration<VolunterrDto>
{
    public void Configure(EntityTypeBuilder<VolunterrDto> builder)
    {
        builder.ToTable("volunteer");
        builder.HasKey(v => v.Id);

        builder.Navigation(v => v.Photos).AutoInclude();

        builder
            .HasMany(v => v.Photos)
            .WithOne()
            .HasForeignKey(ph => ph.Id);

        builder
            .HasMany(v => v.SocialMedias)
            .WithOne()
            .HasForeignKey(sm => sm.Id);

        builder
            .HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.Id);
    }
}