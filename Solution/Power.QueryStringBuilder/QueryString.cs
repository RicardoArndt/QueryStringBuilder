using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Power.QueryStringBuilder
{
    public class QueryString
    {
        public NameValueCollection QueryStringCollection { get; set; }
        private string FullPath { get; set; }
        private string BasePath { get; set; }

        public QueryString()
        {
            FullPath = string.Empty;
            QueryStringCollection = HttpUtility.ParseQueryString(string.Empty);
        }

        public void AddSourceQueryString<TSource>(TSource source, string path = "", string basePath = "")
        {
            if (!string.IsNullOrWhiteSpace(basePath))
                BasePath = basePath;

            var typeOfSource = source.GetType();

            var properties = typeOfSource
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => (t.Name != "Chars" && t.Name != "Length")
                && !(typeOfSource.IsArray && t.PropertyType.Namespace.StartsWith("System")))
                .ToArray();

            if (properties.Length == 0)
            {
                if (typeOfSource.IsArray)
                {
                    foreach (var value in (source as Array))
                        AddSourceQueryString(value, path);
                }
                else
                    AddSimpleQueryString(source, path);
            }

            foreach (var prop in properties)
            {
                var propType = prop.PropertyType;

                if (propType.IsArray)
                    AddArrayQueryString(prop, source);

                if (propType.Namespace != null && (propType.Namespace.StartsWith("System") && !propType.IsArray))
                    AddNativeQueryString(prop, source, path);

                if (propType.Namespace != null && (!propType.Namespace.StartsWith("System") && !propType.IsArray))
                {
                    if (string.IsNullOrWhiteSpace(FullPath))
                        FullPath = path;

                    var fullPath = GetFullPath(prop, FullPath);
                    FullPath = fullPath.ToString();

                    if (prop.GetValue(source) != default)
                        AddSourceQueryString(prop.GetValue(source), FullPath);
                }

                FullPath = string.Empty;
            }
        }

        private void AddSimpleQueryString<TSource>(TSource source, string path = "")
        {
            var fullPath = GetFullPathWithoutPropertyInfo(path);

            QueryStringCollection.Add(fullPath.ToString(), source.ToString());
        }

        private void AddArrayQueryString<TSource>(PropertyInfo prop, TSource source)
        {
            if (!(prop.GetValue(source) is Array values)) 
                return;

            foreach (var value in values)
                AddSourceQueryString(value, prop.Name);
        }

        private void AddNativeQueryString<TSource>(PropertyInfo prop, TSource source, string path = "")
        {
            var fullPath = GetFullPath(prop, path);

            var propValue = prop.GetValue(source);

            if (propValue == null) 
                return;

            var propValueResult = prop.GetValue(source);

            if (prop.PropertyType == typeof(DateTime))
                propValueResult = ((DateTime)propValueResult).ToString("yyyy-MM-dd");

            QueryStringCollection.Add(fullPath.ToString(), propValueResult.ToString());
        }

        private StringBuilder GetFullPathWithoutPropertyInfo(string path)
        {
            var fullPath = new StringBuilder(string.Empty);

            if (!string.IsNullOrWhiteSpace(BasePath)
                && FullPath.IndexOf(BasePath, StringComparison.Ordinal) == -1
                && path.IndexOf(BasePath, StringComparison.Ordinal) == -1)
            {
                fullPath.Append(BasePath);

                if (!string.IsNullOrWhiteSpace(path))
                    fullPath.Append(".");
            }

            if (!string.IsNullOrWhiteSpace(path))
                fullPath.Append(path);

            return fullPath;
        }

        private StringBuilder GetFullPath(MemberInfo prop, string path = "")
        {
            var fullPath = GetFullPathWithoutPropertyInfo(path);

            if (!string.IsNullOrEmpty(prop.Name) && !string.IsNullOrEmpty(path))
                fullPath.Append(".");

            if (!string.IsNullOrWhiteSpace(fullPath.ToString())
                && !string.IsNullOrEmpty(prop.Name)
                && fullPath.ToString().IndexOf(".", StringComparison.Ordinal) == -1)
                fullPath.Append(".");

            fullPath.Append(prop.Name);

            return fullPath;
        }
    }
}
