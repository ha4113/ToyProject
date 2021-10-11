using System;
using System.Threading.Tasks;
using Common.Core.Util;
using Common.Protocol.Network;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Extensions;
using StackExchange.Redis;
using TimeExtensions = Google.Protobuf.WellKnownTypes.TimeExtensions;

namespace WebApiServer.Utility
{
    public static class Util
    {
        private const int LOCK_DURATION_SECOND = 5;
        public static string GetStringValue(this Enum obj)
        {
            return obj.GetAttributeOfType<StringValue>().Value;
        }
        
        public static async Task SendErrorResponse(this HttpContext httpContext, Result resultType, string errorMessage = null)
        {
            httpContext.Response.Headers.SetHeaderResultType(resultType);
            await httpContext.SendResponseBody(errorMessage ?? string.Empty, false).ConfigureAwait(false);
        }
        
        public static async Task<bool> BeginRequestRedisLock(long id)
        {
            var redisKey = GetRedisLockKey(id);
            var time = TimeExtensions.ToTimestamp(DateTime.UtcNow);
            return await RedisUserData.Instance.Database.StringSetAsync(redisKey, time.ToString(), TimeSpan.FromSeconds(LOCK_DURATION_SECOND), When.NotExists).ConfigureAwait(false);
        }

        public static async Task EndRequestRedisLock(long id)
        {
            var redisKey = GetRedisLockKey(id);
            await RedisUserData.Instance.Database.KeyDeleteAsync(redisKey);
        }

        private static string GetRedisLockKey(long id)
        {
            return $"UserRedisLock : {id}";
        }
    }
}