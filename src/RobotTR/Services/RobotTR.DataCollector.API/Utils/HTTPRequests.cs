using Newtonsoft.Json;

using RestSharp;

using System.Text;

namespace RobotTR.DataCollector.API.Utils
{
    public static class HTTPRequests
    {
        public static T Request<T>(string uri, Method method)
        {
            var client = new RestClient("https://api.github.com/" + uri);
            var request = new RestRequest(method);
            var auth = Base64Decode("Z2hwX2Naa1FGWHRsTlkxYklQVk90ZFVsaVRXVmpzRWF0ZTNsT0pwOA==");
            request.AddHeader("Authorization", $"Bearer {auth}");
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

        public static string Base64Decode(string value)
{
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}