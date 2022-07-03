using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using SneakerPlugKC.MVC.UI.Repositories;
using SneakerPlugKC.MVC.UI.Services;

namespace SneakerPlugKC.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            // Set MVC Resolver
            DependencyResolver.SetResolver(resolver);

            // MVC Route
            RouteTable.Routes.MapRoute(
                name: "",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));
            services.AddTransient<IShoesRepository, ShoesRepository>();
            services.AddTransient<IShoesService, ShoesService>();
        }
    }

    public class DefaultDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        protected IServiceProvider serviceProvider;

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }

    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddControllersAsServices(this IServiceCollection services,
            IEnumerable<Type> controllerTypes)
        {
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}
