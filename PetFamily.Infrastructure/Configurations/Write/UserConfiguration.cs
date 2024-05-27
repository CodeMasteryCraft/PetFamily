using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.ComplexProperty(u => u.Email,
            emailBuilder => { emailBuilder.Property(e => e.Value).HasColumnName("email"); });

        builder.ComplexProperty(u => u.Role, roleBuilder =>
        {
            roleBuilder.Property(r => r.Name).HasColumnName("role");
            roleBuilder.Property(r => r.Permissions).HasColumnName("permissions");
        });

        builder.Property(u => u.PasswordHash).IsRequired();
    }
}