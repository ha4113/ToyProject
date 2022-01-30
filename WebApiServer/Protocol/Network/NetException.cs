using System;
using Common.Protocol.Enums;

namespace Common.Protocol.Network
{
    public class NetException : Exception
    {
        public ResponseResult ErrorCode { get; }
        public NetException(ResponseResult errorCode) { ErrorCode = errorCode; }
    }
}