using AlbedoTeam.Accounts.Api.Mappers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Accounts.Api.Mappers
{
    public static class Setup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IAccountMapper, AccountMapper>();

            return services;
        }
    }
}