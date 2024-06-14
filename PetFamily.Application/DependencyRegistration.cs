using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Users.Login;
using PetFamily.Application.Features.VolunteerApplications.ApplyVolunteerApplication;
using PetFamily.Application.Features.VolunteerApplications.ApproveVolunteerApplication;
using PetFamily.Application.Features.Volunteers.DeletePhoto;
using PetFamily.Application.Features.Volunteers.PublishPet;
using PetFamily.Application.Features.Volunteers.UploadPhoto;

namespace PetFamily.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<PublishPetHandler>();

        services.AddScoped<UploadVolunteerPhotoHandler>();
        services.AddScoped<DeleteVolunteerPhotoHandler>();

        services.AddScoped<ApplyVolunteerApplicationHandler>();
        services.AddScoped<ApproveVolunteerApplicationHandler>();

        services.AddScoped<LoginHandler>();

        return services;
    }
}