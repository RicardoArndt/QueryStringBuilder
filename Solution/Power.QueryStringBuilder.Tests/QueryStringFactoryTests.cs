using FluentAssertions;
using Xunit;

namespace Power.QueryStringBuilder.Tests
{
    public class QueryStringFactoryTests
    {
        [Fact]
        public void From_ReturnsExpectedResult()
        {
            var status = 1;

            var result = new QueryStringFactory().From(status, "Status");

            result.Build().Should().Be("?Status=1");

            status = 2;
            
            var result2 = new QueryStringFactory().From(status, "StatusTeste");

            result2.Build().Should().Be("?StatusTeste=2");
        }
    }
}
