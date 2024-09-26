using CsharpServer.PacketType;
using System.Collections.Generic;

namespace CsharpServer.Network
{
    public delegate ServerPacket ParsePacket(Packet packet);

    /// <summary>
    /// Packets recieved by server
    /// </summary>
    public abstract class ServerPacket
    {
        public abstract int ID { get; set; }

        public static Dictionary<int, ParsePacket> parser = new Dictionary<int, ParsePacket>()
        {
            { HandshakePacket.PacketID, HandshakePacket.ParsePacket },
            { PingPacket.PacketID, PingPacket.ParsePacket },
            { KeepAliveServerPacket.PacketID, KeepAliveServerPacket.ParsePacket },
            { LoginAcknowledged.PacketID, LoginAcknowledged.ParsePacket },
            { Disconnect.PacketID, Disconnect.ParsePacket },
        };

        public static ServerPacket ParsePacket(int packetID, Packet packet)
        {
            if (!parser.ContainsKey(packetID))
            {
                Debug.Send($"{packetID.ToHexId()} is not registered packet ID!", Debug.Mode.WARN);
                return null;
            }
            return parser[packetID](packet);
        }
    }
}
