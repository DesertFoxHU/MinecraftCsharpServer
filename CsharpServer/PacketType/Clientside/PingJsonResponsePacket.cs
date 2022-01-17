using Newtonsoft.Json;
using CsharpServer.Network;

namespace CsharpServer.PacketType
{
    public class PingJsonResponsePacket : ClientPacket
    {
        public static int PacketID = 0x00;

        public override int ID { get; set; } = PingJsonResponsePacket.PacketID;

        public PingPayload Payload { get; private set; }

        public PingJsonResponsePacket(PingPayload Payload)
        {
            this.Payload = Payload;
        }

        public override Packet WrapPacket()
        {
            Packet packet = new Packet(ID);
            string json = JsonConvert.SerializeObject(Payload);
            packet.WriteString(json);
            return packet;
        }
    }
}
