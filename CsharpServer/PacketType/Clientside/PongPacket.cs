using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class PongPacket : ClientPacket
    {
        public static int PacketID = 0x01;

        public override int ID { get; set; } = PongPacket.PacketID;

        public long Payload { get; private set; }

        public PongPacket(long Payload)
        {
            this.Payload = Payload;
        }

        public override Packet WrapPacket()
        {
            Packet packet = new Packet(ID);
            packet.WriteLong(Payload);
            return packet;
        }
    }
}
