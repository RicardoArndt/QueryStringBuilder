using Microsoft.Extensions.DependencyInjection;
using QueryStringBuilder.Abstraction;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("QueryStringBuilder.Tests")]
namespace QueryStringBuilder.DependencyInjection
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
