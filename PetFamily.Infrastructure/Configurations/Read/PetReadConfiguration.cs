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
        
        builder.ComplexProperty(p => p.Address, b =>
        {
            b.Property(f => f.City).HasColumnName("city");
            b.Property(f => f.Street).HasColumnName("street");
            b.Property(f => f.Building).HasColumnName("building");
            b.Property(f => f.Index).HasColumnName("index").IsRequired(false);
        });
    }
}