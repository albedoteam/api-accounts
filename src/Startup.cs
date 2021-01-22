using System.Text.Json.Serialization;
using Accounts.Api.Mappers;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.Documentation;
using AlbedoTeam.Sdk.ExceptionHandler;
using AlbedoTeam.Sdk.FailFast;
using AlbedoTeam.Sdk.MessageProducer;
using AlbedoTeam.Sdk.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;

namespace Accounts.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDocumentation(cfg =>
            {
                cfg.Title = "Accounts Domain API";
                cfg.Description = "codiname: Demiurge";
            });

            services.AddProducer(
                configure => configure
                    .SetBrokerOptions(broker => broker.Host = Configuration.GetValue<string>("Broker:Host")),
                clients => clients
                    .Add<ListAccounts>()
                    .Add<GetAccount>()
                    .Add<CreateAccount>()
                    .Add<UpdateAccount>()
                    .Add<DeleteAccount>());

            services.AddMappers();
            services.AddValidators(GetType().Assembly.FullName);
            services.AddFailFastRequest(typeof(Startup));

            services.AddCors();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseGlobalExceptionHandler(loggerFactory);
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseDocumentation();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}