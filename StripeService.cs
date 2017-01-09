using Stripe.Net.Customers;
using Stripe.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private async Task<bool> ConfirmEmailUniqueAsync(string email)
        {
            var emails = new List<string>();
            var result = new CustomerListResult();
            string startingAfter;

            result = await _client.GetJsonAsync<CustomerListResult>($"customers?limit=1");
            emails.AddRange(result.Data.Select(t => t.Email));

            while (result.HasMore) {
                startingAfter = result.Data
                    .OrderBy(t => t.CreatedSeconds)
                    .Select(t => t.Id)
                    .First();
                result = await _client.GetJsonAsync<CustomerListResult>($"customers?starting_after={startingAfter}");
                emails.AddRange(result.Data.Select(t => t.Email));
            }

            return emails.Any(t => t.ToLower() == email.ToLower());
        }

        public async Task CreateCustomerAsync(string email, string description)
        {
            bool emailTaken = await ConfirmEmailUniqueAsync(email);

            if (emailTaken) {
                return;
            }

            var formData = new List<KeyValuePair<string, string>>();

            formData.Add(new KeyValuePair<string, string>("email", email));
            formData.Add(new KeyValuePair<string, string>("description", description));

            Customer result = await _client.PostFormDataAsync<Customer>("customers", formData);

            if (result == null) {
                // failed
            }
        }

        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var result = await _client.GetJsonAsync<Customer>($"customers/{customerId}");

            return result;
        }
    }
}
