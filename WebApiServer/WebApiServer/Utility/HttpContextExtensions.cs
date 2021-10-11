using System;
using System.Threading.Tasks;
using Common.Protocol.Network;
using Microsoft.AspNetCore.Http;
using Utf8Json;

namespace WebApiServer.Utility
{
    public static class HttpContextExtensions
    {
        public static void SetBodyFormat(this IHeaderDictionary headers,HttpFormat format)
        {
            headers[HttpHeader.FORMAT_KEY.GetStringValue()] = format.ToString();
        }

        public static void SetHeaderResultType(this IHeaderDictionary headers, ResponseResult resultType)
        {
            headers[HttpHeader.RESULT_KEY.GetStringValue()] = resultType.ToString();
        }

        public static HttpFormat GetBodyFormat(this IHeaderDictionary headers)
        {
            var result = HttpFormat.NONE;
            if (headers != null && headers.TryGetValue(HttpHeader.FORMAT_KEY.GetStringValue(), out var values) && values.Count > 0)
            {
                _ = Enum.TryParse(values[0], true, out result);
            }

            return result;
        }
        
        public static ResponseResult GetHeaderResultType(this IHeaderDictionary headers)
        {
            var result = ResponseResult.NONE;
            if (headers != null && headers.TryGetValue(HttpHeader.RESULT_KEY.GetStringValue(), out var values) && values.Count > 0)
            {
                _ = Enum.TryParse(values[0], true, out result);
            }

            return result;
        }

        public static async Task SendResponseBody(this HttpContext httpContext, string body, bool success, bool serializeBody = true)
        {
            var response = httpContext.Response;
            if (response == null)
            {
                return;
            }

            if (response.HasStarted)
            {
                // 이미 헤더가 전송되었다.
                // 이후작업이 의미없거나 클라이언트 오동작을 일으킬수있으니 중단.
                return;
            }

            if (success)
            {
                response.StatusCode = 200;
            }
            else
            {
                response.StatusCode = 500;
            }

            if(serializeBody == false)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(body);
                await response.Body.WriteAsync(bytes).ConfigureAwait(false);
            }
            else
            {
                await JsonSerializer.NonGeneric.SerializeAsync(typeof(string), response.Body, body, JsonSerializer.DefaultResolver).ConfigureAwait(false);
            }
        }
    }
}