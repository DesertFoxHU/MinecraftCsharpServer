using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class KeepAliveClientPacket : ClientPacket
    {
        public static int PacketID = 0x21;

        public override int ID { get; set; } = KeepAliveClientPacket.PacketID;

        public long Value { get; private set; }

        public KeepAliveClientPacket(long Value)
        {
            this.Value = Value;
        }

        public override Packet WrapPacket()
        {
            Packet packet = new Packet(ID);
            packet.WriteLong(Value);
            return packet;
        }
    }
}
