using musiXmatch.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using musiXmatch.Models;

namespace musiXmatch
{
    public class ServiceConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository), typeof(Repository));
        }
    }
}
