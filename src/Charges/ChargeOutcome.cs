using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Net.Charges
{
    public class ChargeOutcome
    {
        [JsonProperty("network_status")]
        public string NetworkStatus { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("risk_level")]
        public string RiskLevel { get; set; }

        [JsonProperty("seller_message")]
        public string SellerMessage { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
