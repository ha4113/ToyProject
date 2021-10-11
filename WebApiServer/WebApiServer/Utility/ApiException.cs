using System;
using Common.Protocol.Network;

namespace WebApiServer.Utility
{
    public class ApiException : Exception
    {
        public Result ErrorCode { get; }
        public ApiException(Result errorCode) { ErrorCode = errorCode; }
    }
}