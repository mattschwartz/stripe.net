using Newtonsoft.Json;

namespace Stripe.Net.Charges
{
    public class Charge
    {
        [JsonProperty("failure_code")]
        public string _failureCode { private get; set; }
        [JsonProperty("status")]
        public string _status { get; set; }

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

        [JsonProperty("outcome")]
        public ChargeOutcome Outcome { get; set; }

        [JsonProperty("failure_message")]
        public string FailureMessage { get; set; }

        public ChargeStatus Status
        {
            get
            {
                switch (_status) {
                    case "succeeded":
                        return ChargeStatus.Succeeded;

                    case "pending":
                        return ChargeStatus.Pending;

                    case "failed":
                    default:
                        return ChargeStatus.Failed;
                }
            }
        }
    }
}
