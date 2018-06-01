using Newtonsoft.Json.Serialization;

namespace OrderBook.WebApi.Common
{
    public class CamelCasePropertyContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return string.Concat(propertyName.Substring(0, 1).ToLower(), propertyName.Substring(1));
        }
    }
}