using Newtonsoft.Json;
using RestSharp;

namespace RobotTR.Portal.MVC.Utils
{
    public class HTTPHandler
    {
        public static T Request<T>(string uri, Method method, string address)
        {
            var client = new RestClient(address + uri);
            var request = new RestRequest(method);
            //request.AddHeader("Authorization", "Bearer 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");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}