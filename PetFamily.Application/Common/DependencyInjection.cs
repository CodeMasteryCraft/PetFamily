using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Interfaces;
using PetFamily.Application.Pets.Services;

namespace PetFamily.Application.Common
{
    //This class need for DI
    public static class DependencyInjection
    {
        /// <summary>
        /// Registration services from Application layer
        /// </summary>
        /// <param name="services">Services to be registered</param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPetService, PetService>();

            return services;
        }
    }
}
