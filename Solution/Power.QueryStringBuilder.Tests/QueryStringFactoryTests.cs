using FluentAssertions;
using Power.QueryStringBuilder.Abstraction;
using Xunit;

namespace Power.QueryStringBuilder.Tests
{
    public class QueryStringFactoryTests
    {
        private readonly IQueryStringFactory _queryStringFactory;

        public QueryStringFactoryTests()
        {
            _queryStringFactory = new QueryStringFactory();
        }

        [Fact]
        public void From_ReturnsExpectedResult()
        {
            var status = 1;

            var result = _queryStringFactory.From(status, "Status");

            result.Build().Should().Be("Status=1");

            status = 2;
            
            var result2 = _queryStringFactory.From(status, "StatusTeste");

            result2.Build().Should().Be("StatusTeste=2");
        }
    }
}
