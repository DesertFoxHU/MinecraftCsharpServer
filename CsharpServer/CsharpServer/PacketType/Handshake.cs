namespace CsharpServer.PacketType
{
    public class Handshake : AbstractPacket
    {
        public override int ID { get; set; } = 0x00;

        public int ProtocolVersion { get; private set; }

        public string ServerAddress { get; private set; }

        public ushort Port { get; private set; }

        public int Status { get; private set; } = 0;

        public Handshake(Packet packet)
        {
            ProtocolVersion = packet.ReadVarInt();
            ServerAddress = packet.ReadString();
            Port = packet.ReadUnsignedShort();
            Status = packet.ReadVarInt();
        }

        public static Handshake ParsePacket(Packet packet)
        {
            return new Handshake(packet);
        }
    }
}
