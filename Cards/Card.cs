using Newtonsoft.Json;

namespace Stripe.Net.Cards
{
    public class Card : StripePaymentMethod
    {
        [JsonProperty("cvc_check")]
        public string _cvcCheck { private get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("exp_month")]
        public int ExpirationMonth { get; set; }

        [JsonProperty("exp_year")]
        public int ExpirationYear { get; set; }

        [JsonProperty("address_city")]
        public string AddressCity { get; set; }

        [JsonProperty("address_country")]
        public string AddressCountry { get; set; }

        [JsonProperty("address_line1")]
        public string AddressLineOne { get; set; }

        [JsonProperty("address_line2")]
        public string AddressLineTwo { get; set; }

        [JsonProperty("address_zip")]
        public string AddressZip { get; set; }

        public CvcCheckStatus CvcCheck
        {
            get
            {
                switch (_cvcCheck) {
                    case "pass":
                        return CvcCheckStatus.Pass;

                    case "fail":
                        return CvcCheckStatus.Fail;

                    case "unavailable":
                        return CvcCheckStatus.Unavailable;

                    case "unchecked":
                    default:
                        return CvcCheckStatus.Unchecked;
                }
            }
        }
    }
}
