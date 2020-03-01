namespace Power.QueryStringBuilder.Abstraction
{
    public interface IQueryStringFactory
    {
        IQueryStringBuilder From<TSource>(TSource source, string basePath = "");
    }
}
