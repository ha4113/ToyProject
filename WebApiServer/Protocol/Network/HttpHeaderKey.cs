using Common.Core.Util;

namespace Common.Protocol.Network
{
    public enum HttpHeader
    {
        [StringValue("format")] FORMAT_KEY,
        [StringValue("result")] RESULT_KEY,
    }
}