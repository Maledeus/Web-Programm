using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WifiSD.Common.Attributes;

namespace WifiSD.Application.Bootstrap
{
    public static class ServiceBuilderExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection service)
        {
            service.Scan(scan => scan
                    .FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(c => c.WithAttribute<MappServiceDependencyAttribute>())
                    .AsSelf()
                    .WithScopedLifetime());
        }
    }
}
