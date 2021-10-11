using System;
using Common.Core.Util;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Server.DBProtocol
{
    public abstract class RedisDb<T> : Singleton<T>
        where T : new()
    {
        public IDatabase Database { get; private set; }
        public ISubscriber Subscriber { get; private set; }
        public bool IsConnected { get; private set; }
        public string GroupId { get; private set; }
        public virtual string ConfigKeyName { get; }

        public void Initialize(IConfigurationSection config)
        {
            if (config == null)
            {
                return;
            }

            var host = config.GetValue<string>($"{ConfigKeyName}:Host");
            var port = config.GetValue<int>($"{ConfigKeyName}:Port");
            var password = config.GetValue<string>($"{ConfigKeyName}:Password");
            var ssl = config.GetValue<bool>($"{ConfigKeyName}:Ssl");
            var dbId = config.GetValue<int>($"{ConfigKeyName}:DbId");

            GroupId = config.GetValue<string>("GroupId");

            try
            {
                var configOptions = new ConfigurationOptions();
                configOptions.EndPoints.Add(host, port);
                configOptions.Password = password;
                configOptions.Ssl = ssl;
                configOptions.AbortOnConnectFail = true;

                var connectionMultiplexer = ConnectionMultiplexer.Connect(configOptions);

                Database = connectionMultiplexer.GetDatabase(dbId);
                Subscriber = connectionMultiplexer.GetSubscriber();
                IsConnected = true;

                InitializeImpl(config);

                Serilog.Log.Information($"Connected to {typeof(T)} server ({host}:{port}).");
            }
            catch (Exception e)
            {
                IsConnected = false;

                Serilog.Log.Error($"Can't connect to Redis({host}:{port}). ({e.Message})", e);
            }
        }

        protected virtual void InitializeImpl(IConfigurationSection config)
        {
        }
    }

    public class RedisUserData : RedisDb<RedisUserData>
    {
        public override string ConfigKeyName => "UserData";
    }
}