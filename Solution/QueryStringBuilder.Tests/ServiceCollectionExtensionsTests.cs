using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using QueryStringBuilder.Abstraction;
using QueryStringBuilder.DependencyInjection;
using Xunit;

namespace QueryStringBuilder.Tests
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
