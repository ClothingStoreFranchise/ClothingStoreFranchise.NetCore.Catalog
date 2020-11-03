using Autofac;
using Autofac.Extensions.DependencyInjection;
using ClothingStoreFranchise.NetCore.Catalog.Mapper;
using ClothingStoreFranchise.NetCore.Common.Constants;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using ClothingStoreFranchise.NetCore.Common.Mapper;
using ClothingStoreFranchise.NetCore.Common.RabbitMq;
using ClothingStoreFranchise.NetCore.Common.RabbitMq.Impl;
using ClothingStoreFranchise.NetCore.Common.Security;
using ClothingStoreFranchise.NetCore.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Steeltoe.Discovery.Client;
using System;

namespace ClothingStoreFranchise.NetCore.Catalog
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        protected IMapperProvider MapperProvider { get; } = new CatalogMapperProvider();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var options = Configuration.GetOptions<RabbitMqOptions>("rabbitMq");

            services.AddEventBus(options);
            services.AddIntegrationServices(options);
            services.AddDiscoveryClient(Configuration);
            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAuthentication("Basic")
                 .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>("Basic", null);

            services.AddAuthorization(config =>
            {
                //UPDATE is new in common
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.Customer, Policies.CustomerAdminPolicy());
            });

            services.AddCatalogServices();

            services.AddCustomDbContext();

            services.AddSingleton(MapperProvider);

            services.AddSwaggerGen(x =>
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDiscoveryClient();

            ConfigureEventBus(app);
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        }
    }

    /*public static class CustomExtensionMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, RabbitMqOptions options)
        {
            
            var subscriptionClientName = options.Namespace;
            
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                //var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = options.Retries;

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(@"data source=localhost\SQLEXPRESS; initial catalog=Catalog; Trusted_Connection=True;MultipleActiveResultSets=true",
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                //options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            });

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(@"data source=localhost\SQLEXPRESS; initial catalog=Catalog; Trusted_Connection=True;MultipleActiveResultSets=true",
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, RabbitMqOptions options)
        {

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = options.Hostnames.FirstOrDefault(),
                    DispatchConsumersAsync = true
                };

                factory.UserName = options.Username;
                
                factory.Password = options.Password;

                var retryCount = options.Retries;

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }
    }*/
}
