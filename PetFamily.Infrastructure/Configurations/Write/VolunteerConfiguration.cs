using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Name).IsRequired();
        
        builder.HasMany(v => v.Photos).WithOne();
        builder.HasMany(v => v.SocialMedias).WithOne();
        builder.HasMany(v => v.Pets).WithOne();
    }
}