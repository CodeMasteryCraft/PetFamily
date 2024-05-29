using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Application.DataAccess;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.Interseptors;

namespace PetFamily.Infrastructure.DbContexts;

public class PetFamilyWriteDbContext : DbContext, IUnitOfWork
{
    private readonly IConfiguration _configuration;
    private readonly CacheInvalidationInterceptor _cacheInvalidationInterceptor;

    public PetFamilyWriteDbContext(
        IConfiguration configuration, CacheInvalidationInterceptor cacheInvalidationInterceptor)
    {
        _configuration = configuration;
        _cacheInvalidationInterceptor = cacheInvalidationInterceptor;
    }

    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<VolunteerApplication> VolunteersApplications => Set<VolunteerApplication>();
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PetFamily"));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.AddInterceptors(_cacheInvalidationInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PetFamilyWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}