using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PetFamily.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<PetsService>();
        services.AddValidatorsFromAssembly(typeof(DependencyRegistration).Assembly);

        return services;
    }
}