using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nickname).IsRequired();
        builder.Property(p => p.Nickname).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.BirthDate).IsRequired();
        builder.Property(p => p.Breed).IsRequired();
        builder.Property(p => p.Color).IsRequired();
        builder.Property(p => p.Castration).IsRequired();
        builder.Property(p => p.PeopleAttitude).IsRequired();
        builder.Property(p => p.AnimalAttitude).IsRequired();
        builder.Property(p => p.OnlyOneInFamily).IsRequired();
        builder.Property(p => p.Health).IsRequired();
        builder.Property(p => p.Height).IsRequired(false);
        builder.Property(p => p.OnTreatment).IsRequired();
        builder.Property(p => p.CreatedDate).IsRequired();

        builder.ComplexProperty(p => p.Address, b =>
        {
            b.Property(a => a.City).HasColumnName("city");
            b.Property(a => a.Street).HasColumnName("street").IsRequired(false);
            b.Property(a => a.Building).HasColumnName("building").IsRequired(false);
            b.Property(a => a.Index).HasColumnName("index").IsRequired(false);
        });

        builder.ComplexProperty(p => p.Place,
            b => { b.Property(a => a.Value).HasColumnName("place"); });

        builder.ComplexProperty(p => p.Weight,
            b => { b.Property(a => a.Kilograms).HasColumnName("weight"); });

        builder.ComplexProperty(p => p.ContactPhoneNumber,
            b => { b.Property(a => a.Number).HasColumnName("contact_phone_number"); });

        builder.ComplexProperty(p => p.VolunteerPhoneNumber,
            b => { b.Property(a => a.Number).HasColumnName("volunteer_phone_number"); });

        builder.HasMany(p => p.Photos).WithOne();
        builder.HasMany(p => p.Vaccinations).WithOne();
    }
}