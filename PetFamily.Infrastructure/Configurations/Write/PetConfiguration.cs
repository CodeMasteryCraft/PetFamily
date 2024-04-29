using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nickname).IsRequired().HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        builder.Property(p => p.Description).IsRequired(false).HasMaxLength(Constraints.MAXIMUM_TITLE_LENGTH);
        builder.Property(p => p.BirthDate).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(p => p.Breed).IsRequired().HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        builder.Property(p => p.Color).IsRequired().HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
        builder.Property(p => p.Castration).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.PeopleAttitude).IsRequired(false).HasMaxLength(Constraints.LONG_TITLE_LENGTH);
        builder.Property(p => p.AnimalAttitude).IsRequired(false).HasMaxLength(Constraints.LONG_TITLE_LENGTH);
        builder.Property(p => p.OnlyOneInFamily).IsRequired(false).HasDefaultValue(false);
        builder.Property(p => p.Health).IsRequired(false).HasMaxLength(Constraints.MAXIMUM_TITLE_LENGTH);
        builder.Property(p => p.Height).IsRequired(false);
        builder.Property(p => p.OnTreatment).IsRequired(false).HasDefaultValue(false);
        builder.Property(p => p.CreatedDate).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);

        builder.ComplexProperty(p => p.Address, b =>
        {
            b.Property(a => a.City).HasColumnName("city").HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
            b.Property(a => a.Street).HasColumnName("street").IsRequired(false).HasMaxLength(Constraints.MEDIUM_TITLE_LENGTH);
            b.Property(a => a.Building).HasColumnName("building").IsRequired(false).HasMaxLength(Constraints.SHORT_TITLE_LENGTH);
            b.Property(a => a.Index).HasColumnName("index").IsRequired(false).HasMaxLength(Constraints.INDEX_TITLE_LENGTH);
        });

        builder.ComplexProperty(p => p.Place,
            b => { b.Property(a => a.Value).HasColumnName("place").IsRequired(false).HasMaxLength(Constraints.SHORT_TITLE_LENGTH); });

        builder.ComplexProperty(p => p.Weight,
            b => { b.Property(a => a.Kilograms).IsRequired(false).HasColumnName("weight"); });

        builder.ComplexProperty(p => p.ContactPhoneNumber,
            b => { b.Property(a => a.Number).IsRequired().HasColumnName("contact_phone_number").HasMaxLength(Constraints.SHORT_TITLE_LENGTH); });

        builder.ComplexProperty(p => p.VolunteerPhoneNumber,
            b => { b.Property(a => a.Number).HasColumnName("volunteer_phone_number").IsRequired().HasMaxLength(Constraints.SHORT_TITLE_LENGTH); });

        builder.HasMany(p => p.Photos).WithOne();
        builder.HasMany(p => p.Vaccinations).WithOne();
    }
}