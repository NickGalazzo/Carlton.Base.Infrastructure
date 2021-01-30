//using System;
//using Microsoft.Extensions.DependencyInjection;
//using Autofac;
//using Autofac.Extensions.DependencyInjection;
//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Configuration;
//using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

//namespace Carlton.Infrastructure.Extensions
//{
//    public static class IServiceProviderExtensions
//    {
//        public static IServiceProvider ConvertToAutofac(this IServiceCollection services)
//        {
//            var builder = new ContainerBuilder();

//            builder.Populate(services);

//            var container = builder.Build();

//            //Create the IServiceProvider based on the container.
//            return new AutofacServiceProvider(container);
//        }

//        public static IServiceCollection AddCarltonLogging(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddLogging(logging =>
//            {
//                logging.AddConfiguration(configuration.GetSection("Logging"));
//                logging.AddSentry();
//                logging.AddFile(o => o.RootPath = AppContext.BaseDirectory);
//            });

//            return services;
//        }

//        public static IServiceCollection AddCarltonHealthChecks(this IServiceCollection services, params IHealthCheck[] dependencyHealthChecks)
//        {
//            var builder = services.AddHealthChecks();

//            foreach (var healthCheck in dependencyHealthChecks)
//            {
//                builder.AddCheck(healthCheck);
//            }

//            return services;
//        }
//    }
//}
