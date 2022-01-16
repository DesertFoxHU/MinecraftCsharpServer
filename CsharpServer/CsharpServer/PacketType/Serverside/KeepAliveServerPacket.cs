using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class KeepAliveServerPacket : ServerPacket
    {
        public static int PacketID = 0x0F;

        public override int ID { get; set; } = KeepAliveServerPacket.PacketID;

        public long Value { get; private set; }

        public KeepAliveServerPacket(Packet packet)
        {
            Value = packet.ReadLong();
        }

        public static KeepAliveServerPacket ParsePacket(Packet packet)
        {
            return new KeepAliveServerPacket(packet);
        }
    }
}
