using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stripe.Net.Customers
{
    public class CustomerListResult
    {
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("data")]
        public List<CustomerListObject> Data { get; set; } = new List<CustomerListObject>();
    }
}
