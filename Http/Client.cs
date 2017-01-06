using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Stripe.Net.Http
{
    internal class Client
    {
        private const string _stripeBaseUrl = "https://api.stripe.com/v1/";
        private string _apiKey;

        internal Client(string apiKey)
        {
            _apiKey = apiKey;
        }

        internal async Task<T> PostJsonAsync<T>(string requestUri, object data)
            where T : class
        {
            using (var http = new HttpClient()) {
                http.BaseAddress = new Uri(_stripeBaseUrl);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiKey}");

                var content = new JsonContent(data);
                HttpResponseMessage response = await http.PostAsync(requestUri, content);

                if (!response.IsSuccessStatusCode) {
                    return null;
                }

                string responseJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseJson);
            }
        }

        internal async Task<T> GetJsonAsync<T>(string requestUri)
            where T : class
        {
            using (var http = new HttpClient()) {
                http.BaseAddress = new Uri(_stripeBaseUrl);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiKey}");

                HttpResponseMessage response = await http.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode) {
                    return null;
                }

                string responseJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseJson);
            }
        }
    }
}
