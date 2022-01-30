using System;
using System.Threading.Tasks;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using Microsoft.AspNetCore.Http;
using Server.DBProtocol;
using Server.Enums;
using Server.Util;

namespace Server.NetworkProtocol
{
    public interface IRouteAction
    {
        Type GetReqType();
        Type GetAckType();
        Task Run(HttpContext httpContext);
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

            var (req, _) = await RoutingReader.ReadReqAsync<TReq>(httpContext);
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
                redisLocked = await RedisUtil.BeginRequestRedisLock(id);
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
                        var resultType = (exception is NetException netException) ? netException.ErrorCode : ResponseResult.InternalSystemError;
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
                            var resultType = (exception is NetException netException) ? netException.ErrorCode : ResponseResult.InternalSystemError;
                            var errorMessage = exception.ToString();
                        
                            await httpContext.SendErrorResponse(resultType, errorMessage).ConfigureAwait(false);
                            return;
                        }
                    }

                    // 요청의 응답을 보낸다.
                    await RoutingReader.WriteAckAsync(httpContext, ack);
                }
            }
            finally
            {
                if (redisLocked)
                {
                    await RedisUtil.EndRequestRedisLock(id);
                }
            }
        }
    }
}