using RiotNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedditFlairs.Core.Clients
{
    public class TftRiotClient : RiotNet.RiotClient
    {
        public TftRiotClient(string apiKey) : base(new RiotClientSettings
        {
            ApiKey = apiKey
        })
        {
        }

        protected override Task<T> ExecuteAsync<T>(HttpMethod method, string resource, string resourceName, object body, string platformId,
            CancellationToken token, IDictionary<string, object> queryParameters = null)
        {
            if (platformId == null)
                platformId = PlatformId;
            if (platformId == null)
                throw new RestException(null, "Platform ID was not specified. You must set RiotClient.PlatformId or pass in a platformId parameter.");

            var game = resource.Contains("party") ? "lol" : "tft";
            if (!resourceName.Contains("party"))
            {
                resource = resource.Replace("v4", "v1");
                resourceName = resourceName.Replace("v4", "v1");
            }

            var resourceBuilder = new StringBuilder();
            resourceBuilder
                .Append("https://")
                .Append(platformId.ToLowerInvariant())
                .Append($".api.riotgames.com/{game}/")
                .Append(resource);
            if (queryParameters != null)
            {
                var querySeparator = resource.Contains("?") ? "&" : "?";
                foreach (var kvp in queryParameters)
                {
                    if (!(kvp.Value is string) && kvp.Value is IEnumerable enumerable)
                    {
                        foreach (var value in enumerable)
                            AppendQueryValue(resourceBuilder, kvp.Key, value, ref querySeparator);
                    }
                    else
                    {
                        AppendQueryValue(resourceBuilder, kvp.Key, kvp.Value, ref querySeparator);
                    }
                }
            }
            HttpRequestMessage buildRequest()
            {
                var request = new HttpRequestMessage(method, resourceBuilder.ToString());
                request.Headers.Add("X-Riot-Token", Settings.ApiKey);
                if (body != null)
                    request.Content = new JsonContent(body);
                return request;
            }
            var methodName = method + " " + resourceName;
            var queryIndex = methodName.IndexOf('?');
            if (queryIndex > 0)
                methodName = methodName.Remove(queryIndex);
            return ExecuteAsync<T>(buildRequest, methodName, platformId, token);
        }

        private static void AppendQueryValue(StringBuilder builder, string key, object value, ref string querySeparator)
        {
            if (value is Enum)
                value = (int)value;

            builder
                .Append(querySeparator)
                .Append(key)
                .Append("=")
                .Append(Uri.EscapeDataString(Convert.ToString(value, CultureInfo.InvariantCulture)));
            querySeparator = "&";
        }
    }
}