using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Configurations.Read;

public class PetConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(p => p.Id);

        builder.Navigation(p => p.Photos).AutoInclude();

        builder
            .HasMany(p => p.Photos)
            .WithOne()
            .HasForeignKey(ph => ph.PetId);
        
    }
}