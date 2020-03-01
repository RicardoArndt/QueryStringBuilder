namespace QueryStringBuilder.Abstraction
{
    public interface IQueryStringBuilder
    {
        IQueryStringBuilder With<TSource>(TSource source, string path = "");
        string Build();
    }
}
