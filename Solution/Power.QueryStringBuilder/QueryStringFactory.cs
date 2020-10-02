using Power.QueryStringBuilder.Abstraction;

namespace Power.QueryStringBuilder
{
    internal class QueryStringFactory : IQueryStringFactory
    {
        public IQueryStringBuilder From<TSource>(TSource source, string basePath = "")
        {
            var queryString = new QueryString();

            queryString.AddSourceQueryString(source, string.Empty, basePath);

            return new QueryStringBuilder(queryString);
        }
    }
}
