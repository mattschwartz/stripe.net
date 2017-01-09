using Newtonsoft.Json;

namespace Stripe.Net
{
    public class StripeErrorResult
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("param")]
        public string Parameter { get; set; }
    }

    public class StripeErrorResultContainer
    {
        [JsonProperty("error")]
        public StripeErrorResult Error { get; set; }
    }
}
