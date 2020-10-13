using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static T ParseAsJson<T>(this string str)
            => JsonConvert.DeserializeObject<T>(str, converters: new JsonConverter[] {new IsoDateTimeConverter()});
    }
}