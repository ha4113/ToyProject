using System;
using Common.Protocol.Enums;

namespace Server.NetworkProtocol
{
    public class ApiException : Exception
    {
        public ResponseResult ErrorCode { get; }
        public ApiException(ResponseResult errorCode) { ErrorCode = errorCode; }
    }
}