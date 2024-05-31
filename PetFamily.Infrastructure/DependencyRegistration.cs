using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.DataAccess;
using PetFamily.Application.Features.Users;
using PetFamily.Application.Features.VolunteerApplications;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.Interseptors;
using PetFamily.Infrastructure.Jobs;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Queries.Pets;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteer;
using PetFamily.Infrastructure.Queries.Volunteers.GetVolunteers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDataStorages(configuration)
            .AddRepositories()
            .AddQueries()
            .AddProviders()
            .AddInterseptors()
            .AddHangfire(configuration)
            .AddJobs()
            .RegisterOptions(configuration);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<IVolunteerApplicationsRepository, VolunteerApplicationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }

    private static IServiceCollection AddInterseptors(this IServiceCollection services)
    {
        services.AddScoped<CacheInvalidationInterceptor>();
        return services;
    }

    private static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IMinioProvider, MinioProvider>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<ICacheProvider, CacheProvider>();
        services.AddScoped<IMailProvider, MailProvider>();
        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<GetPetsQuery>();
        services.AddScoped<GetAllPetsQuery>();
        // services.AddScoped<GetVolunteerQuery>();
        services.AddScoped<GetVolunteerQuery>();
        services.AddScoped<GetVolunteersQuery>();

        return services;
    }

    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddScoped<IImageCleanupJob, ImageCleanupJob>();

        return services;
    }

    private static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(c => c
                .UseNpgsqlConnection(configuration.GetConnectionString("PetFamily"))));

        services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(5));

        return services;
    }

    private static IServiceCollection AddDataStorages(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<PetFamilyWriteDbContext>();
        services.AddScoped<PetFamilyReadDbContext>();
        services.AddSingleton<SqlConnectionFactory>();

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.Minio)
                .Get<MinioOptions>() ?? throw new("Minio configuration not found");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(false);
        });

        return services;
    }

    private static IServiceCollection RegisterOptions(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));
        services.Configure<MailOptions>(configuration.GetSection(MailOptions.Mail));

        return services;
    }
}