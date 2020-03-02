using System.Text;

namespace Power.QueryStringBuilder
{
    public class QueryStringBuilder
    {
        private readonly QueryString _instance;

        public QueryStringBuilder(QueryString queryString)
        {
            _instance = queryString;
        }

        public QueryStringBuilder With<TSource>(TSource source, string basePath = "")
        {
            _instance.AddSourceQueryString(source, string.Empty, basePath);

            return this;
        }

        public string Build()
        {
            var stringBuilder = new StringBuilder("?");

            stringBuilder.Append(_instance.QueryStringCollection);

            return stringBuilder.ToString();
        }
    }
}
