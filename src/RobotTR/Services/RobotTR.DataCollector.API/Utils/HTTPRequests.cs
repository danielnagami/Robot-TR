using Newtonsoft.Json;
using RestSharp;

namespace RobotTR.DataCollector.API.Utils
{
    public static class HTTPRequests
    {
        public static T Request<T>(string uri, Method method)
        {
            var client = new RestClient("https://api.github.com/" + uri);
            var request = new RestRequest(method);
            request.AddHeader("Authorization", "Bearer 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");
            IRestResponse response = client.Execute(request);
            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch
            {
                return default(T);
            }
        }
    }
}