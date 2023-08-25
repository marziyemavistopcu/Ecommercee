using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace Ecommerce.Web.Code.Rest
{
    public class UserRestClient
    {

        private string BASE_API_URI = "https://localhost:7004/api";

        public dynamic Login(string Username, string Password)
        {
            RestClient client = new RestClient(BASE_API_URI, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null}));

            RestRequest req = new RestRequest("/Auth/Login", Method.Post);
            req.AddJsonBody(new
            {
                username= Username,
                password= Password
            });

            RestResponse res = client.Post(req);
            string msg = res.Content.ToString();
            dynamic result = JObject.Parse(msg); 
            return result;
        }
    }
}
