using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using StackExchange.Redis;

namespace Server.DBProtocol
{
    public static class RedisUtil
    {
        private const int LOCK_DURATION_SECOND = 5;
        
        public static async Task<bool> BeginRequestRedisLock(long id)
        {
            var redisKey = GetRedisLockKey(id);
            var time = DateTime.UtcNow.ToTimestamp();
            var db = RedisUserData.Instance.Database;
            return await db.StringSetAsync(redisKey, 
                                           time.ToString(),
                                           TimeSpan.FromSeconds(LOCK_DURATION_SECOND),
                                           When.NotExists)
                           .ConfigureAwait(false);
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