namespace Accounts.Api
{
    using System;
    using System.Text.Json.Serialization;
    using AccountGrpc;
    using AlbedoTeam.Sdk.Cache;
    using AlbedoTeam.Sdk.Documentation;
    using AlbedoTeam.Sdk.Documentation.Models;
    using AlbedoTeam.Sdk.ExceptionHandler;
    using AlbedoTeam.Sdk.FailFast;
    using AlbedoTeam.Sdk.Validations;
    using Mappers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Converters;

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
            services.AddDocumentation(apiDocument =>
            {
                apiDocument.Title = "Accounts Domain API";
                apiDocument.Description = "codiname: Demiurge";
                apiDocument.Contact = new ApiContact
                {
                    Name = "Albedo Team",
                    Email = "contato@albedo.team",
                    Url = "https://albedo.team"
                };

                apiDocument
                    .AddVersion(1)
                    .AddDefaultVersion();
            });

            services.AddGrpcClient<Accounts.AccountsClient>(options =>
            {
                options.Address = new Uri("https://localhost:5001");
            });

            services.AddCache(configure => configure.SetOptions(options =>
            {
                options.Host = Configuration.GetValue<string>("Cache_Host");
                options.Port = Configuration.GetValue<int>("Cache_Port");
                options.Password = Configuration.GetValue<string>("Cache_Secret");
                options.InstanceName = Configuration.GetValue<string>("Cache_InstanceName");
            }));

            services.AddMappers();
            services.AddValidators(GetType().Assembly.FullName);
            services.AddFailFastRequest(typeof(Startup));

            services.AddCors();

            // services.AddAuth(configure => configure.SetOptions(options =>
            // {
            //     options.AuthServerUrl = Configuration.GetValue<string>("IdentityServer_ApiUrl");
            //     options.AuthServerId = Configuration.GetValue<string>("IdentityServer_AuthServerId");
            //     options.Audience = Configuration.GetValue<string>("IdentityServer_Audience");
            //
            //     var allowedOrigins = Configuration.GetValue<string>("IdentityServer_AllowedOrigins");
            //     if (!string.IsNullOrWhiteSpace(allowedOrigins))
            //         options.AllowedOrigins = allowedOrigins.Split(";").ToList();
            // }));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }).AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseGlobalExceptionHandler(loggerFactory);
            app.UseDocumentation();
            // app.UseAuth();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}