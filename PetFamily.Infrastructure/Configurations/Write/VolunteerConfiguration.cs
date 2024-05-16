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

        builder.ComplexProperty(v => v.FullName, b =>
        {
            b.Property(f => f.FirstName).HasColumnName("first_name");
            b.Property(f => f.LastName).HasColumnName("last_name");
            b.Property(f => f.Patronymic).HasColumnName("patronymic").IsRequired(false);
        });

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

        builder.HasMany(v => v.Photos).WithOne().IsRequired();
        builder.HasMany(v => v.Pets).WithOne().IsRequired();
    }
}