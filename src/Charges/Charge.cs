using Newtonsoft.Json;

namespace Stripe.Net.Charges
{
    public class Charge
    {
        [JsonProperty("failure_code")]
        public string _failureCode { private get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("amount_refunded")]
        public int AmountRefunded { get; set; }

        [JsonProperty("customer")]
        public string CustomerId { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("outcome")]
        public ChargeOutcome Outcome { get; set; }

        [JsonProperty("failure_message")]
        public string FailureMessage { get; set; }

        public CardFailureType? FailureType
        {
            get
            {
                switch (_failureCode) {
                    default:
                        return null;
                }
            }
        }
    }
}
