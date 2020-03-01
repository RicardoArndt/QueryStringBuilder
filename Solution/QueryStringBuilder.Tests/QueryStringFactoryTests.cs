using FluentAssertions;
using Xunit;

namespace QueryStringBuilder.Tests
{
    public class QueryStringFactoryTests
    {
        [Fact]
        public void From_ReturnsExpectedResult()
        {
            var status = 1;
            var queryString = new QueryStringFactory();

            var result = queryString.From(status, "Status");

            result.Build().Should().Be("?Status=1");
        }
    }
}
