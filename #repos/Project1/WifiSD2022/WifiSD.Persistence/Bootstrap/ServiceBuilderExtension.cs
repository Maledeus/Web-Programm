using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WifiSD.Common.Attributes;

namespace WifiSD.Persistence.Bootstrap
{
    public static class ServiceBuilderExtension
    {
        public static void RegisterRepositories(this IServiceCollection service)
        {
            service.Scan(scan => scan
                    .FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(c => c.WithAttribute<MappServiceDependencyAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        }
    }
}
