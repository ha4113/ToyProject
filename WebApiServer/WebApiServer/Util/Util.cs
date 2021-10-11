using System.Threading.Tasks;
using Common.Protocol.Enums;
using Microsoft.AspNetCore.Http;

namespace WebApiServer.Util
{
    public static class Util
    {
        public static async Task SendErrorResponse(this HttpContext httpContext, ResponseResult resultType, string errorMessage = null)
        {
            httpContext.Response.Headers.SetHeaderResultType(resultType);
            await httpContext.SendResponseBody(errorMessage ?? string.Empty, false).ConfigureAwait(false);
        }
    }
}