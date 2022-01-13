using CsharpServer.PacketType;
using System.Collections.Generic;

namespace CsharpServer
{
    public delegate AbstractPacket ParsePacket(Packet packet);

    public abstract class AbstractPacket
    {
        public abstract int ID { get; set; }

        public static Dictionary<int, ParsePacket> parser = new Dictionary<int, ParsePacket>()
        {
            { (int) 0x00, Handshake.ParsePacket},
        };

        public static AbstractPacket ParsePacket(int packetID, Packet packet)
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
