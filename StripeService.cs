using Newtonsoft.Json;
using Stripe.Net.Cards;
using Stripe.Net.Customers;
using Stripe.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Net
{
    public class StripeService
    {
        private readonly Client _client;

        public StripeService(string apiKey)
        {
            _client = new Client(apiKey);
        }

        public async Task CreateCustomerAsync(string email, string description)
        {
            bool emailTaken = await EmailExistsAsync(email);

            if (emailTaken) {
                return;
            }

            var formData = new List<KeyValuePair<string, string>>();

            formData.Add(new KeyValuePair<string, string>("email", email));
            formData.Add(new KeyValuePair<string, string>("description", description));

            Customer result = await _client.PostFormDataAsync<Customer>("customers", formData);

            if (_client.HasError) {
                // failed
            }
        }

        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var result = await _client.GetJsonAsync<Customer>($"customers/{customerId}");

            if (_client.HasError) {
                // failed
            }

            return result;
        }

        public async Task AddCardAsync(
            string customerId,
            int expirationMonth,
            int expirationYear,
            int cvc,
            string number)
        {
            var formData = new List<KeyValuePair<string, string>>();

            formData.Add(new KeyValuePair<string, string>("card[exp_month]", expirationMonth.ToString()));
            formData.Add(new KeyValuePair<string, string>("card[exp_year]", expirationYear.ToString()));
            formData.Add(new KeyValuePair<string, string>("card[cvc]", cvc.ToString()));
            formData.Add(new KeyValuePair<string, string>("card[number]", number));

            var tokenResult = await _client.PostFormDataAsync<dynamic>("tokens", formData);

            if (_client.HasError) {
                // failed
                return;
            }
            string token = tokenResult.id;

            formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("source", token));

            var result = await _client.PostFormDataAsync<object>($"customers/{customerId}/sources", formData);

            if (_client.HasError) {
                // failed
            }
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            string normalizedEmail = email.ToLower();
            var emails = new List<string>();
            var result = new CustomerListResult();
            string startingAfter;

            result = await _client.GetJsonAsync<CustomerListResult>($"customers");
            emails.AddRange(result.Data.Select(t => t.Email.ToLower()));

            if (emails.Any(t => t == normalizedEmail)) {
                return true;
            }

            while (result.HasMore) {
                startingAfter = result.Data
                    .OrderBy(t => t.CreatedSeconds)
                    .Select(t => t.Id)
                    .First();

                result = await _client.GetJsonAsync<CustomerListResult>($"customers?starting_after={startingAfter}");
                emails.AddRange(result.Data.Select(t => t.Email.ToLower()));

                if (emails.Any(t => t == normalizedEmail)) {
                    return true;
                }
            }

            return emails.Any(t => t == normalizedEmail);
        }
    }
}
