using Microsoft.Extensions.DependencyInjection;
using Power.QueryStringBuilder.Abstraction;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Power.QueryStringBuilder.Tests")]
namespace Power.QueryStringBuilder.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryString(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IQueryStringFactory, QueryStringFactory>();

            return serviceCollection;
        }
    }
}
