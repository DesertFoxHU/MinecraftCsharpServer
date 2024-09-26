using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class Disconnect : ServerPacket
    {
        public static int PacketID = 0x02;

        public override int ID { get; set; } = PacketID;

        public string Username { get; private set; }

        public Disconnect(Packet packet)
        {
            Username = packet.ReadString();
        }

        public static Disconnect ParsePacket(Packet packet)
        {
            return new Disconnect(packet);
        }
    }
}
