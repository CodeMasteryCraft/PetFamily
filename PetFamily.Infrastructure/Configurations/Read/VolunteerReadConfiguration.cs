using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerReadConfiguration : IEntityTypeConfiguration<VolunteerReadModel>
{
    public void Configure(EntityTypeBuilder<VolunteerReadModel> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id);
        // builder.ComplexProperty(v => v.FullName, b =>
        // {
        //     b.Property(f => f.FirstName).HasColumnName("first_name");
        //     b.Property(f => f.LastName).HasColumnName("last_name");
        //     b.Property(f => f.Patronymic).HasColumnName("patronymic").IsRequired(false);
        // });
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