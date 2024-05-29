using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.Configurations.Read;

public class PetReadConfiguration : IEntityTypeConfiguration<PetReadModel>
{
    public void Configure(EntityTypeBuilder<PetReadModel> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(p => p.Id);

        builder.HasOne<VolunteerReadModel>()
            .WithMany(v => v.Pets)
            .HasForeignKey(p => p.VolunteerId)
            .IsRequired();
    }
}