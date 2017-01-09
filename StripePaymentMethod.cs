using Newtonsoft.Json;

namespace Stripe.Net
{
    public abstract class StripePaymentMethod
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("last4")]
        public string LastFour { get; set; }
    }
}
