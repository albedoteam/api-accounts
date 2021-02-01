using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Api.Mappers
{
    public static class Setup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IAccountMapper, AccountMapper>();

            services.AddTransient<Mappers.V2.IAccountMapper, Mappers.V2.AccountMapper>();
            
            return services;
        }
    }
}