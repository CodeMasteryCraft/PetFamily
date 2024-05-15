using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nickname)
            .IsRequired()
            .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.Breed)
            .IsRequired()
            .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);

        builder.Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);

        builder.Property(p => p.Castration)
            .IsRequired();

        builder.Property(p => p.PeopleAttitude)
            .IsRequired()
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(p => p.AnimalAttitude)
            .IsRequired(false)
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(p => p.OnlyOneInFamily)
            .IsRequired();

        builder.Property(p => p.Health)
            .IsRequired()
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(p => p.Height).IsRequired(false);

        builder.Property(p => p.OnTreatment)
            .IsRequired();

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.ComplexProperty(p => p.Address, b =>
        {
            b.Property(a => a.City)
                .HasColumnName("city")
                .IsRequired()
                .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);

            b.Property(a => a.Street)
                .HasColumnName("street")
                .IsRequired()
                .HasMaxLength(Constraints.MEDIUM_TITLE_LENGTH);

            b.Property(a => a.Building)
                .HasColumnName("building")
                .IsRequired()
                .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);

            b.Property(a => a.Index)
                .HasColumnName("index")
                .IsRequired()
                .HasMaxLength(Address.INDEX_TITLE_LENGTH);
        });

        builder.ComplexProperty(p => p.Place, b =>
        {
            b.Property(a => a.Value)
                .HasColumnName("place")
                .IsRequired()
                .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        });

        builder.ComplexProperty(p => p.Weight, b =>
        {
            b.Property(a => a.Kilograms)
                .HasColumnName("weight")
                .IsRequired();
        });

        builder.ComplexProperty(p => p.ContactPhoneNumber, b =>
        {
            b.Property(a => a.Number)
                .HasColumnName("contact_phone_number")
                .IsRequired()
                .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        });

        builder.ComplexProperty(p => p.VolunteerPhoneNumber, b =>
        {
            b.Property(a => a.Number)
                .HasColumnName("volunteer_phone_number")
                .IsRequired()
                .HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        });

        builder.HasMany(p => p.Photos).WithOne().IsRequired();
        builder.HasMany(p => p.Vaccinations).WithOne();
    }
}