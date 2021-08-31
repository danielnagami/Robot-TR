﻿using Newtonsoft.Json;
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
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}