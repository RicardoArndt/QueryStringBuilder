using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Power.QueryStringBuilder.Abstraction;
using Power.QueryStringBuilder.DependencyInjection;
using Xunit;

namespace Power.QueryStringBuilder.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddQueryString_ReturnsExpectedInstances()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddQueryString();

            var provider = serviceCollection.BuildServiceProvider();

            var queryFactoryInstance = provider.GetService<IQueryStringFactory>();

            queryFactoryInstance.Should().NotBeNull();
        }
    }
}
