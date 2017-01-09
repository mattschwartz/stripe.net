using Newtonsoft.Json;
using System;

namespace Stripe.Net.Customers
{
    public class CustomerListObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public long CreatedSeconds { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public DateTime Created
        {
            get
            {
                var unixTime = new DateTime(1970, 1, 1);
                return unixTime.AddSeconds(CreatedSeconds);
            }
        }
    }
}
