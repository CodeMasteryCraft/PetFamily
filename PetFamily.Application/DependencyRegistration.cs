using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Volunteers.CreatePet;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.DeletePhoto;
using PetFamily.Application.Features.Volunteers.GetPhotos;
using PetFamily.Application.Features.Volunteers.GetsPhotos;
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
        services.AddScoped<CreatePetHandler>();
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UploadVolunteerPhotoHandler>();
        services.AddScoped<DeleteVolunteerPhotoHandler>();
        services.AddScoped<GetVolunteerPhotoHandler>();
        services.AddScoped<GetVolunteerAllPhotosHandler>();
        return services;
    }
}