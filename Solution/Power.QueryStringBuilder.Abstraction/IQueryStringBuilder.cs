namespace Power.QueryStringBuilder.Abstraction
{
    public interface IQueryStringBuilder
    {
        IQueryStringBuilder With<TSource>(TSource source, string basePath = "");
        string Build();
    }
}
