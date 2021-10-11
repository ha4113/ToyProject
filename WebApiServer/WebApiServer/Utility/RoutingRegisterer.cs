using System;
using System.Reflection;
using System.Threading.Tasks;
using Common.Protocol.Network;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebApiServer.Attribute;
using WebApiServer.DBProtocol;
using WebApiServer.Interface;

namespace WebApiServer.Utility
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
                var tdReqAttr = reqType.GetCustomAttribute<ReqAttribute>();
                if (tdReqAttr == null)
                {
                    continue;
                }

                var api = tdReqAttr.Api;
                builder.Map(api, async httpContext =>
                {
                    var action = actionCreator();
                    await action.Run(httpContext);
                });
            }
        }
    }
    
    public abstract class RouteAction<TReq, TAck> : IRouteAction
        where TReq : class, IReq
        where TAck : class, IAck
    {
        protected abstract Task<TAck> Run(TReq req, User user);

        Type IRouteAction.GetReqType()
        {
            return typeof(TReq);
        }

        Type IRouteAction.GetAckType()
        {
            return typeof(TAck);
        }

        async Task IRouteAction.Run(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return;
            }

            var format = httpContext.Request.Headers.GetBodyFormat();
            if (format == HttpFormat.NONE)
            {
                await httpContext.SendErrorResponse(ResponseResult.BadRequest).ConfigureAwait(false);
                return;
            }

            var (req, _) = await Reader.ReadReqAsync<TReq>(httpContext);
            if (req == null)
            {
                await httpContext.SendErrorResponse(ResponseResult.BadRequest).ConfigureAwait(false);
                return;
            }

            var id = req.Id;
            var redisLocked = false;
            
            try
            {
                // 동시요청을 막기위해 락을건다.
                redisLocked = await Util.BeginRequestRedisLock(id);
                if (redisLocked == false)
                {
                    await httpContext.SendErrorResponse(ResponseResult.SessionWorking).ConfigureAwait(false);
                    return;
                }

                // 요청한 플레이어객체를 생성한다.
                using (var user = DBConnection.Connect(id))
                {
                    if (user == null)
                    {
                        await httpContext.SendErrorResponse(ResponseResult.InvalidAccessToken).ConfigureAwait(false);
                        return;
                    }

                    // 요청을 수행한다.

                    TAck ack;
                    try
                    {
                        ack = await Run(req, user);
                    }
                    catch (Exception exception)
                    {
                        var resultType = (exception is ApiException apiException) ? apiException.ErrorCode : ResponseResult.InternalSystemError;
                        var errorMessage = exception.ToString();

                        await httpContext.SendErrorResponse(resultType, errorMessage).ConfigureAwait(false);
                        return;
                    }

                    var isSuccess = ack != null && (ack.Result == ResponseResult.Success || ack.Result == ResponseResult.NONE);
                    if (isSuccess)
                    {
                        // 플레이어의 변경사항 적용및 클라동기화 유도.
                        try
                        {
                            await user.CommitAsync();
                        }
                        catch (Exception exception)
                        {
                            var resultType = (exception is ApiException apiException) ? apiException.ErrorCode : ResponseResult.InternalSystemError;
                            var errorMessage = exception.ToString();
                        
                            await httpContext.SendErrorResponse(resultType, errorMessage).ConfigureAwait(false);
                            return;
                        }
                    }

                    // 요청의 응답을 보낸다.
                    await Reader.WriteAckAsync(httpContext, ack);
                }
            }
            finally
            {
                if (redisLocked)
                {
                    await Util.EndRequestRedisLock(id);
                }
            }
        }
    }
}