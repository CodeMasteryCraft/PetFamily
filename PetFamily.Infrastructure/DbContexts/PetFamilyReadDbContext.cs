using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Dtos;
using PetFamily.Infrastructure.ReadModels;

namespace PetFamily.Infrastructure.DbContexts;

public class PetFamilyReadDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PetFamilyReadDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<VolunteerReadModel> Volunteers => Set<VolunteerReadModel>();
    public DbSet<PetReadModel> Pets => Set<PetReadModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PetFamily"));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PetFamilyReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}