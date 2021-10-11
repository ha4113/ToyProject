using System;
using System.Threading.Tasks;
using Common.Protocol.Network;
using Microsoft.AspNetCore.Http;
using Server.Enums;
using Server.Util;

namespace Server.NetworkProtocol
{
    public static class RoutingReader
    {
        public static async Task<(TReq Req, HttpFormat format)> ReadReqAsync<TReq>(HttpContext httpContext)
            where TReq : IReq
        {
            if (httpContext == null)
            {
                return default;
            }

            var request = httpContext.Request;

            if (string.Compare(request.Method, "GET", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // GET은 지원하지않는다.
                return default;
            }

            var requestBody = request.Body;
            if (requestBody.CanSeek && requestBody.Length == 0)
            {
                // body가 비어있으면 스킵.
                return default;
            }

            // 요청포맷에 해당하는 포매터 구현을 찾는다.
            var format = request.Headers.GetBodyFormat();

            // Body로부터 모델을 읽는다.
            var obj = await JsonFormatter.ReadAsync(request, typeof(TReq));
            return obj is TReq req ? (req, format) : default;
        }
    
        public static async Task WriteAckAsync(HttpContext httpContext, object ack)
        {
            if (httpContext == null)
            {
                return;
            }

            if (ack == null)
            {
                return;
            }

            // 유저의 변경사항을 패킷에 덧붙인다.
            
        }
    }
}