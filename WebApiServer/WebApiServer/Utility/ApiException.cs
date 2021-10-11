using System;
using Common.Protocol.Network;

namespace WebApiServer.Utility
{
    public class ApiException : Exception
    {
        public ResponseResult ErrorCode { get; }
        public ApiException(ResponseResult errorCode) { ErrorCode = errorCode; }
    }
}