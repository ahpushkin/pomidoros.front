using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services.API.Model
{
    public class MultipleResponseHolder<T>
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
    }
}