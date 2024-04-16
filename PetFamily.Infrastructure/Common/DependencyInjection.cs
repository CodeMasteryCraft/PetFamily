using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Interfaces;
using PetFamily.Infrastructure.Respositories;

namespace PetFamily.Infrastructure.Common
{
    //This class need for DI
    public static class DependencyInjection
    {
        /// <summary>
        /// Registration services from Infastructure layer
        /// </summary>
        /// <param name="services">Services to be registered</param>
        /// <returns></returns>
        public static IServiceCollection AddInfastructure(this IServiceCollection services)
        {

            services.AddScoped<PetFamilyDbContext>();
            services.AddTransient<IPetRepository, PetRepository>();

            return services;
        }
    }
}
