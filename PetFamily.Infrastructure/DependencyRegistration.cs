using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Pets;
using PetFamily.Application.Features.SocialMedias;
using PetFamily.Application.Features.Vaccinations;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Queries.Pets;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddDatabase()
            .AddRepositories()
            .AddQueries();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
        services.AddScoped<IVaccinationRepository, VaccinationRepository>();
        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<GetPetsQuery>();
        services.AddScoped<GetAllPetsQuery>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<PetFamilyWriteDbContext>();
        services.AddScoped<PetFamilyReadDbContext>();

        services.AddSingleton<SqlConnectionFactory>();
        return services;
    }
}