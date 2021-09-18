namespace Accounts.Api
{
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Sdk.MessageConsumer.Configuration;
    using AlbedoTeam.Sdk.MessageProducer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupBroker
    {
        public static IServiceCollection ConfigureBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProducer(
                configure =>
                {
                    configure.SetBrokerOptions(broker =>
                    {
                        broker.HostOptions = new HostOptions
                        {
                            Host = configuration.GetValue<string>("Broker_Host"),
                            HeartbeatInterval = 10,
                            RequestedChannelMax = 40,
                            RequestedConnectionTimeout = 60000
                        };

                        broker.KillSwitchOptions = new KillSwitchOptions
                        {
                            ActivationThreshold = 10,
                            TripThreshold = 0.15,
                            RestartTimeout = 60
                        };

                        broker.PrefetchCount = 1;
                    });
                },
                clients => clients
                    .Add<ListAccounts>()
                    .Add<GetAccount>()
                    .Add<CreateAccount>()
                    .Add<UpdateAccount>()
                    .Add<DeleteAccount>());

            return services;
        }
    }
}