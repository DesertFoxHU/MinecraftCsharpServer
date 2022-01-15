using CsharpServer.PacketType;
using System.Collections.Generic;

namespace CsharpServer
{
    /// <summary>
    /// Packets sent by server towards client
    /// </summary>
    public abstract class ClientPacket
    {
        public abstract int ID { get; set; }

        public abstract Packet WrapPacket();
    }
}
