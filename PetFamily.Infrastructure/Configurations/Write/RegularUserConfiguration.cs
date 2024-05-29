using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class RegularUserConfiguration : IEntityTypeConfiguration<RegularUser>
{
    public void Configure(EntityTypeBuilder<RegularUser> builder)
    {
        builder.ToTable("regular_users");

        builder.HasKey(r => r.Id);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<RegularUser>(v => v.Id);
    }
}