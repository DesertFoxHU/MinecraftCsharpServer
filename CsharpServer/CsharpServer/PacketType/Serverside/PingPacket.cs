using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class PingPacket : ServerPacket
    {
        public static int PacketID = 0x01;

        public override int ID { get; set; } = PingPacket.PacketID;

        public long Payload { get; private set; }

        public PingPacket(Packet packet)
        {
            Payload = packet.ReadLong();
        }

        public static PingPacket ParsePacket(Packet packet)
        {
            return new PingPacket(packet);
        }
    }
}
