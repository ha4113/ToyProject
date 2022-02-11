using System;
using Common.Protocol.Attributes;

namespace Common.Protocol.Network
{
    public interface IReq
    {
        long Id { get; set; }
    }

    public static class UserCommand
    {
        public const string ROOT = "user";
    }

    [Serializable]
    [Req(API)]
    public class GetTestReq : IReq
    {
        private const string API = UserCommand.ROOT + "/get-test";
        public long Id { get; set; }
    }
}