using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);

        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(v => v.YearsExperience)
            .IsRequired();

        builder.Property(v => v.NumberOfPetsFoundHome)
            .IsRequired(false);

        builder.Property(v => v.DonationInfo)
            .IsRequired(false)
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(v => v.FromShelter)
            .IsRequired();

        builder.OwnsMany(v => v.SocialMedias, navigationBuilder =>
        {
            navigationBuilder.ToJson();

            navigationBuilder.Property(s => s.Social)
                .HasConversion(
                    s => s.Value,
                    s => Social.Create(s).Value);
        });

        builder.HasMany(v => v.Photos).WithOne();
        builder.HasMany(v => v.Pets).WithOne();
    }
}