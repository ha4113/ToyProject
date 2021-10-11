using System;
using Common.Protocol.Enums;

namespace Common.Protocol.Network
{
    public interface IAck
    {
        ResponseResult Result { get; }
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