using FluentAssertions;
using Power.QueryStringBuilder.Abstraction;
using Xunit;

namespace Power.QueryStringBuilder.Tests
{
    public class QueryStringBuilderTests
    {
        private readonly IQueryStringFactory _queryStringFactory;

        public QueryStringBuilderTests()
        {
            _queryStringFactory = new QueryStringFactory();
        }

        [Fact]
        public void With_ReturnsExpectedResult()
        {
            var status = 1;
            var tipo = "Global";

            var result = _queryStringFactory.From(status, "Status").With(tipo, "Tipo");

            result.Build().Should().Be("Status=1&Tipo=Global");
        }
    }
}
