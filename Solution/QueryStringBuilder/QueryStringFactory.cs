using QueryStringBuilder.Abstraction;

namespace QueryStringBuilder
{
    internal class QueryStringFactory : IQueryStringFactory
    {
        private readonly QueryString _instance;

        public QueryStringFactory()
        {
            _instance = new QueryString();
        }

        public IQueryStringBuilder From<TSource>(TSource source, string basePath = "")
        {
            _instance.AddSourceQueryString(source, string.Empty, basePath);

            return new QueryStringBuilder(_instance);
        }
    }
}
