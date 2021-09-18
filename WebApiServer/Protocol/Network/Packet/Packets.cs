using System;

namespace Protocol.Network.Packet
{
    public interface IPacket
    {
        long Id { get; }
    }

    public class GetWakeType : IPacket
    {
        public GetWakeType(long id) { Id = id; }
        public long Id { get; }
    }
}