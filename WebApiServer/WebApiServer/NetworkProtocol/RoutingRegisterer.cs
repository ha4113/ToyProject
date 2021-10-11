using System;
using System.Reflection;
using Common.Protocol.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Server.NetworkProtocol
{
    public static class RoutingRegisterer
    {
        public static void MapCustomRoutes(this IEndpointRouteBuilder builder)
        {
            var baseType = typeof(IRouteAction);
            foreach (var type in baseType.Assembly.GetTypes())
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    continue;
                }

                if (baseType.IsAssignableFrom(type) == false)
                {
                    continue;
                }

                var actionCreator = new Func<IRouteAction>(() => Activator.CreateInstance(type) as IRouteAction);
                var templateAction = actionCreator();
                if (templateAction == null)
                {
                    continue;
                }

                var reqType = templateAction.GetReqType();
                var reqAttr = reqType.GetCustomAttribute<ReqAttribute>();
                if (reqAttr == null)
                {
                    continue;
                }

                var api = reqAttr.Api;
                builder.Map(api, async httpContext =>
                {
                    var action = actionCreator();
                    await action.Run(httpContext);
                });
            }
        }
    }
}