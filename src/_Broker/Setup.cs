using Accounts.Requests;
using AlbedoTeam.Sdk.MessageProducer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Api._Broker
{
    internal static class Setup
    {
        public static IServiceCollection AddMessageBroker(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddProducer(
                configure => configure
                    .SetBrokerOptions(broker => broker.Host = configuration.GetValue<string>("Broker:Host")),
                clients => clients
                    .Add<ListAccounts>()
                    .Add<GetAccountRequest>()
                    .Add<CreateAccount>()
                    .Add<UpdateAccount>()
                    .Add<DeleteAccount>());

            return services;
        }
    }
}