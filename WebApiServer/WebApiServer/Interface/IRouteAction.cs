using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApiServer.Interface
{
    public interface IRouteAction
    {
        Type GetReqType();
        Type GetAckType();
        Task Run(HttpContext httpContext);
    }
}