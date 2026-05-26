using GameStore.Domain.RepositoriesInterfaces.IGameRepositories;
using GameStore.Infrastructure.Data.Context;
using GameStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
        {

            var connectionString =
                Environment.GetEnvironmentVariable("SQLITE_CONNECTION")
                    ?? throw new ArgumentException("Invalid Connection String!!!");

           services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }

}
