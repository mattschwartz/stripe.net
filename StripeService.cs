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

        public async Task CreateCustomerAsync(string sourceToken, string email, string description)
        {
            Customer result = await _client.PostJsonAsync<Customer>(
                "customers",
                new
                {
                    source = sourceToken,
                    email = email,
                    description = description
                });
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
