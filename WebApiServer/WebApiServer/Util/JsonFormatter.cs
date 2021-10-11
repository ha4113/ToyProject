using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Server.Util
{
    public static class JsonFormatter
    {
        public static readonly Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
        };

        public static async Task<object> ReadAsync(HttpRequest request, Type modelType)
        {
            string json;
            using (var sr = new StreamReader(request.Body))
            {
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            Log.Debug($"Request JSON : {json}");
            var req = Newtonsoft.Json.JsonConvert.DeserializeObject(json, modelType);
            return req;
        }

        public static async Task<long> WriteAsync(HttpContext httpContext, object ack)
        {
            httpContext.Response.ContentType = "application/json";

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ack, JsonSerializerSettings);
            await httpContext.SendResponseBody(json, true, false);
            return json.Length * 2;
        }
    }
}