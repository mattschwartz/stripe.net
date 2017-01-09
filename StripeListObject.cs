using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stripe.Net
{
    public class StripeListObject
    {
        [JsonProperty("data")]
        public List<dynamic> Data { get; set; } = new List<dynamic>();
    }

    public class StripeListObject<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; } = new List<T>();
    }
}
