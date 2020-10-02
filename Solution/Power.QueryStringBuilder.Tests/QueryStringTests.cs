using FluentAssertions;
using System;
using Xunit;

namespace Power.QueryStringBuilder.Tests
{
    public class QueryStringTests
    {
        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultForNativeClassArray()
        {
            var mock = new[]
            {
                1, 2, 3, 4
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock, "Status");

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("Status=1&Status=2&Status=3&Status=4");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultForNativeClass()
        {
            var mock = new MockQueryString1
            {
                Status = 1
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("Status=1");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultForNativeClass_StatusDefaultValue()
        {
            var mock = new MockQueryString1
            {
                Status = default
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().BeEmpty();
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultClassWithClass()
        {
            var mock = new MockQueryString2
            {
                MockQueryString1 = new MockQueryString1
                {
                    Status = 1
                }
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("MockQueryString1.Status=1");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultClassWithDate()
        {
            var date = new DateTime(2020, 10, 02);

            var mock = new MockQueryStringZero
            {
                Data = date
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("Data=2020-10-02");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultClassWithNullableDate()
        {
            var mock = new MockQueryStringZero
            {
                Data = null
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().BeEmpty();
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultClassWithClass2()
        {
            var mock = new MockQueryString3
            {
                MockQueryString2 = new MockQueryString2
                {
                    MockQueryString1 = new MockQueryString1
                    {
                        Status = 1,
                        Teste = "Teste2"
                    }
                },
                Teste = "Teste1"
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("MockQueryString2.MockQueryString1.Status=1&" +
                "MockQueryString2.MockQueryString1.Teste=Teste2&Teste=Teste1");
        }

        [Fact]
        public void AddSourceQueryString_Null_ReturnsExpectedResultClassWithClass2()
        {
            var mock = new MockQueryString3
            {
                MockQueryString2 = null,
                Teste = "Teste1"
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("Teste=Teste1");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultClassWithArrayClassAndArrayNative()
        {
            var mock = new MockQueryString3
            {
                MockQueryString2 = new MockQueryString2
                {
                    MockQueryString1 = new MockQueryString1
                    {
                        Status = 1,
                        Teste = "Teste2"
                    }
                },
                Teste = "Teste1",
                MockQueryString4 = new[]
                {
                    new MockQueryString4
                    {
                        MockQueryString1 = new MockQueryString1
                        {
                            Status = 1,
                            Teste = "Teste2"
                        }
                    },
                    new MockQueryString4
                    {
                        MockQueryString1 = new MockQueryString1
                        {
                            Status = 2,
                            Teste = "Teste3"
                        }
                    }
                },
                StatusArray = new[]
                {
                    1, 2, 3, 4
                }
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock);

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("" +
                "MockQueryString2.MockQueryString1.Status=1&" +
                "MockQueryString2.MockQueryString1.Teste=Teste2&" +
                "MockQueryString4.MockQueryString1.Status=1&" +
                "MockQueryString4.MockQueryString1.Status=2&" +
                "MockQueryString4.MockQueryString1.Teste=Teste2&" +
                "MockQueryString4.MockQueryString1.Teste=Teste3&" +
                "StatusArray=1&" +
                "StatusArray=2&" +
                "StatusArray=3&" +
                "StatusArray=4&" +
                "Teste=Teste1");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultClassWithArrayClassAndArrayNativeWithBasePath()
        {
            var mock = new MockQueryString3
            {
                MockQueryString2 = new MockQueryString2
                {
                    MockQueryString1 = new MockQueryString1
                    {
                        Status = 1,
                        Teste = "Teste2"
                    }
                },
                Teste = "Teste1",
                MockQueryString4 = new[]
                {
                    new MockQueryString4
                    {
                        MockQueryString1 = new MockQueryString1
                        {
                            Status = 1,
                            Teste = "Teste2"
                        }
                    },
                    new MockQueryString4
                    {
                        MockQueryString1 = new MockQueryString1
                        {
                            Status = 2,
                            Teste = "Teste3"
                        }
                    }
                },
                StatusArray = new[]
                {
                    1, 2, 3, 4
                }
            };

            var queryString = new QueryString();

            queryString.AddSourceQueryString(mock, string.Empty, "MockQueryString3");

            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("" +
                "MockQueryString3.MockQueryString2.MockQueryString1.Status=1&" +
                "MockQueryString3.MockQueryString2.MockQueryString1.Teste=Teste2&" +
                "MockQueryString3.MockQueryString4.MockQueryString1.Status=1&" +
                "MockQueryString3.MockQueryString4.MockQueryString1.Status=2&" +
                "MockQueryString3.MockQueryString4.MockQueryString1.Teste=Teste2&" +
                "MockQueryString3.MockQueryString4.MockQueryString1.Teste=Teste3&" +
                "MockQueryString3.StatusArray=1&" +
                "MockQueryString3.StatusArray=2&" +
                "MockQueryString3.StatusArray=3&" +
                "MockQueryString3.StatusArray=4&" +
                "MockQueryString3.Teste=Teste1");
        }

        [Fact]
        public void AddSourceQueryString_ReturnsExpectedResultForSimpleVar()
        {
            var status = 1;
            var queryString = new QueryString();

            queryString.AddSourceQueryString(status, "Status");
            var result = queryString.QueryStringCollection.ToString();

            result.Should().Be("Status=1");
        }
    }

    internal class MockQueryStringZero
    {
        public DateTime? Data { get; set; }
    }

    internal class MockQueryString1
    {
        public int? Status { get; set; }
        public string Teste { get; set; }
    }

    internal class MockQueryString2
    {
        public MockQueryString1 MockQueryString1 { get; set; }
    }

    internal class MockQueryString3
    {
        public MockQueryString2 MockQueryString2 { get; set; }

        public MockQueryString4[] MockQueryString4 { get; set; }

        public int[] StatusArray { get; set; }

        public string Teste { get; set; }
    }

    internal class MockQueryString4
    {
        public MockQueryString1 MockQueryString1 { get; set; }
    }
}
