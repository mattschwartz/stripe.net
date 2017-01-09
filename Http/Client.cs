using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Stripe.Net.Http
{
    internal class Client
    {
        private const string _stripeBaseUrl = "https://api.stripe.com/v1/";
        private string _apiKey;
        private StripeErrorResult _lastError;

        internal bool HasError
        {
            get
            {
                return _lastError != null;
            }
        }
        internal StripeErrorResult Error
        {
            get
            {
                return _lastError;
            }
        }

        internal Client(string apiKey)
        {
            _apiKey = apiKey;
        }

        internal async Task<T> PostFormDataAsync<T>(
            string requestUri,
            IEnumerable<KeyValuePair<string, string>> values)
            where T : class
        {
            _lastError = null;

            using (var http = new HttpClient()) {
                http.BaseAddress = new Uri(_stripeBaseUrl);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiKey}");

                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await http.PostAsync(requestUri, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) {
                    var error = JsonConvert.DeserializeObject<StripeErrorResultContainer>(responseJson);
                    _lastError = error?.Error;
                    return null;
                }

                return JsonConvert.DeserializeObject<T>(responseJson);

            }
        }

        internal async Task<T> PostJsonAsync<T>(string requestUri, object data)
            where T : class
        {
            _lastError = null;

            using (var http = new HttpClient()) {
                http.BaseAddress = new Uri(_stripeBaseUrl);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiKey}");

                var content = new JsonContent(data);

                HttpResponseMessage response = await http.PostAsync(requestUri, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) {
                    var error = JsonConvert.DeserializeObject<StripeErrorResultContainer>(responseJson);
                    _lastError = error?.Error;
                    return null;
                }

                return JsonConvert.DeserializeObject<T>(responseJson);
            }
        }

        internal async Task<T> GetJsonAsync<T>(string requestUri)
            where T : class
        {
            _lastError = null;

            using (var http = new HttpClient()) {
                http.BaseAddress = new Uri(_stripeBaseUrl);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiKey}");

                HttpResponseMessage response = await http.GetAsync(requestUri);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode) {
                    var error = JsonConvert.DeserializeObject<StripeErrorResultContainer>(responseJson);
                    _lastError = error?.Error;
                    return null;
                }

                return JsonConvert.DeserializeObject<T>(responseJson);
            }
        }
    }
}
