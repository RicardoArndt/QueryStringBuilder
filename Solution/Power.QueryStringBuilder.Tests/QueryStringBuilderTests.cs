using FluentAssertions;
using Xunit;

namespace Power.QueryStringBuilder.Tests
{
    public class QueryStringBuilderTests
    {
        [Fact]
        public void With_ReturnsExpectedResult()
        {
            var status = 1;
            var tipo = "Global";

            var result = new QueryStringFactory().From(status, "Status").With(tipo, "Tipo");

            result.Build().Should().Be("?Status=1&Tipo=Global");
        }
    }
}
