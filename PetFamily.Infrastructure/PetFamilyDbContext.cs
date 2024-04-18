using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure;

public class PetFamilyDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PetFamilyDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Pet> Pets => Set<Pet>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(PetFamilyDbContext)));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetFamilyDbContext).Assembly);
    }
}