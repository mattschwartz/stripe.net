using Newtonsoft.Json;

namespace Stripe.Net.BankAccounts
{
    public class BankAccount : StripePaymentMethod
    {
        [JsonProperty("status")]
        public string _status { private get; set; }
        [JsonProperty("account_holder_type")]
        public string _accountHolderType { private get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("account_holder_name")]
        public string AccountHolderName { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        public AccountHolderType AccountHolderType
        {
            get
            {
                switch (_accountHolderType) {
                    case "individual":
                        return AccountHolderType.Individual;
                    case "company":
                    default:
                        return AccountHolderType.Company;
                }
            }
        }
        public BankAccountStatus Status
        {
            get
            {
                switch (_status) {
                    case "new":
                        return BankAccountStatus.New;

                    case "validated":
                        return BankAccountStatus.Validated;

                    case "verified":
                        return BankAccountStatus.Verified;

                    case "verification_failed":
                        return BankAccountStatus.VerificationFailed;

                    case "errored":
                    default:
                        return BankAccountStatus.Errored;
                }
            }
        }
    }
}
