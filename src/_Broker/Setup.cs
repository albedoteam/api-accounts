using Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.MessageProducer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Accounts.Api._Broker
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
                    .Add<ListAccountsRequest>()
                    .Add<GetAccountRequest>()
                    .Add<CreateAccountRequest>()
                    .Add<UpdateAccountRequest>()
                    .Add<DeleteAccountRequest>());

            return services;
        }
    }
}