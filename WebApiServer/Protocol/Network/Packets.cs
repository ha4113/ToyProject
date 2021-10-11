using System;
using System.Threading.Tasks;

namespace Common.Protocol.Network
{
    public interface IReq
    {
        long Id { get; }
    }

    public interface IAck
    {
        Result Result { get; }
    }
    
    public class GetTestReq : IReq
    {
        public long Id { get; }
        public GetTestReq(long id) { Id = id; }
    }
    
    public class GetTestAck : IAck
    {
        public Result Result { get; }
        public byte TestValue { get; }

        public GetTestAck(Result result, byte testValue)
        {
            TestValue = testValue;
            Result = result;
        }
        
    }
}