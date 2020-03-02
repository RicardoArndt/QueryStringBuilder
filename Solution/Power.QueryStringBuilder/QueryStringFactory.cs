namespace Power.QueryStringBuilder
{
    public class QueryStringFactory
    {
        private readonly QueryString _instance = new QueryString();

        public QueryStringBuilder From<TSource>(TSource source, string basePath = "")
        {
            _instance.AddSourceQueryString(source, string.Empty, basePath);

            return new QueryStringBuilder(_instance);
        }
    }
}
