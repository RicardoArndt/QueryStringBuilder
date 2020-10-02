using Power.QueryStringBuilder.Abstraction;
using System.Text;

namespace Power.QueryStringBuilder
{
    internal class QueryStringBuilder : IQueryStringBuilder
    {
        private readonly QueryString _instance;

        public QueryStringBuilder(QueryString queryString)
        {
            _instance = queryString;
        }

        public IQueryStringBuilder With<TSource>(TSource source, string basePath = "")
        {
            _instance.AddSourceQueryString(source, string.Empty, basePath);

            return this;
        }

        public string Build()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(_instance.QueryStringCollection);

            return stringBuilder.ToString();
        }
    }
}
