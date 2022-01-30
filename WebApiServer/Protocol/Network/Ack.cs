using System;
using Common.Protocol.Enums;

namespace Common.Protocol.Network
{
    public interface IAck
    {
        ResponseResult Result { get; }
        // TODO : Ack 내에 DB 변경사항 추가
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