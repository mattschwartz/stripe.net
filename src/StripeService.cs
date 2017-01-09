﻿using Stripe.Net.BankAccounts;
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
        public bool HasError
        {
            get
            {
                return _client.HasError;
            }
        }
        public StripeErrorResult Error
        {
            get
            {
                return _client.Error;
            }
        }

        public StripeService(string apiKey)
        {
            _client = new Client(apiKey);
        }

        /// <summary>
        /// Creates a new, empty customer object from the provided email and
        /// description. Will not create a new customer if the email address
        /// is taken.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<Customer> CreateCustomerAsync(string email, string description)
        {
            bool emailTaken = await EmailExistsAsync(email);

            if (emailTaken) {
                return null;
            }

            var formData = new List<KeyValuePair<string, string>>();

            formData.Add(new KeyValuePair<string, string>("email", email));
            formData.Add(new KeyValuePair<string, string>("description", description));

            Customer result = await _client.PostFormDataAsync<Customer>("customers", formData);

            if (_client.HasError) {
                // failed
                return null;
            }

            return result;
        }

        /// <summary>
        /// Retrieves the customer id with the provided id, or null if no
        /// customer with that id exists.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var result = await _client.GetJsonAsync<Customer>($"customers/{customerId}");

            if (_client.HasError) {
                // failed
            }

            return result;
        }

        /// <summary>
        /// Adds a new credit/debit card to the specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="expirationMonth">An integer 1-12</param>
        /// <param name="expirationYear">A four digit number in the future</param>
        /// <param name="cvc">A 3-4 digit confirmation number</param>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<Card> AddCardAsync(
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
                return null;
            }
            string token = tokenResult.id;

            formData = new List<KeyValuePair<string, string>>();

            formData.Add(new KeyValuePair<string, string>("source", token));

            var result = await _client.PostFormDataAsync<Card>($"customers/{customerId}/sources", formData);

            if (_client.HasError) {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Retrieves the card for the specified customer and card id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<Card> GetCardAsync(string customerId, string cardId)
        {
            var result = await _client.GetJsonAsync<Card>($"customers/{customerId}/sources/{cardId}");

            // retrieved bankaccount
            if (result.Object != "card") {
                return null;
            }

            if (_client.HasError) {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Updates a <see cref="Card"/> with the supplied values for the 
        /// specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cardId"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="lineOne"></param>
        /// <param name="lineTwo"></param>
        /// <param name="zip"></param>
        /// <param name="expirationMonth"></param>
        /// <param name="expirationYear"></param>
        /// <returns></returns>
        public async Task<Card> UpdateCardAsync(
            string customerId,
            string cardId,
            string city = null,
            string country = null,
            string lineOne = null,
            string lineTwo = null,
            string zip = null,
            int? expirationMonth = null,
            int? expirationYear = null)
        {
            var formData = new List<KeyValuePair<string, string>>();

            if (city != null) {
                formData.Add(new KeyValuePair<string, string>("address_city", city));
            }
            if (country != null) {
                formData.Add(new KeyValuePair<string, string>("address_country", country));
            }
            if (lineOne != null) {
                formData.Add(new KeyValuePair<string, string>("address_line1", lineOne));
            }
            if (lineTwo != null) {
                formData.Add(new KeyValuePair<string, string>("address_line2", lineTwo));
            }
            if (zip != null) {
                formData.Add(new KeyValuePair<string, string>("address_zip", zip));
            }
            if (expirationMonth != null) {
                formData.Add(new KeyValuePair<string, string>("exp_month", expirationMonth.ToString()));
            }
            if (expirationYear != null) {
                formData.Add(new KeyValuePair<string, string>("exp_year", expirationYear.ToString()));
            }

            if (!formData.Any()) {
                return null;
            }

            var result = await _client.PostFormDataAsync<Card>($"customers/{customerId}/sources/{cardId}", formData);

            if (_client.HasError) {
                // failed
                return null;
            }

            return result;
        }

        public async Task DeleteCardAsync(string customerId, string cardId)
        {
            await _client.DeleteAsync($"customers/{customerId}/sources/{cardId}");
        }

        /// <summary>
        /// Adds a <see cref="BankAccount"/> with the specified account details
        /// to the specified user
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="accountHolderName"></param>
        /// <param name="accountHolderType"></param>
        /// <param name="accountNumber"></param>
        /// <param name="routingNumber"></param>
        /// <param name="country"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task AddBankAccountAsync(
            string customerId,
            string accountHolderName,
            AccountHolderType accountHolderType,
            string accountNumber,
            string routingNumber,
            string country = "US",
            string currency = "usd")
        {
            var formData = new List<KeyValuePair<string, string>>();

            string accountHolderTypeValue;
            switch (accountHolderType) {
                case AccountHolderType.Individual:
                    accountHolderTypeValue = "Individual";
                    break;

                case AccountHolderType.Company:
                default:
                    accountHolderTypeValue = "Company";
                    break;
            }

            formData.Add(new KeyValuePair<string, string>("source[object]", "bank_account"));
            formData.Add(new KeyValuePair<string, string>("source[currency]", currency));
            formData.Add(new KeyValuePair<string, string>("source[country]", country));
            formData.Add(new KeyValuePair<string, string>("source[account_holder_name]", accountHolderName));
            formData.Add(new KeyValuePair<string, string>("source[account_holder_type]", accountHolderTypeValue));
            formData.Add(new KeyValuePair<string, string>("source[account_number]", accountNumber));
            formData.Add(new KeyValuePair<string, string>("source[routing_number]", routingNumber));

            var result = await _client.PostFormDataAsync<BankAccount>($"customers/{customerId}/sources", formData);

            if (_client.HasError) {
                // failed
                return;
            }
        }

        /// <summary>
        /// Retrieves a <see cref="BankAccount"/> with the specified
        /// id for the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public async Task<BankAccount> GetBankAccountAsync(string customerId, string bankAccountId)
        {
            var result = await _client.GetJsonAsync<BankAccount>($"customers/{customerId}/sources/{bankAccountId}");

            if (_client.HasError) {
                // failed
            }

            return result;
        }

        /// <summary>
        /// Updates <see cref="BankAccount"/> credentials with the supplied
        /// values for the specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="bankAccountId"></param>
        /// <param name="accountHolderName"></param>
        /// <param name="accountHolderType"></param>
        /// <returns></returns>
        public async Task<BankAccount> UpdateBankAccountAsync(
            string customerId,
            string bankAccountId,
            string accountHolderName = null,
            AccountHolderType? accountHolderType = null)
        {
            var formData = new List<KeyValuePair<string, string>>();

            string accountHolderTypeValue;
            switch (accountHolderType) {
                case AccountHolderType.Individual:
                    accountHolderTypeValue = "Individual";
                    break;

                case AccountHolderType.Company:
                default:
                    accountHolderTypeValue = "Company";
                    break;
            }

            if (accountHolderName != null) {
                formData.Add(new KeyValuePair<string, string>("account_holder_name", accountHolderName));
            }
            if (accountHolderType != null) {
                formData.Add(new KeyValuePair<string, string>("account_holder_type", accountHolderTypeValue));
            }

            if (!formData.Any()) {
                return null;
            }

            var result = await _client.PostFormDataAsync<BankAccount>($"customers/{customerId}/sources/{bankAccountId}", formData);

            if (_client.HasError) {
                // failed
                return null;
            }

            return result;
        }

        /// <summary>
        /// Removes the <see cref="BankAccount"/> from the specified customer
        /// with the specified bank account id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public async Task DeleteBankAccountAsync(string customerId, string bankAccountId)
        {
            await _client.DeleteAsync($"customers/{customerId}/sources/{bankAccountId}");

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