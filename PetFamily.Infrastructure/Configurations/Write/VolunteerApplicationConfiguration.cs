using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class VolunteerApplicationConfiguration : IEntityTypeConfiguration<VolunteerApplication>
{
    public void Configure(EntityTypeBuilder<VolunteerApplication> builder)
    {
        builder.ToTable("volunteer_applications");

        builder.HasKey(v => v.Id);

        builder.ComplexProperty(v => v.FullName, b =>
        {
            b.Property(f => f.FirstName).HasColumnName("first_name");
            b.Property(f => f.LastName).HasColumnName("last_name");
            b.Property(f => f.Patronymic).HasColumnName("patronymic").IsRequired(false);
        });

        builder.ComplexProperty(v => v.Status, b => { b.Property(f => f.Status).HasColumnName("status"); });

        builder.ComplexProperty(v => v.Email, b => { b.Property(v => v.Value).HasColumnName("email"); });

        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

        builder.Property(v => v.YearsExperience)
            .IsRequired();

        builder.Property(v => v.NumberOfPetsFoundHome)
            .IsRequired(false);

        builder.Property(v => v.FromShelter)
            .IsRequired();
    }
}