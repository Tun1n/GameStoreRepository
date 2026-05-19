using FluentValidation;
using GameStore.Application.IServicesInterfaces;
using GameStore.Application.Services;
using GameStore.Application.Validators;
using Microsoft.Extensions.DependencyInjection;


namespace GameStore.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IGameService, GameService>();

            services.AddValidatorsFromAssemblyContaining<GameCreateDTOValidator>();

            return services;
        }
    }
}
