using Autofac;
using AutoMapper;
using ClothingStoreFranchise.NetCore.Catalog.Dao;
using ClothingStoreFranchise.NetCore.Catalog.Facade;
using ClothingStoreFranchise.NetCore.Catalog.Facade.Impl;
using ClothingStoreFranchise.NetCore.Common.Events;
using ClothingStoreFranchise.NetCore.Common.RabbitMq;
using ClothingStoreFranchise.NetCore.Common.RabbitMq.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace ClothingStoreFranchise.NetCore.Catalog
{
    public static class CatalogServiceCollection
    {
        public static void AddCatalogServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogProductDao, CatalogProductDao>();

            services.AddScoped<ICatalogProductService, CatalogProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICatalogProductService, CatalogProductService>();

            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();
            services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, RabbitMqOptions options)
        {

            var subscriptionClientName = options.Namespace;

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = options.Retries;

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, RabbitMqOptions options)
        {

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                //var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = options.Hostnames.FirstOrDefault(),
                    DispatchConsumersAsync = true
                };

                factory.UserName = options.Username;

                factory.Password = options.Password;

                var retryCount = options.Retries;

                return new RabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<CatalogContext>(options =>
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
    }
}
