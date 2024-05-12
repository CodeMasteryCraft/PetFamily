using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id);

        builder.Navigation(v => v.Photos).AutoInclude();

        builder
            .HasMany(v => v.Photos)
            .WithOne()
            .HasForeignKey(ph => ph.VolunteerId);
        
    }
}