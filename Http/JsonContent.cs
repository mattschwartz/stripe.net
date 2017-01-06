using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Stripe.Net.Http
{
    public class JsonContent : StringContent
    {
        public JsonContent(object content)
            : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
        {
        }
    }
}
