using System;
using System.Threading.Tasks;
using WebApiServer.Attribute;

namespace Common.Protocol.Network
{
    public interface IReq
    {
        long Id { get; }
    }

    public interface IAck
    {
        ResponseResult Result { get; }
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
        public GetTestReq(long id) { Id = id; }
    }
    
    [Serializable]
    public class GetTestAck : IAck
    {
        public ResponseResult Result { get; set; }
        public byte TestValue { get; set; }

        public GetTestAck(ResponseResult result, byte testValue)
        {
            TestValue = testValue;
            Result = result;
        }
    }
}